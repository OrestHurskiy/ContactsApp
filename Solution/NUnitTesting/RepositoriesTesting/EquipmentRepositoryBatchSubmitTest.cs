using System.Collections.Generic;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
   [TestFixture]
    public class EquipmentRepositoryBatchSubmitTest
    {
        private IContextManager contextManager;
        private IEquipmentRepository equipmentRepository;
        private Equipment equipmentToCreate;
        private Equipment equipmentToCreate1;
        private Equipment equipmentToDelete;
        private Equipment equipmentToDelete1;
        private Equipment equipmentToUpdate;
        private Equipment equipmentToUpdate1;
        private Equipment equipmentToGet;


        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager();
            equipmentRepository = new EquipmentRepository(contextManager);

            equipmentToCreate = new Equipment
            {
                EquipmentName = EquipmentType.Laptop,
                IsAvaliable = true,
                IsWorking = true
            };
            equipmentToCreate1 = new Equipment
            {
                EquipmentName = EquipmentType.Pc, 
                IsWorking = false, 
                IsAvaliable = true
            };
            equipmentToUpdate = new Equipment
            {
                EquipmentName = EquipmentType.Phone,
                IsAvaliable = true,
                IsWorking = true
            };
            equipmentToUpdate1 = new Equipment
            {
                EquipmentName = EquipmentType.Table,
                IsAvaliable = false,
                IsWorking = false
            };
            equipmentToDelete = new Equipment
            {
                EquipmentName = EquipmentType.Laptop,
                IsAvaliable = true,
                IsWorking = true
            };
            equipmentToDelete1 = new Equipment
            {
                EquipmentName = EquipmentType.Laptop,
                IsAvaliable = true,
                IsWorking = false
            };
            equipmentToGet = new Equipment
            {
                EquipmentName = EquipmentType.Laptop,
                IsAvaliable = true,
                IsWorking = true
            };

            equipmentRepository.Create(equipmentToUpdate);
            equipmentRepository.Create(equipmentToUpdate1);
            equipmentRepository.Create(equipmentToGet);
            contextManager.BatchSave();

        }

        [TearDown]
        public void TearDown()
        {
            equipmentRepository.Delete(equipmentToUpdate);
            equipmentRepository.Delete(equipmentToUpdate1);
            equipmentRepository.Delete(equipmentToGet);
            contextManager.BatchSave();
        }

        [Test]
        public void InsertEquipment_ToDatabase_InBatchMode_Success()
        {
            equipmentRepository.Create(equipmentToCreate);
            equipmentRepository.Create(equipmentToCreate1);
            contextManager.BatchSave();

            Assert.IsNotNull(equipmentRepository.GetEquipmentById(equipmentToCreate.Id));
            Assert.IsNotNull(equipmentRepository.GetEquipmentById(equipmentToCreate1.Id));

            equipmentRepository.Delete(equipmentToCreate);
            equipmentRepository.Delete(equipmentToCreate1);
        }

        [Test]
        public void UpdateEquipment_FromDatabase_InBatchMode_Success()
        {
            equipmentToUpdate.EquipmentName = EquipmentType.Laptop;
            equipmentToUpdate.IsAvaliable = false;
            equipmentToUpdate.IsWorking = true;
            equipmentToUpdate1.EquipmentName = EquipmentType.Pc;
            equipmentToUpdate1.IsAvaliable = true;
            equipmentToUpdate1.IsWorking = false;
            equipmentRepository.Update(equipmentToUpdate);
            equipmentRepository.Update(equipmentToUpdate1);
            contextManager.BatchSave();

            var updated = equipmentRepository.GetEquipmentById(equipmentToUpdate.Id);
            Assert.IsNotNull(updated);
            Assert.AreEqual(EquipmentType.Laptop,updated.EquipmentName);
            Assert.IsFalse(updated.IsAvaliable);
            Assert.IsTrue(updated.IsWorking);

            var updated1 = equipmentRepository.GetEquipmentById(equipmentToUpdate1.Id);
            Assert.IsNotNull(updated1);
            Assert.AreEqual(EquipmentType.Pc, updated1.EquipmentName);
            Assert.IsTrue(updated1.IsAvaliable);
            Assert.IsFalse(updated1.IsWorking);

        }

        [Test]
        public void DeleteEquipment_FromDatabase_InBatchMode_Success()
        {
            equipmentRepository.Create(equipmentToDelete);
            equipmentRepository.Create(equipmentToDelete1);
            contextManager.BatchSave();

            Assert.IsTrue(equipmentRepository.Delete(equipmentToDelete));
            Assert.IsTrue(equipmentRepository.Delete(equipmentToDelete1));
            contextManager.BatchSave();

            Assert.IsNull(equipmentRepository.GetEquipmentById(equipmentToDelete.Id));
            Assert.IsNull(equipmentRepository.GetEquipmentById(equipmentToDelete1.Id));
        }

        [Test]
        public void GetEquipmentById_FromDatabase_Success()
        {
            var equipment = equipmentRepository.GetEquipmentById(equipmentToGet.Id);
            Assert.IsNotNull(equipment);
            Assert.AreEqual(EquipmentType.Laptop, equipment.EquipmentName);
            Assert.IsTrue(equipment.IsAvaliable);
            Assert.IsTrue(equipment.IsWorking);
            
        }

        [Test]
        public void GetEquipmentByEquipmentType_FromDatabase_Success()
        {
            var equipment = equipmentRepository.GetEquipmentByEquipmentType(equipmentToGet.EquipmentName);
            Assert.IsNotNull(equipment);
            Assert.AreEqual(EquipmentType.Laptop, equipment.EquipmentName);
            Assert.IsTrue(equipment.IsAvaliable);
            Assert.IsTrue(equipment.IsWorking);
        }

        [Test]
        public void GetEquipments_FromDatabase_Success()
        {
            var equipments = equipmentRepository.GetEquipments();
            Assert.IsNotNull(equipments);
            Assert.IsNotEmpty(equipments);
            Assert.IsInstanceOf(typeof(List<Equipment>), equipments);
        }
    }
}

