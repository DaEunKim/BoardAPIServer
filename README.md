# Board API

## Post

### Create Post
* [POST] http://lunahc92.tplinkdns.com:5100/api/posts/create
```
{
	"title" : "This is Title",
	"creatorId" : "CreatorID",
	"text" : "This is Text",
	"tags" : [ "This", "is", "Tag" ],
	"isAnonymous" : true,
	"password" : "1234"
}
```

### Read Post
* [GET] http://lunahc92.tplinkdns.com:5100/api/posts/read/{id}

### Update Post
* [PUT] http://lunahc92.tplinkdns.com:5100/api/posts/update/{id}
```
{
  "id" : "generated post id",
  "title" : "This is Title 2",
  "text" : "This is Text 2",
  "tags" : [ "This", "is", "Tag", "2" ],
  "password" : "1234"
}
```

### Delete Post
* [DELETE] http://lunahc92.tplinkdns.com:5100/api/posts/delete/{id}

### List Post
* [GET] http://lunahc92.tplinkdns.com:5100/api/posts/list?page={page#}

### Like Post
* [POST] http://lunahc92.tplinkdns.com:5100/api/posts/like
```
{
  "userId" : "lunahc92", // must create user first
  "postId" : "generated post id"
}
```

### UnLike Post
* [POST] http://lunahc92.tplinkdns.com:5100/api/posts/unlike
```
{
  "userId" : "lunahc92", // must create user first
  "postId" : "generated post id"
}
```

## User

### Create User
* [POST] http://lunahc92.tplinkdns.com:5100/api/users/create
```
{
  "id" : "lunahc92",
  "password" : "1234",
  "name" : "한울",
  "email" : "lunahc92@gmail.com"
}
```

### Read User
* [GET] http://lunahc92.tplinkdns.com:5100/api/users/read/{id}

### Update User
* [PUT] http://lunahc92.tplinkdns.com:5100/api/users/read/{id}
```
{
  "id" : "lunahc92",
  "password" : "123456789",
  "name" : "다니",
  "eamil" : "ekdms717@gmail.com"
}
```

### Delete User
* [DELETE] http://lunahc92.tplinkdns.com:5100/api/users/delete/{id}

### List User
* [GET] http://lunahc92.tplinkdns.com:5100/api/users/list?page={page#}

### Add Bookmark
* [POST] http://lunahc92.tplinkdns.com:5100/api/users/addbookmark
```
{
  "userId" : "lunahc92", // must create user first
  "postId" : "generated post id"
}
```

### Remove Bookmark
* [POST] http://lunahc92.tplinkdns.com:5100/api/users/removebookmark
```
{
  "userId" : "lunahc92", // must create user first
  "postId" : "generated post id"
}
```
