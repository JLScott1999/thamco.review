namespace ThAmCo.Review.Repositories
{
    public interface IRepositoryInsert<TModel> : IRepository
    {

        public void Insert(TModel model);

    }
}
