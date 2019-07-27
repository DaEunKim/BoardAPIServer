using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BoardWebAPIServer.Models;
using BoardWebAPIServer.Services;
using MongoDB.Driver;

namespace BoardWebAPIServer.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> _posts;

        public PostRepository()
        {
            var connStr = ConnectionInfo.GetDbConnectionString();
            var client = new MongoClient(connStr);
            var boardDb = client.GetDatabase("BoardDb");

            _posts = boardDb.GetCollection<Post>("Posts");
        }

        public bool Create(Post postIn, out Post created)
        {
            created = null;

            if (postIn == null)
            {
                return false;
            }

            var postId = Guid.NewGuid().ToString();

            postIn.Id = postId;
            //postIn.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                _posts.InsertOne(postIn);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            created = postIn;
            return true;
        }

        public bool Read(string id, out Post read)
        {
            read = null;

            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            read = _posts
                .Find(p => p.Id == id)
                .SingleOrDefault();
            if (read == null)
            {
                return false;
            }

            ++read.ViewCount;
            _posts.ReplaceOne(p => p.Id == id, read);

            return true;
        }

        public bool Update(string id, Post postIn)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            if (postIn == null)
            {
                return false;
            }

            if (id != postIn.Id)
            {
                return false;
            }

            _posts.ReplaceOne(p => p.Id == id, postIn);
            // @TODO : Result Check

            return true;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            _posts.DeleteOne(p => p.Id == id);
            // @TODO : Result Check

            return true;
        }

        public bool List(int skip, int limit, out ICollection<Post> listed)
        {
            listed = null;

            if (skip < 0 || limit < 0)
            {
                return false;
            }

            listed = _posts
                        .Find(_ => true)
                        .Skip(skip)
                        .Limit(limit)
                        .ToList();

            return true;
        }

        public bool Search(int skip, int limit, FilterDefinition<Post> filter, out ICollection<Post> searched)
        {
            searched = null;

            if (skip < 0 || limit < 0)
            {
                return false;
            }

            searched = _posts
                        .Find(filter)
                        .Skip(skip)
                        .Limit(limit)
                        .ToList();

            return true;
        }

        public long Count()
        {
            return _posts.CountDocuments(_ => true);
        }
    }
}
