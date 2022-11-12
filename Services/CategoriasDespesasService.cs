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
    public interface ICategoriasDespesasService
    {
        IEnumerable<CategoriasDespesas> GetAll(Guid userId);
        CategoriasDespesas GetById(Guid id, Guid userId);
        Task<CategoriasDespesas> Post(CategoriasDespesas categoria, Guid userId);
        Task<CategoriasDespesas> Put(Guid id, CategoriasDespesas categoriaPost, Guid userId);
        Task<bool> Delete(Guid id, Guid userId);

    }

    public class CategoriasDespesasService : ICategoriasDespesasService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public CategoriasDespesasService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<CategoriasDespesas> GetAll(Guid userId)
        {
            return _context.CategoriasDespesas.Where(r => r.UserId == userId).ToList();
        }

        public CategoriasDespesas GetById(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoriasDespesas> Post(CategoriasDespesas categoria, Guid userId)
        {
            try
            {
                _context.CategoriasDespesas.Add(categoria);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return categoria;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoriasDespesas> Put(Guid id, CategoriasDespesas categoriaPost, Guid userId)
        {
            try
            {
                var categorias = await _context.CategoriasDespesas.FindAsync(id);
                if (categorias == null) return null;

                categoriaPost.Id = categorias.Id;

                _context.Update(categoriaPost);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return await _context.CategoriasDespesas.FindAsync(id);
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
                var categoria = await _context.CategoriasDespesas.FindAsync(id);
                if (categoria == null) throw new Exception("Conta Não Encontrada");

                _context.Remove(categoria);
                return await _context.SaveChangesAsync()>0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
