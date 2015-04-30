using DataLayer.Context;

namespace DataLayer
{
    public interface IContextManager
    {
        bool ContextTypeSingleton { get; }
        void Save(ContactsContext context);
        ContactsContext CurrentContext { get; }
        void BatchSave();
    }
}
