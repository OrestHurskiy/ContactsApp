using System.Collections.Generic;
using Models.Entities;

namespace DataLayer.Repositories.Interfaces
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
