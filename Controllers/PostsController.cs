using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BoardWebAPIServer.Models;
using BoardWebAPIServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoardWebAPIServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postSvc;
        private readonly IUserService _userSvc;

        public PostsController(IPostService postSvc, IUserService userSvc)
        {
            _postSvc = postSvc;
            _userSvc = userSvc;
        }

        [HttpPost("create")]
        public IActionResult Create(CreatePostIn postIn)
        {
            if (_postSvc.Create(postIn, out var post) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Create Post",
                });
            }

            return Ok(new
            {
                success = true,
                post,
            });
        }

        [HttpGet("read/{id}")]
        public IActionResult Read(string id)
        {
            if (_postSvc.Read(id, out var post) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Read Post",
                });
            }

            return Ok(new
            {
                success = true,
                post,
            });
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, UpdatePostIn postIn)
        {
            if (_postSvc.Update(id, postIn) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Update Post",
                });
            }

            return Ok(new
            {
                success = true,
            });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            if (_postSvc.Delete(id) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Delete Post",
                });
            }

            return Ok(new
            {
                success = true,
            });
        }

        [HttpGet("list")]
        public IActionResult List(int page, int itemsPerPage)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (itemsPerPage == 0)
            {
                itemsPerPage = 30;
            }

            if (_postSvc.List(page, itemsPerPage, out var pageCount, out var posts) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to List Post",
                });
            }

            return Ok(new
            {
                success = true,
                page = page,
                page_count = pageCount,
                posts,
            });
        }

        [HttpGet("search")]
        public IActionResult Search(int page, int itemsPerPage, SearchPostIn postIn)
        {
            return Ok(new
            {
                success = true,
                reason = "Failed to Search Post, Not Implemented Yet",
            });
        }

        [HttpPost("like")]
        public IActionResult Like(LikeIn likeIn)
        {
            if (_postSvc.Like(likeIn.PostId, likeIn.UserId) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Like Post",
                });
            }

            return Ok(new
            {
                success = true,
            });
        }

        [HttpPost("unlike")]
        public IActionResult Unlike(LikeIn likeIn)
        {
            if (_postSvc.Unlike(likeIn.PostId, likeIn.UserId) == false)
            {
                return Ok(new
                {
                    success = true,
                    reason = "Failed to Unlike Post",
                });
            }

            return Ok(new
            {
                success = true,
            });
        }
    }
}
