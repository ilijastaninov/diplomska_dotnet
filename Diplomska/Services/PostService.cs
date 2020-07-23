using System;
using System.Collections.Generic;
using System.Linq;
using Diplomska.Context;
using Diplomska.Entities;
using Diplomska.Interfaces;

namespace Diplomska.Services
{
    public class PostService : IPostInterface
    {
        private readonly ConnectorDbContext context;
        public PostService(ConnectorDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<Post> GetAllPostsForUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return context.Posts.Where(p => p.UserId == userId).ToList();
        }

        public Post GetPostById(Guid userId, Guid postId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            if (postId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(postId));
            }

            return context.Posts.Where(p => p.UserId == userId && p.PostId == postId).FirstOrDefault();
        }

        public void AddPost(Guid userId, Post post)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            post.UserId = userId;
            context.Posts.Add(post);
        }

        public void UpdatePost(Post post)
        {
            
        }

        public void DeletePost(Post post)
        {
            context.Posts.Remove(post);
        }

        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

        public bool UserExists(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return context.Users.Any(a => a.Id == userId);
        }
        public User GetUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return context.Users
                .FirstOrDefault(u => u.Id == userId);
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return context.Posts.ToList();
        }
    }
}