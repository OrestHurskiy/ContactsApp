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
    #region CustomerDataComponent Interface

    public interface ICustomerDataComponent
    {
        #region ICustomerDataComponent Methods

        Customer CreateCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByName(string name);
        Customer GetCustomerByMail(string mail);
        List<Project> GetProjectsOfCustomer(Customer customer);

        #endregion
    }

        #endregion
}
