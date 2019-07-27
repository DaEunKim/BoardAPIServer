using System.Collections.Generic;
using BoardWebAPIServer.Models;
using MongoDB.Driver;

namespace BoardWebAPIServer.Repository
{
    public interface IPostRepository
    {
        bool Create(Post postIn, out Post created);
        bool Delete(string id);
        bool List(int skip, int limit, out ICollection<Post> listed);
        bool Read(string id, out Post read);
        bool Search(int skip, int limit, FilterDefinition<Post> filter, out ICollection<Post> searched);
        bool Update(string id, Post postIn);
        long Count();
    }
}