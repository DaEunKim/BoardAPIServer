using BoardWebAPIServer.Models;
using System.Collections.Generic;

namespace BoardWebAPIServer.Services
{
    public interface IUserService
    {
        bool AddBookMark(string id, string postId);
        bool Create(CreateUserIn userIn, out User created);
        bool Delete(string id);
        bool Read(string id, out User read);
        bool RemoveBookMark(string id, string postId);
        bool Update(string id, UpdateUserIn userIn);
        bool List(out ICollection<User> listed);
    }
}