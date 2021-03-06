﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Jugnoon.Models;

namespace Jugnoon.Framework
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            /* Setup Default Values When Creating Database */

            // ApplicationUser
            builder.Entity<ApplicationUser>().Property(b => b.UserName).HasMaxLength(100);
            builder.Entity<ApplicationUser>().Property(b => b.NormalizedUserName).HasMaxLength(100);
            builder.Entity<ApplicationUser>().Property(b => b.Email).HasMaxLength(100);
            builder.Entity<ApplicationUser>().Property(b => b.NormalizedEmail).HasMaxLength(100);
            builder.Entity<ApplicationUser>().Property(b => b.PhoneNumber).HasMaxLength(30);

            // JGN_AbuseReports
            builder.Entity<JGN_AbuseReports>().Property(b => b.type).HasDefaultValue(0);
            
            // JGN_Ads
            builder.Entity<JGN_Ads>().Property(b => b.type).HasDefaultValue(0);
            
            // JGN_Attr_Attributes
            builder.Entity<JGN_Attr_Attributes>().Property(b => b.sectionid).HasDefaultValue(0);
            builder.Entity<JGN_Attr_Attributes>().Property(b => b.priority).HasDefaultValue(0);
            builder.Entity<JGN_Attr_Attributes>().Property(b => b.attr_type).HasDefaultValue(0);
            builder.Entity<JGN_Attr_Attributes>().Property(b => b.attr_type).HasDefaultValue(0);
            builder.Entity<JGN_Attr_Attributes>().Property(b => b.element_type).HasDefaultValue(0);
            builder.Entity<JGN_Attr_Attributes>().Property(b => b.isrequired).HasDefaultValue(0);
            builder.Entity<JGN_Attr_Attributes>().Property(b => b.variable_type).HasDefaultValue(0);
            builder.Entity<JGN_Attr_Attributes>().Property(b => b.min).HasDefaultValue(0);
            builder.Entity<JGN_Attr_Attributes>().Property(b => b.max).HasDefaultValue(0);

            // JGN_Attr_Templates
            builder.Entity<JGN_Attr_Templates>().Property(b => b.attr_type).HasDefaultValue(0);
            
            // JGN_Attr_Templates
            builder.Entity<JGN_Attr_TemplateSections>().Property(b => b.attr_type).HasDefaultValue(0);
            builder.Entity<JGN_Attr_TemplateSections>().Property(b => b.priority).HasDefaultValue(0);
            builder.Entity<JGN_Attr_TemplateSections>().Property(b => b.showsection).HasDefaultValue(0);

            // JGN_Attr_Values
            builder.Entity<JGN_Attr_Values>().Property(b => b.attr_type).HasDefaultValue(0);
            builder.Entity<JGN_Attr_Values>().Property(b => b.priority).HasDefaultValue(0);

          
            // JGN_Categories
            builder.Entity<JGN_Categories>().Property(b => b.parentid).HasDefaultValue(0);
            builder.Entity<JGN_Categories>().Property(b => b.type).HasDefaultValue(0);
            builder.Entity<JGN_Categories>().Property(b => b.priority).HasDefaultValue(0);
            builder.Entity<JGN_Categories>().Property(b => b.isenabled).HasDefaultValue(0);
            builder.Entity<JGN_Categories>().Property(b => b.isenabled).HasDefaultValue(0);
            builder.Entity<JGN_Categories>().Property(b => b.mode).HasDefaultValue(0);
            builder.Entity<JGN_Categories>().Property(b => b.records).HasDefaultValue(0);

            // JGN_CategoryContents
            builder.Entity<JGN_CategoryContents>().Property(b => b.type).HasDefaultValue(0);

            // JGN_Dictionary
            builder.Entity<JGN_Dictionary>().Property(b => b.type).HasDefaultValue(0);

            // JGN_Favorites
            builder.Entity<JGN_Favorites>().Property(b => b.type).HasDefaultValue(0);

            // JGN_Languages
            builder.Entity<JGN_Languages>().Property(b => b.isdefault).HasDefaultValue(0);
            builder.Entity<JGN_Languages>().Property(b => b.isselected).HasDefaultValue(0);
                     
            // JGN_Messages
            builder.Entity<JGN_Messages>().Property(b => b.reply_id).HasDefaultValue(0);

            // JGN_Notifications
            builder.Entity<JGN_Notifications>().Property(b => b.notification_type).HasDefaultValue(0);
            builder.Entity<JGN_Notifications>().Property(b => b.is_unread).HasDefaultValue(0);
            builder.Entity<JGN_Notifications>().Property(b => b.is_hidden).HasDefaultValue(0);
                                 
            // JGN_User_Ratings
            builder.Entity<JGN_User_Ratings>().Property(b => b.type).HasDefaultValue(0);
            builder.Entity<JGN_User_Ratings>().Property(b => b.rating).HasDefaultValue(0);

            // JGN_User_Settings
            builder.Entity<JGN_User_Settings>().Property(b => b.issendmessages).HasDefaultValue(0);
            builder.Entity<JGN_User_Settings>().Property(b => b.isemail).HasDefaultValue(0);

        }

        public virtual DbSet<JGN_AbuseReports> JGN_AbuseReports { get; set; }
        public virtual DbSet<JGN_Ads> JGN_Ads { get; set; }
        public virtual DbSet<JGN_BlockIP> JGN_BlockIP { get; set; }
        public virtual DbSet<JGN_Categories> JGN_Categories { get; set; }
        public virtual DbSet<JGN_CategoryContents> JGN_CategoryContents { get; set; }
        public virtual DbSet<JGN_Dictionary> JGN_Dictionary { get; set; }
        public virtual DbSet<JGN_Log> JGN_Log { get; set; }
        public virtual DbSet<JGN_Favorites> JGN_Favorites { get; set; }
        public virtual DbSet<JGN_Languages> JGN_Languages { get; set; }
        public virtual DbSet<JGN_MailTemplates> JGN_MailTemplates { get; set; }
        public virtual DbSet<JGN_User_Ratings> JGN_User_Ratings { get; set; }
        public virtual DbSet<ApplicationUser> AspNetusers { get; set; }
        public virtual DbSet<JGN_User_Settings> JGN_User_Settings { get; set; }
        public virtual DbSet<JGN_User_IPLogs> JGN_User_IPLogs { get; set; }
        public virtual DbSet<JGN_RoleObjects> JGN_RoleObjects { get; set; }
        public virtual DbSet<JGN_RolePermissions> JGN_RolePermissions { get; set; }
        public virtual DbSet<JGN_Roles> JGN_Roles { get; set; }
        public virtual DbSet<JGN_Wiki> JGN_Wiki { get; set; }
        public virtual DbSet<JGN_Attr_Attributes> JGN_Attr_Attributes { get; set; }
        public virtual DbSet<JGN_Attr_Values> JGN_Attr_Values { get; set; }
        public virtual DbSet<JGN_Attr_Templates> JGN_Attr_Templates { get; set; }
        public virtual DbSet<JGN_Attr_TemplateSections> JGN_Attr_TemplateSections { get; set; }
        public virtual DbSet<JGN_Messages> JGN_Messages { get; set; }
        public virtual DbSet<JGN_Messages_Recipents> JGN_Messages_Recipents { get; set; }
        public virtual DbSet<JGN_Notifications> JGN_Notifications { get; set; }

    }

}

/*
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.md', which is part of this source code package.
 * Copyright 2007 - 2020 MediaSoftPro
 * For more information email at support@mediasoftpro.com
 */
