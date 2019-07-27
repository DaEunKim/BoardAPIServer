using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson.Serialization.Attributes;

namespace BoardWebAPIServer.Models
{
    public class CreateUserIn
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserIn
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class SearchUserIn
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class BookMarkIn
    {
        public string UserId { get; set; }
        public string PostId { get; set; }
    }

    public class User
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("create_time")]
        public string CreateTime { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("bookmark_post_ids")]
        public ICollection<string> BookMarkPostIds { get; set; }

        [BsonElement("like_post_ids")]
        public ICollection<string> LikePostIds { get; set; }
    }
}
