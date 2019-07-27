using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BoardWebAPIServer.Repository;
using BoardWebAPIServer.Models;

namespace BoardWebAPIServer.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepo;
        private readonly IUserRepository _userRepo;

        public PostService(IPostRepository postRepo, IUserRepository userRepo)
        {
            _postRepo = postRepo;
            _userRepo = userRepo;
        }

        public bool Create(CreatePostIn postIn, out Post created)
        {
            created = null;

            // Validation
            {
                if (postIn == null)
                {
                    return false;
                    throw new Exception("Input is Null");
                }

                if (string.IsNullOrWhiteSpace(postIn.Title))
                {
                    return false;
                    throw new Exception("Title is Null or Empty");
                }

                if (string.IsNullOrWhiteSpace(postIn.CreatorId))
                {
                    return false;
                    throw new Exception("Creator Id is Null or Empty");
                }

                if (string.IsNullOrWhiteSpace(postIn.Text))
                {
                    return false;
                    throw new Exception("Text is Null or Empty");
                }

                if (postIn.IsAnonymous)
                {
                    if (string.IsNullOrWhiteSpace(postIn.Password))
                    {
                        return false;
                    }
                }
                else
                {
                    if (_postRepo.Read(postIn.CreatorId, out var read) == false)
                    {
                        return false;
                        throw new Exception(string.Format("Failed to Find User ({0})", postIn.CreatorId));
                    }

                    // 본인확인 - session?
                }
            }

            var postId = Guid.NewGuid().ToString();

            Post post = new Post
            {
                Id = postId,
                Title = postIn.Title,
                CreatorId = postIn.CreatorId,
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Text = postIn.Text,
                ViewCount = 0,
                LikeCount = 0,
                Tags = postIn.Tags,
                Comments = new List<Comment>(),
                IsAnonymous = postIn.IsAnonymous,
                Password = postIn.Password,
            };

            if (_postRepo.Create(post, out created) == false)
            {
                return false;
                throw new Exception("Failed to Create Post");
            }

            return true;
        }

        public bool Read(string id, out Post read)
        {
            read = null;

            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return false;
                }
            }

            if (_postRepo.Read(id, out read) == false)
            {
                return false;
            }

            ++read.ViewCount;
            if (_postRepo.Update(id, read) == false)
            {
                return true; // viewcount update 실패해도 ok
            }

            return true;
        }

        public bool Update(string id, UpdatePostIn postIn)
        {
            Post read = null;
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return false;
                }

                if (postIn == null)
                {
                    return false;
                }

                if (id == postIn.Id)
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(postIn.Title))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(postIn.Text))
                {
                    return false;
                }

                if (_postRepo.Read(id, out read) == false)
                {
                    return false;
                }

                if (read.IsAnonymous)
                {
                    if (postIn.Password != read.Password)
                    {
                        return false;
                    }
                }
                else
                {
                    // 본인 확인 - session?
                }
            }

            read.Title = postIn.Title;
            read.Text = postIn.Text;
            read.Tags = postIn.Tags;

            if (_postRepo.Update(id, read) == false)
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

            if (_postRepo.Delete(id) == false)
            {
                return false;
            }

            return true;
        }

        public bool List(int page, int itemsPerPage, out int pageCount, out ICollection<Post> listed)
        {
            {
                if (page < 1)
                {
                    page = 1;
                }

                if (itemsPerPage <= 0)
                {
                    itemsPerPage = 30;
                }

                var postCount = _postRepo.Count();
                pageCount = (int)(postCount / itemsPerPage);
                if (postCount % itemsPerPage != 0)
                {
                    ++pageCount;
                }
            }

            if (_postRepo.List(itemsPerPage * (page - 1), itemsPerPage, out listed) == false)
            {
                return false;
            }

            return true;
        }

        public bool Search(int page, int itemsPerPage, SearchPostIn postIn, out int pageCount, out ICollection<Post> searched)
        {
            pageCount = 0;
            searched = null;

            //if (page < 1)
            //{
            //    page = 1;
            //}

            //if (postIn == null)
            //{
            //    return List(page, itemsPerPage, out searched);
            //}

            //// Search Each Property
            //{
            //    searched = null;



            //    // Search by Id
            //    if (string.IsNullOrWhiteSpace(postIn.Id) == false)
            //    {
            //        if (Read(postIn.Id, out Post read) == true)
            //        {
            //            searched.Add(read);
            //        }

            //        return true; // When Search Context, Not Found Post -> Ok 
            //    }

            //    // Search by Title
            //    if (string.IsNullOrWhiteSpace(postIn.Title) == false)
            //    {
            //        searched = _posts
            //                    .Find(p => p.Title.Contains(postIn.Title))
            //                    .ToList();

            //        return true;
            //    }

            //    if (string.IsNullOrWhiteSpace(postIn.CreatorId) == false)
            //    {
            //        searched = _posts
            //            .Find(p => p.CreatorId == postIn.CreatorId)
            //            .ToList();

            //        return true;
            //    }

            //    if (string.IsNullOrWhiteSpace(postIn.Text) == false)
            //    {
            //        searched = _posts
            //            .Find(p => p.Text == postIn.Text)
            //            .ToList();

            //        return true;
            //    }

            //    if (postIn.Tags != null && postIn.Tags.Count() > 0)
            //    {
            //        var first = postIn.Tags.First();
            //        searched = _posts
            //            .Find(p => p.Tags.Contains(first))
            //            .ToList();

            //        // @TODO : Multiple Tag Search

            //        return true;
            //    }
            //}

            return true;
        }

        public bool Like(string id, string userId)
        {
            Post post = null;
            User user = null;
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(userId))
                {
                    return false;
                }

                if (_userRepo.Read(userId, out user) == false)
                {
                    return false;
                }

                // Already Like
                if (user.LikePostIds != null && user.LikePostIds.Contains(id))
                {
                    return false;
                }

                if (_postRepo.Read(id, out post) == false)
                {
                    return false;
                }
            }

            if (user.LikePostIds == null)
            {
                user.LikePostIds = new List<string>();
            }

            ++post.LikeCount;
            user.LikePostIds.Add(id);

            if (_postRepo.Update(id, post) == false)
            {
                return false;
            }

            if (_userRepo.Update(userId, user) == false)
            {
                return false;
            }

            return true;
        }

        public bool Unlike(string id, string userId)
        {
            Post post = null;
            User user = null;
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(userId))
                {
                    return false;
                }

                if (_userRepo.Read(userId, out user) == false)
                {
                    return false;
                }

                // Already Unlike
                if (user.LikePostIds == null || user.LikePostIds.Contains(id) == false)
                {
                    return false;
                }

                if (_postRepo.Read(id, out post) == false)
                {
                    return false;
                }
            }

            --post.LikeCount;
            if (user.LikePostIds.Remove(id) == false)
            {
                return false;
            }

            if (_postRepo.Update(id, post) == false)
            {
                return false;
            }

            if (_userRepo.Update(userId, user) == false)
            {
                return false;
            }

            return true;
        }
    }
}
