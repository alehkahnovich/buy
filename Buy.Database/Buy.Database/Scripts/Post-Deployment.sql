/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/
--!test123
DECLARE @ClientKey NVARCHAR(50) = N'b0cd9875-fc55-4106-a56e-01e12ccac781'
DECLARE @UserKey NVARCHAR(50) = N'b0cd9875-fc55-4106-a56e-01e12ccac782'

CREATE TABLE #DestinationResource (
        ResourceId INT NOT NULL,
        Name NVARCHAR(255) NOT NULL,
        Description NVARCHAR(MAX),
        Type NVARCHAR(50) NOT NULL
    );
CREATE TABLE #SourceResource(
        Name NVARCHAR(255) NOT NULL,
        Description NVARCHAR(MAX),
        Type NVARCHAR(50) NOT NULL
    );
CREATE TABLE #SourceUri (
        ClientId UNIQUEIDENTIFIER,
        Uri NVARCHAR(MAX) NOT NULL,
        Type NVARCHAR(50)
    );
CREATE TABLE #SourceClaim (
        Name NVARCHAR(50) NOT NULL,
        Resource NVARCHAR(50) NOT NULL
    );

INSERT INTO #SourceClaim
    VALUES
    (N'sub', N'openid'),
    (N'name', N'profile'),
    (N'family_name', N'profile'),
    (N'given_name', N'profile'),
    (N'middle_name', N'profile'),
    (N'nickname', N'profile'),
    (N'preferred_username', N'profile'),
    (N'profile', N'profile'),
    (N'picture', N'profile'),
    (N'website', N'profile'),
    (N'gender', N'profile'),
    (N'birthdate', N'profile'),
    (N'zoneinfo', N'profile'),
    (N'locale', N'profile'),
    (N'updated_at', N'profile');
    
INSERT INTO #SourceResource
    VALUES
    (N'identity-resource', N'default identity provider scope', N'Api'),
    (N'openid', N'default OpenID Connect scope', N'Identity'),
    (N'profile', N'OpenID Connect identity scope', N'Identity');

INSERT INTO #SourceUri
    VALUES
    (@ClientKey, N'http://localhost:5100/home/signin-oidc', N'Redirect'),
    (@ClientKey, N'http://localhost:5100/signout-callback-oidc', N'PostLogout');

