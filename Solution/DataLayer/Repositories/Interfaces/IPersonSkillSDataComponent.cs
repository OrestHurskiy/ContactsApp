#region UsedNamespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Components;
#endregion

namespace DataLayer.Interfaces
{
    #region PersonSkillsDataComponent Interface

    public interface IPersonSkillsDataComponent
    {
        #region IPersonSkillsDataComponent Methods

        List<Skill> GetSkillsByPersonId(int personId);
        List<Person> GetPersonsBySkillId(int skillId);
        void CreatePersonWithSkill(Person person, Skill skill);
        void DetelePersonWithSkill(Person person, Skill skill);
        void UpdatePersonWithSkill(Person person, Skill skill);

        #endregion

    }

    #endregion
}
