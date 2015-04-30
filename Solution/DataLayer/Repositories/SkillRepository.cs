using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Repositories.Interfaces;
using Models.Entities;

namespace DataLayer.Repositories
{
    public class SkillRepository:ISkillRepository
    {
        private readonly IContextManager contextManager;

        public SkillRepository(IContextManager contextManager)
        {
            this.contextManager = contextManager;
        }

        public void Create(Skill skill)
        {
            if (skill == null) return;
            var context = contextManager.CurrentContext;
            context.Skills.Add(skill);
            contextManager.Save(context);
        }

        public void Update(Skill skill)
        {
            if (skill == null) return;
            var context = contextManager.CurrentContext;
            context.Entry(skill).State = EntityState.Modified;
            contextManager.Save(context);
        }

        public bool Delete(Skill skill)
        {
            if (skill == null) return false;
            var context = contextManager.CurrentContext;
            context.Entry(skill).State = EntityState.Deleted;
            contextManager.Save(context);
            return true;
        }

        public Skill GetSkillById(int skillId)
        {
            return contextManager.CurrentContext.Skills.AsNoTracking().FirstOrDefault(s => s.Id == skillId);
        }

        public Skill GetSkillByDevelopment(string development)
        {
            return contextManager.CurrentContext.Skills.AsNoTracking().FirstOrDefault(s => s.Development == development);
        }

        public Skill GetSkillByCertification(string certification)
        {
            return contextManager.CurrentContext.Skills.AsNoTracking().FirstOrDefault(s => s.Certification == certification);
        }

        public Skill GetSkillByDegree(Degree degree)
        {
            return contextManager.CurrentContext.Skills.AsNoTracking().FirstOrDefault(s => s.Degree == degree);
        }

        public IList<Skill> GetSkills()
        {
            return contextManager.CurrentContext.Skills.AsNoTracking().ToList();
        }
    }
}