BEGIN
    MERGE IDP.Resource AS target
    USING (SELECT Name, Description, Type FROM #SourceResource) AS source (Name, Description, Type)
    ON (target.Name = source.Name AND target.Type = source.Type)
    WHEN NOT MATCHED BY SOURCE
        THEN DELETE
    WHEN NOT MATCHED 
        THEN INSERT (Name, Description, Type)
        VALUES (source.Name, source.Description, source.Type)
        OUTPUT inserted.* INTO #DestinationResource;
    
END

BEGIN
    MERGE IDP.Client AS target
    USING (SELECT @ClientKey, N'root', 0, 1) as source (ClientId, Name, ReuseRefresh, AllowOfflineAccess)
    ON (target.ClientID = source.ClientId)
    WHEN NOT MATCHED 
        THEN INSERT (ClientId, Name, ReuseRefresh, AllowOfflineAccess)
        VALUES (source.ClientId, source.Name, source.ReuseRefresh, source.AllowOfflineAccess);
END


BEGIN
    MERGE #DestinationResource as target
    USING (SELECT ResourceId, Name, Description, Type FROM IDP.Resource) as source (ResourceId, Name, Description, Type)
    ON (source.ResourceId = target.ResourceId)
    WHEN NOT MATCHED 
        THEN INSERT (ResourceId, Name, Description, Type)
        VALUES (source.ResourceId, source.Name, source.Description, source.Type);
END

BEGIN
    MERGE IDP.ClientScope as target
    USING (SELECT @ClientKey, ResourceId FROM #DestinationResource) as source (ClientId, ResourceId)
    ON (target.ClientId = source.ClientId AND target.ResourceId = source.ResourceId)
    WHEN NOT MATCHED 
        THEN INSERT (ClientId, ResourceId)
        VALUES (source.ClientId, source.ResourceId);
END

BEGIN
    MERGE IDP.ClientSecret as target
    USING (SELECT @ClientKey, N'ClbjgTzfm6maoN7l1soZt/hlqCDpmFHonK6AP7O+G1I=', NULL) as source (ClientId, Pwd, Expiration)
    ON (target.ClientId = source.ClientId AND target.Value = source.Pwd)
    WHEN NOT MATCHED
        THEN INSERT (ClientId, Value, Expiration)
        VALUES (source.ClientId, source.Pwd, source.Expiration);
END

BEGIN
    MERGE IDP.ClientUri as target
    USING (SELECT ClientId, Uri, Type FROM #SourceUri) as source (ClientId, Uri, Type)
    ON (target.ClientId = source.ClientId AND target.Type = source.Type)
    WHEN NOT MATCHED
        THEN INSERT (ClientId, Uri, Type)
        VALUES (source.ClientId, source.Uri, source.Type);
END

BEGIN
    MERGE IDP.Claim as target
    USING (SELECT Name FROM #SourceClaim) as source (Name)
    ON (target.Name = source.Name)
    WHEN NOT MATCHED
        THEN INSERT (Name)
        VALUES (source.Name);
END


CREATE TABLE #SourceClaimResource (
        ResourceId INT NOT NULL,
        ClaimId INT NOT NULL
    );

BEGIN
    INSERT INTO #SourceClaimResource
    SELECT 
        r.ResourceId, 
        c.ClaimId
    FROM #SourceClaim as sc
    INNER JOIN IDP.Claim as c
        ON c.Name = sc.Name 
    LEFT OUTER JOIN IDP.Resource as r
        ON sc.Resource = r.Name
    WHERE r.ResourceId IS NOT NULL 

    MERGE IDP.ResourceClaim as target
    USING (SELECT ResourceId, ClaimId FROM #SourceClaimResource) as source (ResourceId, ClaimId)
    ON (target.ResourceId = source.ResourceId AND target.ClaimId = source.ClaimId)
    WHEN NOT MATCHED 
        THEN INSERT (ResourceId, ClaimId)
        VALUES (source.ResourceId, source.ClaimId);
END


BEGIN
    MERGE IDP.[User] as target
    USING (SELECT @UserKey, N'bob@email.com', N'ClbjgTzfm6maoN7l1soZt/hlqCDpmFHonK6AP7O+G1I=') as source (UserId, Email, Password)
    ON (target.UserId = source.UserId)
    WHEN NOT MATCHED
        THEN INSERT (UserId, Email, Password)
        VALUES (source.UserId, source.Email, source.Password);
END

/*----Categories Begin-----*/

CREATE TABLE #Categories (
        CategoryId INT NOT NULL,
        ParentId INT,
        Name NVARCHAR(255)
    );

INSERT INTO #Categories
    VALUES
    (1, NULL, N'Продать'),
	(100, NULL, N'Доставить'),
	(1000, NULL, N'Купить');

BEGIN
    SET IDENTITY_INSERT CONTENT.Category ON;  

    BEGIN    
        MERGE CONTENT.Category as target
        USING (SELECT CategoryId, ParentId, Name FROM #Categories) 
        as source (CategoryId, ParentId, Name)
        ON (target.CategoryId = source.CategoryId)
        WHEN NOT MATCHED 
            THEN INSERT (CategoryId, ParentId, Name)
            VALUES (SOURCE.CategoryId, SOURCE.ParentId, SOURCE.Name);
    END

    SET IDENTITY_INSERT CONTENT.Category OFF;  
END

/*----Categories End-----*/
/*
/*----Properties Begin-----*/

CREATE TABLE #PropertyControl (
        ControlId INT NOT NULL,
        Type NVARCHAR(50) NOT NULL
    );

INSERT INTO #PropertyControl
    VALUES
    (1, N'text'),
    (2, N'number'),
    (3, N'year'),
    (4, N'date');

BEGIN
    MERGE CONTENT.Control as target
        USING (SELECT ControlId, Type FROM #PropertyControl) as source (ControlId, Type)
        ON (target.ControlId = source.ControlId AND target.Type = source.Type)
        WHEN NOT MATCHED 
            THEN INSERT (ControlId, Type)
            VALUES (source.ControlId, source.Type)
        WHEN NOT MATCHED BY SOURCE
            THEN DELETE;
END


CREATE TABLE #PropertyBehavior (
        BehaviorId INT NOT NULL,
        Type NVARCHAR(50) NOT NULL
    );

INSERT INTO #PropertyBehavior
    VALUES
    (1, N'single'),
    (2, N'multiple');

BEGIN
    MERGE CONTENT.Behavior as target
        USING (SELECT BehaviorId, Type FROM #PropertyBehavior) as source (BehaviorId, Type)
        ON (target.BehaviorId = source.BehaviorId AND target.Type = source.Type)
        WHEN NOT MATCHED 
            THEN INSERT (BehaviorId, Type)
            VALUES (source.BehaviorId, source.Type)
        WHEN NOT MATCHED BY SOURCE
            THEN DELETE;
END



CREATE TABLE #Properties (
        PropertyId INT NOT NULL,
        ControlId INT,
        BehaviorId INT,
        ParentId INT,
        Name NVARCHAR(MAX),
        IsFacet bit,
        Type NVARCHAR(50) NOT NULL,
        CategoryId INT,
        SortOrder INT
    );

INSERT INTO #Properties
    VALUES 
    /*PropertyId, ControlId(1-text,2-number,3-year,4-data), BehaviorId(1-single,2-multiple), ParentId, Name, IsFacet, Type*/
    (1, 2, 1, NULL, N'Объем Двигателя', 1, N'number', 2, 20),
    (2, NULL, NULL, 1, N'1.0', 0, N'number', 2, 0),
    (3, NULL, NULL, 1, N'1.2', 0, N'number', 2, 0),
    (4, NULL, NULL, 1, N'1.4', 0, N'number', 2, 0),
    (5, NULL, NULL, 1, N'1.6', 0, N'number', 2, 0),
    (6, NULL, NULL, 1, N'1.8', 0, N'number', 2, 0),
    (7, NULL, NULL, 1, N'2.0', 0, N'number', 2, 0),
    (8, NULL, NULL, 1, N'2.2', 0, N'number', 2, 0),
    (9, NULL, NULL, 1, N'2.4', 0, N'number', 2, 0),
    (10, NULL, NULL, 1, N'2.8', 0, N'number', 2, 0),
    (11, NULL, NULL, 1, N'3.0', 0, N'number', 2, 0),
    (12, NULL, NULL, 1, N'3.2', 0, N'number', 2, 0),
    (13, NULL, NULL, 1, N'3.4', 0, N'number', 2, 0),
    (14, NULL, NULL, 1, N'3.6', 0, N'number', 2, 0),
    (15, NULL, NULL, 1, N'3.8', 0, N'number', 2, 0),
    (16, NULL, NULL, 1, N'4.0', 0, N'number', 2, 0),
    (17, NULL, NULL, 1, N'4.2', 0, N'number', 2, 0),
    (18, NULL, NULL, 1, N'4.4', 0, N'number', 2, 0),
    (19, NULL, NULL, 1, N'4.8', 0, N'number', 2, 0),
    /*PropertyId, ControlId(1-text,2-number,3-year,4-data), BehaviorId(1-single,2-multiple), ParentId, Name, IsFacet, Type*/
    (50, 2, 1, NULL, N'Тип Двигателя', 1, N'string', 2, 30),
    (51, NULL, NULL, 50, N'Бензин', 0, N'string', 2, 0),
    (52, NULL, NULL, 50, N'Дизель', 0, N'string', 2, 0),
    (53, NULL, NULL, 50, N'Гибрид', 0, N'string', 2, 0),
    (54, NULL, NULL, 50, N'Гибрид(Дизель)', 0, N'string', 2, 0),
    (55, NULL, NULL, 50, N'Гибрид(Бензин)', 0, N'string', 2, 0),
    (56, NULL, NULL, 50, N'Электро', 0, N'string', 2, 0),
    /*PropertyId, ControlId(1-text,2-number,3-year,4-date), BehaviorId(1-single,2-multiple), ParentId, Name, IsFacet, Type*/
    (100, 3, 1, NULL, N'Год выпуска', 1, N'numberrange', 2, 40),
	/*PropertyId, ControlId(1-text,2-number,3-year,4-date), BehaviorId(1-single,2-multiple), ParentId, Name, IsFacet, Type*/
    (150, 1, 1, NULL, N'Кузов', 1, N'string', 2, 50),
	(151, NULL, NULL, 150, N'Седан', 0, N'string', 2, 0),
	(152, NULL, NULL, 150, N'Хетчбэк', 0, N'string', 2, 0),
	(153, NULL, NULL, 150, N'Лифтбэк', 0, N'string', 2, 0),
	(154, NULL, NULL, 150, N'Внедорожник', 0, N'string', 2, 0),
	(155, NULL, NULL, 150, N'Универсал', 0, N'string', 2, 0),
	(156, NULL, NULL, 150, N'Минивэн', 0, N'string', 2, 0),
	(157, NULL, NULL, 150, N'Микроавтобус', 0, N'string', 2, 0),
	(158, NULL, NULL, 150, N'Купе', 0, N'string', 2, 0),
	(159, NULL, NULL, 150, N'Фургон', 0, N'string', 2, 0),
	(160, NULL, NULL, 150, N'Пикап', 0, N'string', 2, 0),
	(161, NULL, NULL, 150, N'Кабриолет', 0, N'string', 2, 0),
	(162, NULL, NULL, 150, N'Лимузин', 0, N'string', 2, 0),
	/*PropertyId, ControlId(1-text,2-number,3-year,4-date), BehaviorId(1-single,2-multiple), ParentId, Name, IsFacet, Type*/
    (200, 1, 1, NULL, N'Коробка передач', 1, N'string', 2, 60),
	(201, NULL, NULL, 200, N'Автоматическая', 0, N'string', 2, 0),
	(202, NULL, NULL, 200, N'Механическая', 0, N'string', 2, 0),
	/*PropertyId, ControlId(1-text,2-number,3-year,4-date), BehaviorId(1-single,2-multiple), ParentId, Name, IsFacet, Type*/
    (250, 1, 1, NULL, N'Привод', 1, N'string', 2, 70),
	(251, NULL, NULL, 250, N'Передний', 0, N'string', 2, 0),
	(252, NULL, NULL, 250, N'Задний', 0, N'string', 2, 0),
	(253, NULL, NULL, 250, N'Полный', 0, N'string', 2, 0),
	/*PropertyId, ControlId(1-text,2-number,3-year,4-date), BehaviorId(1-single,2-multiple), ParentId, Name, IsFacet, Type*/
	(300, 1, 2, NULL, N'Опции', 1, N'string', 2, 80),
	(301, NULL, NULL, 300, N'Кондиционер', 0, N'string', 2, 0),
	(302, NULL, NULL, 300, N'Кожанный салон', 0, N'string', 2, 0),
	(303, NULL, NULL, 300, N'Легкосплавные диски', 0, N'string', 2, 0),
	(304, NULL, NULL, 300, N'Ксенон', 0, N'string', 2, 0),
	(305, NULL, NULL, 300, N'Парктроник', 0, N'string', 2, 0),
	(306, NULL, NULL, 300, N'Подогрев сидений', 0, N'string', 2, 0),
	(307, NULL, NULL, 300, N'Система контроля стабилизации', 0, N'string', 2, 0),
	(308, NULL, NULL, 300, N'Навигация', 0, N'string', 2, 0),
	(309, NULL, NULL, 300, N'Громкая связь', 0, N'string', 2, 0),
	/*PropertyId, ControlId(1-text,2-number,3-year,4-date), BehaviorId(1-single,2-multiple), ParentId, Name, IsFacet, Type*/
	(600, 1, 1, NULL, N'Марка', 1, N'string', 2, 10),
	(601, NULL, NULL, 600, N'Acura', 0, N'string', 2, 1),
	(602, NULL, NULL, 600, N'Alfa Romeo', 0, N'string', 2, 2),
	(603, NULL, NULL, 600, N'Aston Martin', 0, N'string', 2, 2),
	(604, NULL, NULL, 600, N'Audi', 0, N'string', 2, 2),
	(605, NULL, NULL, 600, N'Bentley', 0, N'string', 2, 2),
	(606, NULL, NULL, 600, N'BMW', 0, N'string', 2, 2),
	(607, NULL, NULL, 600, N'Cadillac', 0, N'string', 2, 2),
	(608, NULL, NULL, 600, N'Chery', 0, N'string', 2, 2),
	(609, NULL, NULL, 600, N'Chevrolet', 0, N'string', 2, 2),
	(610, NULL, NULL, 600, N'Chrysler', 0, N'string', 2, 2),
	(611, NULL, NULL, 600, N'Citroen', 0, N'string', 2, 2),
	(612, NULL, NULL, 600, N'Daewoo', 0, N'string', 2, 2),
	(613, NULL, NULL, 600, N'DAF', 0, N'string', 2, 2),
	(614, NULL, NULL, 600, N'Dodge', 0, N'string', 2, 2),
	(615, NULL, NULL, 600, N'Chevrolet', 0, N'string', 2, 2),
	(616, NULL, NULL, 600, N'Fiat', 0, N'string', 2, 2),
	(617, NULL, NULL, 600, N'Ford', 0, N'string', 2, 2),
	(618, NULL, NULL, 600, N'Geely', 0, N'string', 2, 2),
	(619, NULL, NULL, 600, N'Genesis', 0, N'string', 2, 2),
	(620, NULL, NULL, 600, N'GMC', 0, N'string', 2, 2),
	(621, NULL, NULL, 600, N'Honda', 0, N'string', 2, 2),
	(622, NULL, NULL, 600, N'Hummer', 0, N'string', 2, 2),
	(623, NULL, NULL, 600, N'Hyundai', 0, N'string', 2, 2),
	(624, NULL, NULL, 600, N'Infiniti', 0, N'string', 2, 2),
	(625, NULL, NULL, 600, N'Isuzu', 0, N'string', 2, 2),
	(626, NULL, NULL, 600, N'Jaguar', 0, N'string', 2, 2),
	(627, NULL, NULL, 600, N'Jeep', 0, N'string', 2, 2),
	(628, NULL, NULL, 600, N'Kia', 0, N'string', 2, 2),
	(629, NULL, NULL, 600, N'Ford', 0, N'string', 2, 2),
	(630, NULL, NULL, 600, N'LADA', 0, N'string', 2, 2),
	(631, NULL, NULL, 600, N'Land Rover', 0, N'string', 2, 2),
	(632, NULL, NULL, 600, N'Lexus', 0, N'string', 2, 2),
	(633, NULL, NULL, 600, N'Lincoln', 0, N'string', 2, 2),
	(634, NULL, NULL, 600, N'Mazda', 0, N'string', 2, 2),
	(635, NULL, NULL, 600, N'Mercedes-Benz', 0, N'string', 2, 2),
	(636, NULL, NULL, 600, N'Mini', 0, N'string', 2, 2),
	(637, NULL, NULL, 600, N'Mitusbishi', 0, N'string', 2, 2),
	(638, NULL, NULL, 600, N'Nissan', 0, N'string', 2, 2),
	(639, NULL, NULL, 600, N'Opel', 0, N'string', 2, 2),
	(640, NULL, NULL, 600, N'Peugeout', 0, N'string', 2, 2),
	(641, NULL, NULL, 600, N'Pontiac', 0, N'string', 2, 2),
	(642, NULL, NULL, 600, N'Porsche', 0, N'string', 2, 2),
	(643, NULL, NULL, 600, N'Renault', 0, N'string', 2, 2),
	(644, NULL, NULL, 600, N'Rolls-Royce', 0, N'string', 2, 2),
	(645, NULL, NULL, 600, N'Rover', 0, N'string', 2, 2),
	(646, NULL, NULL, 600, N'Saab', 0, N'string', 2, 2),
	(647, NULL, NULL, 600, N'Seat', 0, N'string', 2, 2),
	(648, NULL, NULL, 600, N'Skoda', 0, N'string', 2, 2),
	(649, NULL, NULL, 600, N'Smart', 0, N'string', 2, 2),
	(650, NULL, NULL, 600, N'Subaru', 0, N'string', 2, 2),
	(651, NULL, NULL, 600, N'Suzuki', 0, N'string', 2, 2),
	(652, NULL, NULL, 600, N'Tesla', 0, N'string', 2, 2),
	(653, NULL, NULL, 600, N'Toyota', 0, N'string', 2, 2),
	(654, NULL, NULL, 600, N'Tesla', 0, N'string', 2, 2),
	(655, NULL, NULL, 600, N'Volkswagen', 0, N'string', 2, 2),
	(656, NULL, NULL, 600, N'Volvo', 0, N'string', 2, 2)
    ;

--BEGIN
--    SET IDENTITY_INSERT CONTENT.Property ON;  

--    BEGIN    
--        MERGE CONTENT.Property as target
--        USING (SELECT PropertyId, ControlId, BehaviorId, ParentId, Name, IsFacet, Type, SortOrder FROM #Properties) 
--        as source (PropertyId, ControlId, BehaviorId, ParentId, Name, IsFacet, Type, SortOrder)
--        ON (target.PropertyId = source.PropertyId)
--		WHEN MATCHED THEN
--			UPDATE SET 
--				ControlId = SOURCE.ControlId,
--				BehaviorId = SOURCE.BehaviorId,
--				ParentId = SOURCE.ParentId,
--				Name = SOURCE.Name,
--				IsFacet = SOURCE.IsFacet,
--				Type = SOURCE.Type,
--				SortOrder = SOURCE.SortOrder
--		WHEN NOT MATCHED BY SOURCE
--			THEN DELETE
--        WHEN NOT MATCHED
--            THEN INSERT (PropertyId, ControlId, BehaviorId, ParentId, Name, IsFacet, Type, SortOrder)
--            VALUES (SOURCE.PropertyId, SOURCE.ControlId, SOURCE.BehaviorId, SOURCE.ParentId, SOURCE.Name, SOURCE.IsFacet, SOURCE.Type, SOURCE.SortOrder);
--    END

--    SET IDENTITY_INSERT CONTENT.Property OFF;  


--    BEGIN
--        MERGE CONTENT.CategoryProperty as target
--        USING (SELECT PropertyId, CategoryId from #Properties) 
--        as source (PropertyId, CategoryId)
--        ON (target.PropertyId = source.PropertyId AND target.CategoryId = source.CategoryId)
--        WHEN NOT MATCHED
--            THEN INSERT (PropertyId, CategoryId)
--            VALUES (SOURCE.PropertyId, SOURCE.CategoryId);
--    END
--END
 

/*----Properties End-----*/
*/
