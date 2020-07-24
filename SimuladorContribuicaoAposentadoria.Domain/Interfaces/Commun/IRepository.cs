using System.Linq;

namespace SimuladorContribuicaoAposentadoria.Domain.Interfaces.Commun
{
    public interface IRepository<TModel>
    {
        void Create(TModel model);

        void Update(TModel model);

        void Remove(TModel model);

        IQueryable<TModel> GetAll();
    }
}