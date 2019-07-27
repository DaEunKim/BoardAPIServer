using System.Collections.Generic;
using BoardWebAPIServer.Models;

namespace BoardWebAPIServer.Services
{
    public interface IPostService
    {
        bool Create(CreatePostIn postIn, out Post created);
        bool Delete(string id);
        bool Like(string id, string userId);
        bool List(int page, int itemsPerPage, out int pageCount, out ICollection<Post> listed);
        bool Read(string id, out Post read);
        bool Search(int page, int itemsPerPage, SearchPostIn postIn, out int pageCount, out ICollection<Post> searched);
        bool Unlike(string id, string userId);
        bool Update(string id, UpdatePostIn postIn);
    }
}