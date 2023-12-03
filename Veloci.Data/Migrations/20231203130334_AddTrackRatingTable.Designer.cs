﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Veloci.Data;

#nullable disable

namespace Veloci.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231203130334_AddTrackRatingTable")]
    partial class AddTrackRatingTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Veloci.Data.Domain.Competition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<long>("ChatId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CurrentResultsId")
                        .HasColumnType("TEXT");

                    b.Property<string>("InitialResultsId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ResultsPosted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TrackId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CurrentResultsId");

                    b.HasIndex("InitialResultsId");

                    b.HasIndex("TrackId");

                    b.ToTable("Competitions", (string)null);
                });

            modelBuilder.Entity("Veloci.Data.Domain.CompetitionResults", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CompetitionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("GlobalRank")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocalRank")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrackTime")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.ToTable("CompetitionResults", (string)null);
                });

            modelBuilder.Entity("Veloci.Data.Domain.Track", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("MapId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RatingId")
                        .HasColumnType("TEXT");

                    b.Property<int>("TrackId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.HasIndex("RatingId");

                    b.ToTable("Tracks", (string)null);
                });

            modelBuilder.Entity("Veloci.Data.Domain.TrackMap", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("MapId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TrackMaps", (string)null);
                });

            modelBuilder.Entity("Veloci.Data.Domain.TrackRating", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("PollMessageId")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("TrackRating");
                });

            modelBuilder.Entity("Veloci.Data.Domain.TrackResults", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TrackResults", (string)null);
                });

            modelBuilder.Entity("Veloci.Data.Domain.TrackTime", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("GlobalRank")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocalRank")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Time")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TrackResultsId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TrackResultsId");

                    b.ToTable("TrackTimes", (string)null);
                });

            modelBuilder.Entity("Veloci.Data.Domain.TrackTimeDelta", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CompetitionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LocalRank")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LocalRankOld")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RankOld")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TimeChange")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrackTime")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.ToTable("TrackTimeDeltas", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Veloci.Data.Domain.Competition", b =>
                {
                    b.HasOne("Veloci.Data.Domain.TrackResults", "CurrentResults")
                        .WithMany()
                        .HasForeignKey("CurrentResultsId");

                    b.HasOne("Veloci.Data.Domain.TrackResults", "InitialResults")
                        .WithMany()
                        .HasForeignKey("InitialResultsId");

                    b.HasOne("Veloci.Data.Domain.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentResults");

                    b.Navigation("InitialResults");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Veloci.Data.Domain.CompetitionResults", b =>
                {
                    b.HasOne("Veloci.Data.Domain.Competition", "Competition")
                        .WithMany("CompetitionResults")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");
                });

            modelBuilder.Entity("Veloci.Data.Domain.Track", b =>
                {
                    b.HasOne("Veloci.Data.Domain.TrackMap", "Map")
                        .WithMany("Tracks")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Veloci.Data.Domain.TrackRating", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId");

                    b.Navigation("Map");

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("Veloci.Data.Domain.TrackTime", b =>
                {
                    b.HasOne("Veloci.Data.Domain.TrackResults", null)
                        .WithMany("Times")
                        .HasForeignKey("TrackResultsId");
                });

            modelBuilder.Entity("Veloci.Data.Domain.TrackTimeDelta", b =>
                {
                    b.HasOne("Veloci.Data.Domain.Competition", "Competition")
                        .WithMany("TimeDeltas")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");
                });

            modelBuilder.Entity("Veloci.Data.Domain.Competition", b =>
                {
                    b.Navigation("CompetitionResults");

                    b.Navigation("TimeDeltas");
                });

            modelBuilder.Entity("Veloci.Data.Domain.TrackMap", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("Veloci.Data.Domain.TrackResults", b =>
                {
                    b.Navigation("Times");
                });
#pragma warning restore 612, 618
        }
    }
}
