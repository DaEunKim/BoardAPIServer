using System.Collections.Generic;
using BoardWebAPIServer.Models;
using MongoDB.Driver;

namespace BoardWebAPIServer.Repository
{
    public interface IUserRepository
    {
        bool Create(User userIn, out User created);
        bool Delete(string id);
        bool List(int skip, int limit, out ICollection<User> listed);
        bool Read(string id, out User read);
        bool Search(int skip, int limit, FilterDefinition<User> filter, out ICollection<User> searched);
        bool Update(string id, User userIn);
    }
}