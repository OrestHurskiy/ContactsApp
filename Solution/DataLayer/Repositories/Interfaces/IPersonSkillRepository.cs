using System.Collections.Generic;
using Models.Entities;

namespace DataLayer.Repositories.Interfaces
{
    public interface IPersonSkillRepository
    {
        void CreatePersonWithSkill(Person person, Skill skill);
        void AttachPersonToSkill(Person person, Skill skill);
        bool DeletePersonWithSkill(Person person, Skill skill);
        IList<Person> GetPersonsBySkillId(int skillId);
        IList<Skill> GetSkillsByPersonId(int personId);
    }
}
