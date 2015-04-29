using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Components;

namespace DataLayer.Wrapper
{
    public class EquipmentRepository:IEquipmentRepository
    {
        private readonly IContextManager contextManager;

        public EquipmentRepository(IContextManager contextManager)
        {
            this.contextManager = contextManager;
        }

        public void Create(Equipment equipment)
        {
            if (equipment == null) return;
            var context = contextManager.CurrentContext;
            context.Equipments.Add(equipment);
            contextManager.Save(context);
        }

        public void Update(Equipment equipment)
        {
            if (equipment == null) return;
            var context = contextManager.CurrentContext;
            context.Entry(equipment).State = EntityState.Modified;
            contextManager.Save(context);
        }

        public bool Delete(Equipment equipment)
        {
            if (equipment == null) return false;
            var context = contextManager.CurrentContext;
            context.Entry(equipment).State = EntityState.Deleted;
            contextManager.Save(context);
            return true;
        }

        public Equipment GetEquipmentById(int equipmentId)
        {
            return contextManager.CurrentContext.Equipments.AsNoTracking().FirstOrDefault(e => e.Id == equipmentId);
        }

        public Equipment GetEquipmentByEquipmentType(EquipmentType equipmentType)
        {
            return contextManager.CurrentContext.Equipments.AsNoTracking().FirstOrDefault(e=>e.EquipmentName==equipmentType);
        }

        public IList<Equipment> GetEquipments()
        {
            return contextManager.CurrentContext.Equipments.AsNoTracking().ToList();
        }
    }
}
