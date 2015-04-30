namespace Models.Entities
{
    public class Roles
    {
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Role { get; set; }
    }
}
