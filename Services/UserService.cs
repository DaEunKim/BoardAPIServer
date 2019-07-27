using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BoardWebAPIServer.Repository;
using BoardWebAPIServer.Models;

namespace BoardWebAPIServer.Services
{
    public class UserService : IUserService
    {
        private readonly IPostRepository _postRepo;
        private readonly IUserRepository _userRepo;

        public UserService(IPostRepository postRepo, IUserRepository userRepo)
        {
            _postRepo = postRepo;
            _userRepo = userRepo;
        }

        public bool Create(CreateUserIn userIn, out User created)
        {
            created = null;

            {
                if (userIn == null)
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(userIn.Id))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(userIn.Password))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(userIn.Name))
                {
                    return false;
                }

                // Duplicated User Id
                if (_userRepo.Read(userIn.Id, out var _))
                {
                    return false;
                }

                // Email is Optional Field
            }

            User user = new User
            {
                Id = userIn.Id,
                Password = userIn.Password,
                Name = userIn.Name,
                Email = userIn.Email,
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            if (_userRepo.Create(user, out created) == false)
            {
                return false;
            }

            return true;
        }

        public bool Read(string id, out User read)
        {
            read = null;

            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            if (_userRepo.Read(id, out read) == false)
            {
                return false;
            }

            return true;
        }

        public bool Update(string id, UpdateUserIn userIn)
        {
            User user = null;
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

                if (_userRepo.Read(id, out user) == false)
                {
                    return false;
                }
            }

            user.Password = userIn.Password;
            user.Name = userIn.Name;
            user.Email = userIn.Email;

            if (_userRepo.Update(id, user) == false)
            {
                return false;
            }

            return true;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            if (_userRepo.Delete(id) == false)
            {
                return false;
            }

            return true;
        }

        public bool List(out ICollection<User> listed)
        {
            return _userRepo.List(0, 100, out listed);
        }

        public bool AddBookMark(string id, string postId)
        {
            User user = null;
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    return false;
                }

                if (_userRepo.Read(id, out user) == false)
                {
                    return false;
                }

                // Already Added
                if (user.BookMarkPostIds != null && user.BookMarkPostIds.Contains(postId))
                {
                    return false;
                }

                if (_postRepo.Read(postId, out var _) == false)
                {
                    return false;
                }
            }

            if (user.BookMarkPostIds == null)
            {
                user.BookMarkPostIds = new List<string>();
            }

            user.BookMarkPostIds.Add(postId);

            if (_userRepo.Update(id, user) == false)
            {
                return false;
            }

            return true;
        }

        public bool RemoveBookMark(string id, string postId)
        {
            User user = null;
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(postId))
                {
                    return false;
                }

                if (_userRepo.Read(id, out user) == false)
                {
                    return false;
                }

                // Already Removed
                if (user.BookMarkPostIds == null || user.BookMarkPostIds.Contains(postId) == false)
                {
                    return false;
                }

                if (_postRepo.Read(postId, out var _) == false)
                {
                    return false;
                }
            }

            if (user.BookMarkPostIds.Remove(postId) == false)
            {
                return false;
            }

            if (_userRepo.Update(id, user) == false)
            {
                return false;
            }

            return true;
        }
    }
}
