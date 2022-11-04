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
    public interface IReceitasService
    {
        IEnumerable<Receitas> GetAll(Guid userId);
        Receitas GetById(Guid id, Guid userId);
        Task<Receitas> Post(Receitas receita);
        Task<Receitas> Put(Guid id, Receitas receitaPost, Guid userId);
        Task<bool> Delete(Guid id, Guid userId);

    }

    public class ReceitasService : IReceitasService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public ReceitasService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<Receitas> GetAll(Guid userId)
        {
            return _context.Receitas.Include(c => c.Conta).Include(c => c.Categoria).Where(r => r.UserId == userId).ToList();
        }

        public Receitas GetById(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Receitas> Post(Receitas receita)
        {
            try
            {
                _context.Receitas.Add(receita);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return receita;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Receitas> Put(Guid id, Receitas receitaPost, Guid userId)
        {
            try
            {
                var receitas = await _context.Receitas.FindAsync(id);
                if (receitas == null) return null;

                receitaPost.Id = receitas.Id;

                _context.Update(receitaPost);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return await _context.Receitas.FindAsync(id);
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
                var receita = await _context.Receitas.FindAsync(id);
                if (receita == null) throw new Exception("Receita Não Encontrada");

                _context.Remove(receita);
                return await _context.SaveChangesAsync()>0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
