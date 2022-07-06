using System;

namespace Buy.Content.DataAccess.Domains.Abstractions
{
    public interface ITimeDomain {
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}