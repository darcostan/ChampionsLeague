using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTest.DAL
{
    public class ChampionsLeagueTestDbContext : DbContext
    {
        public ChampionsLeagueTestDbContext(DbContextOptions<ChampionsLeagueTestDbContext> options)
            : base(options)
        { }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Group> Groups { get; set;  }
        public DbSet<Team> Teams { get; set; }
        public DbSet<GroupStanding> GroupStandings { get; set; }
        public DbSet<TeamStanding> TeamStandings { get; set; }
        public DbSet<TeamMatch> TeamMatches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=ChampionsLeagueTestDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(bld =>
            {
                bld.HasMany(b => b.Matches).WithOne(b => b.Group).HasForeignKey(a => a.GroupId).OnDelete(DeleteBehavior.Restrict);
                bld.HasMany(b => b.Teams).WithOne().HasForeignKey(a => a.GroupId).OnDelete(DeleteBehavior.Restrict);
                bld.HasMany(b => b.GroupStandings).WithOne().HasForeignKey(b => b.GroupId).OnDelete(DeleteBehavior.Restrict);

                bld.Property(b => b.GroupName).IsRequired();
                bld.HasIndex(b => b.GroupName).IsUnique();
            });

            modelBuilder.Entity<Match>(bld =>
            {
                bld.Property(b => b.AwayTeam).IsRequired();
                bld.Property(b => b.HomeTeam).IsRequired();
                bld.Property(b => b.KickOffAt).IsRequired();
                bld.Property(b => b.GoalsAwayTeam).IsRequired();
                bld.Property(b => b.GoalsHomeTeam).IsRequired();
                bld.Property(b => b.LeagueTitle).IsRequired();
            });

            modelBuilder.Entity<Team>(bld =>
            {
                bld.HasMany(a => a.TeamStandings).WithOne(a => a.Team).HasForeignKey(a => a.TeamId).OnDelete(DeleteBehavior.Cascade);

                bld.Property(a => a.Name).IsRequired();
                bld.HasIndex(a => a.Name).IsUnique();
            });

            modelBuilder.Entity<GroupStanding>(bld =>
            {
                bld.HasMany(a => a.TeamStandings).WithOne().HasForeignKey(a => a.GroupStandingId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TeamStanding>(bld =>
            {
                bld.Property(a => a.Draw).IsRequired();
                bld.Property(a => a.GoalDifference).IsRequired();
                bld.Property(a => a.Goals).IsRequired();
                bld.Property(a => a.GoalsAgainst).IsRequired();
                bld.Property(a => a.Lose).IsRequired();
                bld.Property(a => a.PlayedGames).IsRequired();
                bld.Property(a => a.Points).IsRequired();
                bld.Property(a => a.Rank).IsRequired();
                bld.Property(a => a.Win).IsRequired();
            });

            modelBuilder.Entity<TeamMatch>()
                .HasKey(pc => new { pc.TeamId, pc.MatchId });
        }
    }
}
