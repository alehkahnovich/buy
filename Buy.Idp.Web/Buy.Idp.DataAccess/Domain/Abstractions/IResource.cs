namespace Buy.Idp.DataAccess.Domain.Abstractions
{
    public interface IResource {
        int ResourceId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Type { get; set; }
    }
}