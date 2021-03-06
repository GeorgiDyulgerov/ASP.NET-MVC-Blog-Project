﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogProject.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BlogProject.Models.Post> Posts { get; set; }

        public System.Data.Entity.DbSet<BlogProject.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<BlogProject.Models.Tag> Tags { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<Post>()
        //                .HasMany<Tag>(s => s.Tags)
        //                .WithMany(c => c.Posts)
        //                .Map(cs =>
        //                {
        //                    cs.MapLeftKey("PostRefId");
        //                    cs.MapRightKey("TagRefId");
        //                    cs.ToTable("PostsTags");
        //                });

        //}
    }
}