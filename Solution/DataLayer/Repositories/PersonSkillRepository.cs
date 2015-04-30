using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Repositories.Interfaces;
using Models.Entities;

namespace DataLayer.Repositories
{
    public class PersonSkillRepository:IPersonSkillRepository
    {
        private readonly IContextManager contextManager;

        public PersonSkillRepository(IContextManager contextManager)
        {
            this.contextManager = contextManager;
        }

        public void CreatePersonWithSkill(Person person, Skill skill)
        {
            if (person == null || skill == null) return;
            var context = contextManager.CurrentContext;
            context.Persons.Add(person);
            context.Skills.Add(skill);
            person.Skills.Add(skill);
            contextManager.Save(context);
        }

        public void AttachPersonToSkill(Person person, Skill skill)
        {
            if (person == null || skill == null) return;
            var context = contextManager.CurrentContext;
            context.Entry(person).State = EntityState.Modified;
            context.Entry(skill).State = EntityState.Modified;
            person.Skills.Add(skill);
            contextManager.Save(context);
        }

        public bool DeletePersonWithSkill(Person person, Skill skill)
        {
            if (person == null || skill == null) return false;
            var context = contextManager.CurrentContext;
            context.Entry(person).State = EntityState.Modified;
            context.Entry(skill).State = EntityState.Modified;
            person.Skills.Remove(skill);
            contextManager.Save(context);
            return true;
        }

        public IList<Person> GetPersonsBySkillId(int skillId)
        {
            return
                contextManager.CurrentContext.Skills.AsNoTracking()
                    .Where(s => s.Id == skillId)
                    .SelectMany(p => p.Persons).ToList();
        }

        public IList<Skill> GetSkillsByPersonId(int personId)
        {
            return
                contextManager.CurrentContext.Persons.AsNoTracking()
                    .Where(p => p.Id == personId)
                    .SelectMany(s => s.Skills)
                    .ToList();
        }

    }
}
