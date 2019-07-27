using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace BoardWebAPIServer.Models
{
    public class CreatePostIn
    {
        public string Title { get; set; }
        public string CreatorId { get; set; }
        public string Text { get; set; }
        public ICollection<string> Tags { get; set; }

        // Anonymous Mode
        public bool IsAnonymous { get; set; }
        public string Password { get; set; }
    }

    public class UpdatePostIn
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ICollection<string> Tags { get; set; }

        public string CreatorId { get; set; }
        public string Password { get; set; }
    }

    public class SearchPostIn
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CreatorId { get; set; }
        public string Text { get; set; }
        public ICollection<string> Tags { get; set; }
    }

    public class LikeIn
    {
        public string UserId { get; set; }
        public string PostId { get; set; }
    }

    public class Post
    {
        //public void CopyFrom(CreatePostIn post)
        //{
        //    Title = post.Title;
        //    CreatorId = post.CreatorId;
        //    Text = post.Text;
        //    Tags = post.Tags;
        //}

        [BsonId]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("creator_id")]
        public string CreatorId { get; set; }

        [BsonElement("create_time")]
        public string CreateTime { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("view_count")]
        public int ViewCount { get; set; }

        [BsonElement("like_count")]
        public int LikeCount { get; set; }

        [BsonElement("tags")]
        public ICollection<string> Tags { get; set; }

        [BsonElement("comments")]
        public ICollection<Comment> Comments { get; set; }

        // Anonymous Mode
        [BsonElement("is_anonymous")]
        public bool IsAnonymous { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }

    public class Comment
    {
        //public Comment()
        //{
        //    SubComments = new List<Comment>();
        //}

        [BsonId]
        public string Id { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("creator_id")]
        public string CreatorId { get; set; }

        [BsonElement("create_time")]
        public string CreateTime { get; set; }

        [BsonElement("sub_comments")]
        public ICollection<Comment> SubComments { get; set; }
    }
}
