using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public DbSet<ArticleImages> ArticleImages { get; set; }
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
        //[RegularExpression(@"[0-9]",
        //    ErrorMessage = "Năm sinh không hợp lệ")]

        public int ID { get; set; }
         [DisplayName("Tiêu đề")][Required]
        
        public string Name { get; set; }
         [DisplayName("Đường dẫn youtube")] 
        public string ExternalURL { get; set; }
        public string DropboxShareLink { get; set; }
        public DateTime DropboxShareLinkExpire { get; set; }
        public int UserID { get; set; }
        public long PageView { get; set; }
        public long Like { get; set; }
        public long Comment { get; set; }
        [DisplayName("Mô tả ngắn")] 
        public string Description { get; set; }
        public string Content { get; set; }
        public string LocalURL { get; set; }
        [DisplayName("Trạng thái")]
        public int Status { get; set; }//0: new post, 1: approve, 2: fuck
        public bool IsHot { get; set; }
        [DisplayName("Nguồn")]
        public string Source { get; set; }
        public string UserName { get; set; }
        [DisplayName("Ngày post")]
        public DateTime DatePost { get; set; }
        [DisplayName("Thumbnail")]
        public byte[] ThumbnailData { get; set; }
        public string OtherObject { get; set; }
        public string ThumbnailURL { get; set; }
        public int Cat { get; set; }

        public Cats Category
        {
            get { return (Cats)Cat; }
            set { Cat = (int)value; }
        }

        //public virtual Categories Category { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public string getYoutubeID()
        {
            if (this.Category == Cats.Video)
            {
                var list = this.ExternalURL.Split('=');
                if (list.Length > 1)
                    return list[1];
                return "";
            }
            return "";
        }
        public string GetStatus()
        {
            if (this.Status == 0)
                return "Đang duyệt";
            if (this.Status == 1)
                return "Đã duyệt";
            return "Bị hủy";
        }
    }
    public class ArticleImages
    {
        public int ID { get; set; }
        public int ArticleID { get; set; }
        public string LocalURL { get; set; }
        public string DropboxURL { get; set; }
        public string DropboxShareLink { get; set; }
        public DateTime DropboxShareLinkExpire { get; set; }
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
