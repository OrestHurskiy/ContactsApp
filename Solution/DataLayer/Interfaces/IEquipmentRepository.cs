using System.Collections.Generic;
using DataLayer.Components;

namespace DataLayer.Wrapper
{
    public interface IEquipmentRepository
    {
        void Create(Equipment equipment);
        void Update(Equipment equipment);
        bool Delete(Equipment equipment);
        Equipment GetEquipmentById(int equipmentId);
        Equipment GetEquipmentByEquipmentType(EquipmentType equipmentType);
        IList<Equipment> GetEquipments();
    }
}
