﻿// <auto-generated />
using System;
using ChampionsLeagueTest.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChampionsLeagueTest.DAL.Migrations
{
    [DbContext(typeof(ChampionsLeagueTestDbContext))]
    [Migration("20190421220346_Match-Fix")]
    partial class MatchFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChampionsLeagueTest.DAL.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GroupName")
                        .IsUnique();

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ChampionsLeagueTest.DAL.GroupStanding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId");

                    b.Property<int?>("GroupId1");

                    b.Property<int>("MachDay");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("GroupId1");

                    b.ToTable("GroupStandings");
                });

            modelBuilder.Entity("ChampionsLeagueTest.DAL.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AwayTeam")
                        .IsRequired();

                    b.Property<int>("GoalsAwayTeam");

                    b.Property<int>("GoalsHomeTeam");

                    b.Property<int>("GroupId");

                    b.Property<string>("HomeTeam")
                        .IsRequired();

                    b.Property<DateTime>("KickOffAt");

                    b.Property<string>("LeagueTitle")
                        .IsRequired();

                    b.Property<int>("MatchDay");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("ChampionsLeagueTest.DAL.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId");

                    b.Property<int?>("GroupId1");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("GroupId1");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ChampionsLeagueTest.DAL.TeamMatch", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("MatchId");

                    b.HasKey("TeamId", "MatchId");

                    b.HasIndex("MatchId");

                    b.ToTable("TeamMatches");
                });

            modelBuilder.Entity("ChampionsLeagueTest.DAL.TeamStanding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Draw");

                    b.Property<int>("GoalDifference");

                    b.Property<int>("Goals");

                    b.Property<int>("GoalsAgainst");

                    b.Property<int>("GroupStandingId");

                    b.Property<int?>("GroupStandingId1");

                    b.Property<int>("Lose");

                    b.Property<int>("PlayedGames");

                    b.Property<int>("Points");

                    b.Property<int>("Rank");

                    b.Property<int>("TeamId");

                    b.Property<int>("Win");

                    b.HasKey("Id");

                    b.HasIndex("GroupStandingId");

                    b.HasIndex("GroupStandingId1");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamStandings");
                });

            modelBuilder.Entity("ChampionsLeagueTest.DAL.GroupStanding", b =>
                {
                    b.HasOne("ChampionsLeagueTest.DAL.Group")
                        .WithMany("GroupStandings")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ChampionsLeagueTest.DAL.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId1");
                });

            modelBuilder.Entity("ChampionsLeagueTest.DAL.Match", b =>
                {
                    b.HasOne("ChampionsLeagueTest.DAL.Group", "Group")
                        .WithMany("Matches")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ChampionsLeagueTest.DAL.Team", b =>
                {
                    b.HasOne("ChampionsLeagueTest.DAL.Group")
                        .WithMany("Teams")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ChampionsLeagueTest.DAL.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId1");
                });

            modelBuilder.Entity("ChampionsLeagueTest.DAL.TeamMatch", b =>
                {
                    b.HasOne("ChampionsLeagueTest.DAL.Match", "Match")
                        .WithMany("TeamMatches")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChampionsLeagueTest.DAL.Team", "Team")
                        .WithMany("TeamMatches")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChampionsLeagueTest.DAL.TeamStanding", b =>
                {
                    b.HasOne("ChampionsLeagueTest.DAL.GroupStanding")
                        .WithMany("TeamStandings")
                        .HasForeignKey("GroupStandingId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ChampionsLeagueTest.DAL.GroupStanding", "GroupStanding")
                        .WithMany()
                        .HasForeignKey("GroupStandingId1");

                    b.HasOne("ChampionsLeagueTest.DAL.Team", "Team")
                        .WithMany("TeamStandings")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
