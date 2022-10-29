using meu_financeiro.API.Authorization;
using meu_financeiro.API.Entities;
using meu_financeiro.API.Helpers;
using meu_financeiro.API.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace meu_financeiro.API.Services
{
    public interface IDespesasService
    {
        IEnumerable<Despesas> GetAll(Guid userId);
        Despesas GetById(Guid id, Guid userId);
        Task<Despesas> Post(Despesas despesa, Guid userId);
        Task<Despesas> Put(Guid id, Despesas despesaPost, Guid userId);
        Task<bool> Delete(Guid id, Guid userId);

    }

    public class DespesasService : IDespesasService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public DespesasService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<Despesas> GetAll(Guid userId)
        {
            return _context.Despesas.Include(c => c.Conta).Include(c => c.Categoria).Where(r => r.UserId == userId).ToList();
        }

        public Despesas GetById(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Despesas> Post(Despesas despesa, Guid userId)
        {
            try
            {
                _context.Despesas.Add(despesa);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return despesa;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Despesas> Put(Guid id, Despesas despesaPost, Guid userId)
        {
            try
            {
                var Despesas = await _context.Despesas.FindAsync(id);
                if (Despesas == null) return null;

                despesaPost.Id = Despesas.Id;

                _context.Update(despesaPost);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return await _context.Despesas.FindAsync(id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(Guid id, Guid userId)
        {
            try
            {
                var despesa = await _context.Despesas.FindAsync(id);
                if (despesa == null) throw new Exception("Despesa Não Encontrada");

                _context.Remove(despesa);
                return await _context.SaveChangesAsync()>0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
