using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BoardWebAPIServer.Models;
using BoardWebAPIServer.Services;
using MongoDB.Driver;

namespace BoardWebAPIServer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository()
        {
            var connStr = ConnectionInfo.GetDbConnectionString();
            var client = new MongoClient(connStr);
            var boardDb = client.GetDatabase("BoardDb");

            _users = boardDb.GetCollection<User>("Users");
        }

        public bool Create(User userIn, out User created)
        {
            created = null;

            if (userIn == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(userIn.Id))
            {
                return false;
            }

            var count = _users
                .Find(u => u.Id == userIn.Id)
                .CountDocuments();
            if (count != 0)
            {
                return false;
            }

            try
            {
                _users.InsertOne(userIn);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            created = userIn;
            return true;
        }

        public bool Read(string id, out User read)
        {
            read = null;

            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            read = _users
                .Find(u => u.Id == id)
                .SingleOrDefault();
            if (read == null)
            {
                return false;
            }

            return true;
        }

        public bool Update(string id, User userIn)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            if (userIn == null)
            {
                return false;
            }

            if (id != userIn.Id)
            {
                return false;
            }

            _users.ReplaceOne(u => u.Id == id, userIn);
            // @TODO : Result Check

            return true;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            _users.DeleteOne(p => p.Id == id);
            // @TODO : Result Check

            return true;
        }

        public bool List(int skip, int limit, out ICollection<User> listed)
        {
            listed = null;

            if (skip < 0 || limit < 0)
            {
                return false;
            }

            listed = _users
                        .Find(_ => true)
                        .Skip(skip)
                        .Limit(limit)
                        .ToList();

            return true;
        }

        public bool Search(int skip, int limit, FilterDefinition<User> filter, out ICollection<User> searched)
        {
            searched = null;

            if (skip < 0 || limit < 0)
            {
                return false;
            }

            searched = _users
                        .Find(filter)
                        .Skip(skip)
                        .Limit(limit)
                        .ToList();

            return true;
        }
    }
}
