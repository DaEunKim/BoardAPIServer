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
    public class UsersController : ControllerBase
    {
        private readonly IPostService _postSvc;
        private readonly IUserService _userSvc;

        public UsersController(IPostService postSvc, IUserService userSvc)
        {
            _postSvc = postSvc;
            _userSvc = userSvc;
        }

        [HttpPost("create")]
        public IActionResult Create(CreateUserIn userIn)
        {
            if (_userSvc.Create(userIn, out var user) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Create User",
                });
            }

            return Ok(new
            {
                success = true,
                user,
            });
        }

        [HttpGet("read/{id}")]
        public IActionResult Read(string id)
        {
            if (_userSvc.Read(id, out var user) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Read User",
                });
            }

            return Ok(new
            {
                success = true,
                user,
            });
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, UpdateUserIn userIn)
        {
            if (_userSvc.Update(id, userIn) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Update User",
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
            if (_userSvc.Delete(id) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Delete User",
                });
            }

            return Ok(new
            {
                success = true,
            });
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            if (_userSvc.List(out var users) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to List User",
                });
            }

            return Ok(new
            {
                success = true,
                users
            });
        }

        [HttpPost("addbookmark")]
        public IActionResult AddBookMark(BookMarkIn bookmarkIn)
        {
            if (_userSvc.AddBookMark(bookmarkIn.UserId, bookmarkIn.PostId) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Add BookMark Post"
                });
            }

            return Ok(new
            {
                success = true,
            });
        }

        [HttpPost("removebookmark")]
        public IActionResult RemoveBookMark(BookMarkIn bookmarkIn)
        {
            if (_userSvc.RemoveBookMark(bookmarkIn.UserId, bookmarkIn.PostId) == false)
            {
                return Ok(new
                {
                    success = false,
                    reason = "Failed to Delete BookMark Post"
                });
            }

            return Ok(new
            {
                success = true,
            });
        }
    }
}
