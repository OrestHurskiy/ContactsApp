using System.Collections.Generic;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Models.Entities;
using NUnit.Framework;

namespace NUnitTesting.RepositoriesTesting
{
    [TestFixture]
    public class EquipmentRepositorySingleSubmitTest
    {
        private IContextManager contextManager;
        private IEquipmentRepository equipmentRepository;
        private Equipment equipmentToCreate;
        private Equipment equipmentToDelete;
        private Equipment equipmentToUpdate;
        private Equipment equipmentToGet;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager(false);
            equipmentRepository = new EquipmentRepository(contextManager);

            equipmentToCreate = new Equipment
            {
                EquipmentName = EquipmentType.Laptop,
                IsAvaliable = true,
                IsWorking = true
            };
            equipmentToUpdate = new Equipment
            {
                EquipmentName = EquipmentType.Phone,
                IsAvaliable = true,
                IsWorking = true
            };
            equipmentToDelete = new Equipment
            {
                EquipmentName = EquipmentType.Laptop,
                IsAvaliable = true,
                IsWorking = true
            };
            equipmentToGet = new Equipment
            {
                EquipmentName = EquipmentType.Laptop,
                IsAvaliable = true,
                IsWorking = true
            };

            equipmentRepository.Create(equipmentToUpdate);
            equipmentRepository.Create(equipmentToGet);

        }

        [TearDown]
        public void TearDown()
        {
            equipmentRepository.Delete(equipmentToUpdate);
            equipmentRepository.Delete(equipmentToGet);
        }

        [Test]
        public void InsertEquipment_ToDatabase_PerRequest_Success()
        {
            equipmentRepository.Create(equipmentToCreate);

            Assert.IsNotNull(equipmentRepository.GetEquipmentById(equipmentToCreate.Id));

            equipmentRepository.Delete(equipmentToCreate);
        }

        [Test]
        public void UpdateEquipment_FromDatabase_PerRequest_Success()
        {
            equipmentToUpdate.EquipmentName = EquipmentType.Laptop;
            equipmentToUpdate.IsAvaliable = false;
            equipmentToUpdate.IsWorking = true;
            equipmentRepository.Update(equipmentToUpdate);

            var updated = equipmentRepository.GetEquipmentById(equipmentToUpdate.Id);
            Assert.IsNotNull(updated);
            Assert.AreEqual(EquipmentType.Laptop, updated.EquipmentName);
            Assert.IsFalse(updated.IsAvaliable);
            Assert.IsTrue(updated.IsWorking);

        }

        [Test]
        public void DeleteEquipment_FromDatabase_PerRequest_Success()
        {
            equipmentRepository.Create(equipmentToDelete);

            Assert.IsTrue(equipmentRepository.Delete(equipmentToDelete));

            Assert.IsNull(equipmentRepository.GetEquipmentById(equipmentToDelete.Id));
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
        public void GetEquipments()
        {
            var equipments = equipmentRepository.GetEquipments();
            Assert.IsNotNull(equipments);
            Assert.IsNotEmpty(equipments);
            Assert.IsInstanceOf(typeof(List<Equipment>), equipments);
        }
    }
}
