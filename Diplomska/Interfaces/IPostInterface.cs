using System;
using System.Collections.Generic;
using Diplomska.Entities;

namespace Diplomska.Interfaces
{
    public interface IPostInterface
    {
        IEnumerable<Post> GetAllPostsForUser(Guid userId);
        Post GetPostById(Guid userId, Guid postId);
        void AddPost(Guid userId, Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        bool Save();
        bool UserExists(Guid userId);
        User GetUser(Guid id);
        IEnumerable<Post> GetAllPosts();
    }
}