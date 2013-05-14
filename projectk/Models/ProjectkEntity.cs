using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace projectk.Models
{
    public class ProjectkEntity:DbContext
    {
        
            public ProjectkEntity()
                : base("DefaultConnection")
            {
            }

           // public DbSet<UserProfile> UserProfiles { get; set; }
            public DbSet<Test1> test1s { get; set; }
            public DbSet<Test2> test2s { get; set; }

            public DbSet<UserProfile> UserProfiles { get; set; }
         
    }
    public class Test1
    {
        public int ID { get; set; }
        public string value { get; set; }
    }
    public class Test2
    {
        public int ID { get; set; }
        public string value { get; set; }
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

    }

}