namespace Models.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public EquipmentType EquipmentName { get; set; }
        public bool IsAvaliable { get; set; }
        public bool IsWorking { get; set; }
        public Person Person { get; set; }
    }

    public enum EquipmentType
    {
        Pc,
        Laptop,
        Table,
        Phone
    }
}
