using System.Collections.Generic;
using DataLayer.Components;

namespace DataLayer.Wrapper
{
    public interface ISkillRepository
    {
        void Create(Skill skill);
        void Update(Skill skill);
        bool Delete(Skill skill);
        Skill GetSkillById(int skillId);
        Skill GetSkillByDevelopment(string development);
        Skill GetSkillByCertification(string certification);
        Skill GetSkillByDegree(Degree degree);
        IList<Skill> GetSkills();
    }
}
