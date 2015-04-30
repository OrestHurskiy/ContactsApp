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
    #region Skill Interface

    public interface ISkillDataComponent
    {
        #region ISkillDataComponent Methods

        Skill CreateSkill(Skill skill);
        Skill UpdateSkill(Skill skill);
        bool DeleteSkill(Skill skill);
        Skill GetSkillByDevelopment(string development);
        Skill GetSkillByCertification(string certification);
        Skill GetSkillById(int id);
        List<Skill> GetSkills();

        #endregion
    }
    #endregion
}
