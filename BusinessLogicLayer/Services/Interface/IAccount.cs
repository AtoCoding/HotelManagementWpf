using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IAccount<T>
    {
        (bool isAuthen, string role) CheckAuth(string email, string password);
    }
}
