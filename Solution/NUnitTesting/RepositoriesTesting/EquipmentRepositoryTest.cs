using NUnit.Framework;
using DataLayer.Components;
using DataLayer.Wrapper;
using System.Collections.Generic;

namespace NUnitTesting.RepositoriesTesting
{
   [TestFixture]
    public class EquipmentRepositoryTest
    {
        private IContextManager contextManager;
        private IEquipmentRepository equipmentRepository;

        [SetUp]
        public void SetUp()
        {
            contextManager = new ContextManager();
            equipmentRepository = new EquipmentRepository(contextManager);
        }

        [Test]
        public void CreateEquipment()
        {
            var equipment = new Equipment {EquipmentName = EquipmentType.Laptop, IsAvaliable = true, IsWorking = true};
            var equipment1 = new Equipment {EquipmentName = EquipmentType.Phone, IsAvaliable = true, IsWorking = false};
            Assert.That(equipment,!Is.Null);
            Assert.That(equipment1,!Is.Null);
            equipmentRepository.Create(equipment);
            equipmentRepository.Create(equipment1);
            contextManager.BatchSave();

            Assert.That(equipment,!Is.Null);
            Assert.That(equipment.Id,!Is.NaN);
            Assert.That(equipment.Id,Is.Positive);
            Assert.IsInstanceOf(typeof(int), equipment.Id);

            Assert.That(equipment1, !Is.Null);
            Assert.That(equipment1.Id, !Is.NaN);
            Assert.That(equipment1.Id, Is.Positive);
            Assert.IsInstanceOf(typeof(int), equipment1.Id);
            
        }

        [Test]
        public void UpdateEquipment()
        {
            var equipmentUp1 = equipmentRepository.GetEquipmentById(2);
            var equipmentUp2 = equipmentRepository.GetEquipmentByEquipmentType(EquipmentType.Laptop);
            var equipmentUp3 = equipmentRepository.GetEquipmentById(3);
            Assert.That(equipmentUp1,!Is.Null);
            Assert.That(equipmentUp2, !Is.Null);
            Assert.That(equipmentUp3, !Is.Null);

            equipmentUp1.IsAvaliable = false;
            equipmentUp2.EquipmentName = EquipmentType.Table;
            equipmentUp3.IsWorking = true;
            equipmentRepository.Update(equipmentUp1);
            equipmentRepository.Update(equipmentUp2);
            equipmentRepository.Update(equipmentUp3);
            contextManager.BatchSave();

            Assert.That(equipmentUp1,!Is.Null);
            Assert.That(equipmentUp2, !Is.Null);
            Assert.That(equipmentUp3, !Is.Null);
            Assert.IsFalse(equipmentUp1.IsAvaliable);
            Assert.AreEqual(equipmentUp2.EquipmentName,EquipmentType.Table);
            Assert.IsTrue(equipmentUp3.IsWorking);


        }

        [Test]
        public void DeleteEquipment()
        {
            var equipmentTodel = equipmentRepository.GetEquipmentById(2);
            var equipmentTodel1 = equipmentRepository.GetEquipmentByEquipmentType(EquipmentType.Table);

            Assert.That(equipmentTodel,!Is.Null);
            Assert.That(equipmentTodel1,!Is.Null);

            Assert.IsTrue(equipmentRepository.Delete(equipmentTodel), "Something go wrong");
            Assert.IsTrue(equipmentRepository.Delete(equipmentTodel1), "Something go wrong");
            contextManager.BatchSave();
            
        }

        [Test]
        public void GetEquipmentById()
        {
            var equipment = equipmentRepository.GetEquipmentById(4);
            Assert.That(equipment,!Is.Null);
            Assert.AreEqual(equipment.Id,4);
            Assert.AreEqual(equipment.EquipmentName,EquipmentType.Laptop);
            Assert.IsTrue(equipment.IsAvaliable);
            Assert.IsTrue(equipment.IsWorking);
            
            var equipment1 = equipmentRepository.GetEquipmentById(5);
            Assert.That(equipment1, !Is.Null);
            Assert.AreEqual(equipment1.Id, 5);
            Assert.AreEqual(equipment1.EquipmentName, EquipmentType.Phone);
            Assert.IsTrue(equipment1.IsAvaliable);
            Assert.IsFalse(equipment1.IsWorking);

        }

        [Test]
        public void GetEquipmentByEquipmentType()
        {
            var equipment = equipmentRepository.GetEquipmentByEquipmentType(EquipmentType.Phone);
            Assert.That(equipment, !Is.Null);
            Assert.AreEqual(equipment.Id, 3);
            Assert.AreEqual(equipment.EquipmentName, EquipmentType.Phone);
            Assert.IsTrue(equipment.IsAvaliable);
            Assert.IsTrue(equipment.IsWorking);

            var equipment1 = equipmentRepository.GetEquipmentByEquipmentType(EquipmentType.Laptop);
            Assert.That(equipment1, !Is.Null);
            Assert.AreEqual(equipment1.Id, 4);
            Assert.AreEqual(equipment1.EquipmentName, EquipmentType.Laptop);
            Assert.IsTrue(equipment1.IsAvaliable);
            Assert.IsTrue(equipment1.IsWorking);
        }

        [Test]
        public void GetEquipments()
        {
            var equipments = equipmentRepository.GetEquipments();
            Assert.That(equipments, !Is.Null);
            Assert.IsNotEmpty(equipments);
            Assert.IsInstanceOf(typeof(List<Equipment>), equipments);
        }
    }
}

