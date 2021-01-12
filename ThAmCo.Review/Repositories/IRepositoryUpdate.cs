namespace ThAmCo.Review.Repositories
{
    public interface IRepositoryUpdate<TModel> : IRepository
    {

        public void Update(TModel model);

    }
}
