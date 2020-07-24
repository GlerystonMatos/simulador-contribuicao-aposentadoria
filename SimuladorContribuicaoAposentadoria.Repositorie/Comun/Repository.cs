using Microsoft.EntityFrameworkCore;
using SimuladorContribuicaoAposentadoria.Data.Context;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Commun;
using System.Linq;

namespace SimuladorContribuicaoAposentadoria.Data.Comun
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : Entity
    {
        protected readonly SimuladorContribuicaoAposentadoriaContext _context;

        public Repository(SimuladorContribuicaoAposentadoriaContext context)
            => _context = context;

        public void Create(TModel model)
        {
            _context.Set<TModel>().Add(model);
            _context.SaveChanges();
        }

        public void Update(TModel model)
        {
            DetachLocal(model);
            _context.Set<TModel>().Update(model);
            _context.SaveChanges();
        }

        public void Remove(TModel model)
        {
            DetachLocal(model);
            _context.Set<TModel>().Remove(model);
            _context.SaveChanges();
        }

        public IQueryable<TModel> GetAll()
            => _context.Set<TModel>().AsQueryable();

        private void DetachLocal(TModel model)
        {
            TModel local = _context.Set<TModel>().Local.Where(m => m.Id.Equals(model.Id)).FirstOrDefault();
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}