using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interface
{
    public interface IRepository<T>
    {
        T Add(T data);
        T? Update(T data);
        bool Delete(int id);
        T? Get(int id);
        List<T> GetAll();
        int Count();
        List<T> Search(string? description, string? typeName, int capacity);
        List<T> Search(string? fullName, string? telephone, string? emailAddress);
    }
}
