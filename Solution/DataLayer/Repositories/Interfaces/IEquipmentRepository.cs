using System.Collections.Generic;
using Models.Entities;

namespace DataLayer.Repositories.Interfaces
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
