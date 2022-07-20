using JwtTokenTest.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    internal interface IDataProvider
    {
        void AddUser(Person person);
        void EditUser(Person person);
        void DeleteUser(Person person);
    }
}
