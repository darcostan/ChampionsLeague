using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTest.DAL.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ChampionsLeagueTestDbContext _db;

        public DbInitializer(ChampionsLeagueTestDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            _db.Database.Migrate();

            var groups = _db.Groups.ToList();

            if (groups.Count != 8)
            {
                _db.Groups.RemoveRange(groups);

                _db.SaveChanges();

                _db.Groups.AddRange(GenerateGroups());

                _db.SaveChanges();
            }

            var teams = _db.Teams.ToList();

            if (teams.Count != 32)
            {
                _db.RemoveRange(teams);

                _db.SaveChanges();

                _db.Teams.AddRange(GenerateTeams(_db.Groups.ToList()));

                _db.SaveChanges();
            }
        }

        private List<Group> GenerateGroups()
        {
            var groupNames = new List<string> {"A", "B", "C", "D", "E", "F", "G", "H"};
            var result = new List<Group>(8);

            foreach (var name in groupNames)
            {
                result.Add(new Group {GroupName = name});
            }

            return result;
        }

        private List<Team> GenerateTeams(List<Group> groups)
        {
            var list = new List<(string group, string teamName)>
            {
                ("A", "PSG"), ("A", "Arsenal"), ("A", "Basel"), ("A", "Ludogorets"),
                ("B", "Benfica"), ("B", "Napoli"), ("B", "Dynamo Kyiv"), ("B", "Besiktas"),
                ("C", "Barcelona"), ("C", "Manchester City"), ("C", "Borussia Mönchengladbach"), ("C", "Celtic"),
                ("D", "Bayern"), ("D", "Atletico Madrid"), ("D", "PSV"), ("D", "Rostov"),
                ("E", "CSKA Moscow"), ("E", "Bayer Leverkusen"), ("E", "Tottenham"), ("E", "Monaco"),
                ("F", "Real Madrid"), ("F", "Borussia Dortmund"), ("F", "Sporting CP"), ("F", "Legia Warszawa"),
                ("G", "Leicester"), ("G", "Porto"), ("G", "Club Brugge"), ("G", "København"),
                ("H", "Juventus"), ("H", "Sevilla"), ("H", "Olympique Lyon"), ("H", "Dinamo Zagreb")
            };

            var result = new List<Team>();

            foreach (var team in list)
            {
                var group = groups.SingleOrDefault(g => g.GroupName == team.group);
                if (group != null)
                {
                    result.Add(new Team {Name = team.teamName, GroupId = group.Id, Group = group});
                }
            }

            return result;
        }
    }

    public interface IDbInitializer
    {
        void Initialize();
    }
}
