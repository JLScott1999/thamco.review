namespace ThAmCo.Review.Repositories
{
    using System;

    public interface IRepositoryDelete<TModel> : IRepository
    {

        public void Delete(TModel model);

        public void Delete(Guid id);

    }
}
