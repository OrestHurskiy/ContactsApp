using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;

namespace DataLayer.Wrapper
{
    public class ContextManager:IContextManager
    {
        public bool ContextTypeSingleton { get; private set; }

        public ContextManager(bool contextTypeSingleton=true)
        {
            ContextTypeSingleton = contextTypeSingleton;
        }
        private ContactsContext _singletonContext;

        public ContactsContext CurrentContext
        {
            get
            {
                if (ContextTypeSingleton)
                {
                    return _singletonContext ?? (_singletonContext = new ContactsContext());
                }
                else
                {
                    return new ContactsContext();
                }
            }
        }
        private void SaveContext(ContactsContext context)
        {
            if (ContextTypeSingleton) return;
            try
            {
                context.SaveChanges();
            }
            finally
            {
                context.Dispose();
            }
        }

        public void Save(ContactsContext context)
        {
            SaveContext(context);
        }

        public void BatchSave()
        {
            if(!ContextTypeSingleton)return;
            ContextTypeSingleton = false;
            SaveContext(_singletonContext);
            _singletonContext = null;
        }

       
    }
    
}
