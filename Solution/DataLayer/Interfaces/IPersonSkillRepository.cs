using System.Collections.Generic;
using DataLayer.Components;

namespace DataLayer.Wrapper
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
