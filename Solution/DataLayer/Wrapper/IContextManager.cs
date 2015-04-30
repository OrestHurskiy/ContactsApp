using System;
using System.Security.Cryptography.X509Certificates;
using DataLayer.Context;

namespace DataLayer.Wrapper
{
    public interface IContextManager
    {
        bool ContextTypeSingleton { get; }
        void Save(ContactsContext context);
        ContactsContext CurrentContext { get; }
        void BatchSave();
    }
}
