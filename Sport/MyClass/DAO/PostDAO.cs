﻿using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class PostDAO
    {
        private SportDbContext db = new SportDbContext();
        public List<PostInfo> getListByTopicId(int? topid,string type = "Post")
        {
            List<PostInfo> list = db.Posts
                .Join(
                db.Topics,
                p => p.TopicId,
                t => t.Id,
                (p, t) => new PostInfo()
                {
                    Id = p.Id,
                    TopicId = p.TopicId,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    Description = p.Description,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                }
                )
               .Where(m => m.Status == 1 && m.PostType == type && m.TopicId==topid).ToList();
            return list;
        }
        public List<PostInfo> getList(string type = "Page")
        {
            List<PostInfo> list = db.Posts
                .Join(
                db.Topics,
                p => p.TopicId,
                t => t.Id,
                (p, t) => new PostInfo()
                {
                    Id = p.Id,
                    TopicId = p.TopicId,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    Description = p.Description,
                    Created_By = p.Created_By,
                    Created_At = p.Created_At,
                    Updated_By = p.Updated_By,
                    Updated_At = p.Updated_At,
                    Status = p.Status
                }
                )
               .Where(m => m.Status == 1 && m.PostType == type)
               .OrderByDescending(m => m.Created_At)
                
                .ToList();
            return list;
        }
        public List<Post> getList(string status = "All", string type = "Page")
        {
            List<Post> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Posts.Where(m => m.Status != 0 && m.PostType == type).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Posts.Where(m => m.Status == 0 && m.PostType == type).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Posts.Where(m => m.PostType == type).ToList();
                        break;
                    }
            }
            return list;
        }
        //Trả về 1 mẫu tin
        public Post getRow(int? id)
        {
            if (id == null)
            { return null; }
            else
            {
                return db.Posts.Find(id);
            }
        }
        public Post getRow(string slug, string posttype)
        {
            return db.Posts
                .Where(m => m.PostType == posttype && m.Slug == slug && m.Status == 1)
                .FirstOrDefault();
        }
        public int Insert(Post row)
        {
            db.Posts.Add(row);
            return db.SaveChanges();
        }
        public int Update(Post row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public int Delete(Post row)
        {
            db.Posts.Remove(row);
            return db.SaveChanges();
        }
    }
}
