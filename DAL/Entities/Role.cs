namespace DAL.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<User> Users { get; set; } = null!;
}