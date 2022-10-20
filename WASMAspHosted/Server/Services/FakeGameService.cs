using WASMAspHosted.Shared;

namespace WASMAspHosted.Server.Services
{
    public class FakeGameService
    {
        public List<Game> MyList { get; set; }
        public FakeGameService()
        {
            MyList = new List<Game>
            {
                new Game { Id =1 , Name = "World of Warcraft", Genre = "Meuporg", Description = "on tue des trucs qui bougent"},
                new Game { Id =2 , Name = "League of Noobie", Genre = "Moba", Description = "on tue des trucs qui bougent"},
                new Game { Id =3 , Name = "The 4th Coming", Genre = "Meuporg", Description = "..."}
            };
        }

        public List<Game> GetAll()
        {
            return MyList;
        }
        public Game GetById(int id)
        {
            return MyList.FirstOrDefault(x => x.Id == id);
        }
        public void Add(Game j)
        {
            MyList.Add(j);
        }
    }
}
