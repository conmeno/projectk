using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace projectk.Models
{
    public class ProjectkContext : DbContext
    {
        public ProjectkContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Article> Articles { get; set; }

        //public DbSet<Categories> Categories { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int TotalLike { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

    }

    public partial class Article
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ExternalURL { get; set; }
        public string DropboxShareLink { get; set; }
        public DateTime DropboxShareLinkExpire { get; set; }
        public int UserID { get; set; }
        public Nullable<long> PageView { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string LocalURL { get; set; }
        public int Status { get; set; }
        public bool IsHot { get; set; }


        public byte[] ThumbnailData { get; set; }
        public string OtherObject { get; set; }

        public int Cat { get; set; }

        public Cats Category
        {
            get { return (Cats)Cat; }
            set { Cat = (int)value; }
        }

        //public virtual Categories Category { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
    //public partial class Categories
    //{
    //    [Key]
    //    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    public int CategoryID { get; set; }
    //    public string Name { get; set; }

    //    public virtual ICollection<Article> Articles { get; set; }
    //}

    public enum Cats
    {
        Funny = 1, HotGirl = 2, Video = 3,Truyen=4
    }




}
