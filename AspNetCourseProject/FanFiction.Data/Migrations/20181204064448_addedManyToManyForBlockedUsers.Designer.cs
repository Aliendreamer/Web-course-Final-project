﻿// <auto-generated />
using System;
using FanFiction.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FanFiction.Data.Migrations
{
    [DbContext(typeof(FanFictionContext))]
    [Migration("20181204064448_addedManyToManyForBlockedUsers")]
    partial class addedManyToManyForBlockedUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FanFiction.Models.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("PublshedOn");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("FanFiction.Models.BlockedUsers", b =>
                {
                    b.Property<string>("FanfictionUserId");

                    b.Property<string>("BlockedUserId");

                    b.HasKey("FanfictionUserId", "BlockedUserId");

                    b.HasIndex("BlockedUserId");

                    b.ToTable("BlockedUsers");
                });

            modelBuilder.Entity("FanFiction.Models.Chapter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(3000);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("FanFictionStoryId");

                    b.Property<int?>("FanFictionStoryId1");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("FanFictionStoryId");

                    b.HasIndex("FanFictionStoryId1");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("FanFiction.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChapterId");

                    b.Property<DateTime>("CommentedOn");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<int?>("StoryId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.HasIndex("StoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FanFiction.Models.DbLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<bool>("Handled");

                    b.Property<string>("LogType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("FanFiction.Models.FanFictionRating", b =>
                {
                    b.Property<int>("FanFictionId");

                    b.Property<int>("RatingId");

                    b.HasKey("FanFictionId", "RatingId");

                    b.HasIndex("RatingId");

                    b.ToTable("FictionRatings");
                });

            modelBuilder.Entity("FanFiction.Models.FanFictionStory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime>("LastEditedOn");

                    b.Property<int>("StoryTypeId");

                    b.Property<string>("Summary")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("StoryTypeId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("FictionStories");
                });

            modelBuilder.Entity("FanFiction.Models.FanFictionUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FanFiction.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsReaden");

                    b.Property<string>("ReceiverId");

                    b.Property<DateTime>("SendOn");

                    b.Property<string>("SenderId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("FanFiction.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FanFictionUserId");

                    b.Property<string>("Message");

                    b.HasKey("Id");

                    b.HasIndex("FanFictionUserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("FanFiction.Models.StoryRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Rating");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("StoryRatings");
                });

            modelBuilder.Entity("FanFiction.Models.StoryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("StoryTypes");
                });

            modelBuilder.Entity("FanFiction.Models.UserStory", b =>
                {
                    b.Property<string>("FanfictionUserId");

                    b.Property<int>("FanFictionStoryId");

                    b.HasKey("FanfictionUserId", "FanFictionStoryId");

                    b.HasIndex("FanFictionStoryId");

                    b.ToTable("UsersStories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FanFiction.Models.Announcement", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionUser", "Author")
                        .WithMany("Announcements")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FanFiction.Models.BlockedUsers", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionUser", "BlockedUser")
                        .WithMany("BLockedBy")
                        .HasForeignKey("BlockedUserId");

                    b.HasOne("FanFiction.Models.FanFictionUser", "FanFictionUser")
                        .WithMany("BlockedUsers")
                        .HasForeignKey("FanfictionUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FanFiction.Models.Chapter", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionUser", "FanFictionUser")
                        .WithMany("Chapters")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FanFiction.Models.FanFictionStory", "FanFictionStory")
                        .WithMany()
                        .HasForeignKey("FanFictionStoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FanFiction.Models.FanFictionStory")
                        .WithMany("Chapters")
                        .HasForeignKey("FanFictionStoryId1");
                });

            modelBuilder.Entity("FanFiction.Models.Comment", b =>
                {
                    b.HasOne("FanFiction.Models.Chapter", "Chapter")
                        .WithMany("Comments")
                        .HasForeignKey("ChapterId");

                    b.HasOne("FanFiction.Models.FanFictionStory", "FanFictionStory")
                        .WithMany("Comments")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("FanFiction.Models.FanFictionUser", "FanFictionUser")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FanFiction.Models.FanFictionRating", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionStory", "FanFictionStory")
                        .WithMany("Ratings")
                        .HasForeignKey("FanFictionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FanFiction.Models.StoryRating", "StoryRating")
                        .WithMany("FanFictionRatings")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FanFiction.Models.FanFictionStory", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionUser", "Author")
                        .WithMany("FanFictionStories")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FanFiction.Models.StoryType", "Type")
                        .WithMany("Stories")
                        .HasForeignKey("StoryTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FanFiction.Models.Message", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionUser", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FanFiction.Models.FanFictionUser", "Sender")
                        .WithMany("SendMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FanFiction.Models.Notification", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionUser", "FanFictionUser")
                        .WithMany("Notifications")
                        .HasForeignKey("FanFictionUserId");
                });

            modelBuilder.Entity("FanFiction.Models.StoryRating", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionUser", "FanFictionUser")
                        .WithMany("StoryRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FanFiction.Models.UserStory", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionStory", "FanFictionStory")
                        .WithMany("Followers")
                        .HasForeignKey("FanFictionStoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FanFiction.Models.FanFictionUser", "FanFictionUser")
                        .WithMany("FollowedStories")
                        .HasForeignKey("FanfictionUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FanFiction.Models.FanFictionUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FanFiction.Models.FanFictionUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
