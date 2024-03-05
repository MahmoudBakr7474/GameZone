namespace GameZone.Services
{
    public interface IGameServices
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task Create(CreateGameFormViewModel model);
        Task<Game?> Update(EditGameFormviewModel model);

        bool Delete(int id);


    }
}
