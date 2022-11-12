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
    public interface ICategoriasReceitasService
    {
        IEnumerable<CategoriasReceitas> GetAll(Guid userId);
        CategoriasReceitas GetById(Guid id, Guid userId);
        Task<CategoriasReceitas> Post(CategoriasReceitas categoria, Guid userId);
        Task<CategoriasReceitas> Put(Guid id, CategoriasReceitas categoriaPost, Guid userId);
        Task<bool> Delete(Guid id, Guid userId);

    }

    public class CategoriasReceitasService : ICategoriasReceitasService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public CategoriasReceitasService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<CategoriasReceitas> GetAll(Guid userId)
        {
            return _context.CategoriasReceitas.Where(r => r.UserId == userId).ToList();
        }

        public CategoriasReceitas GetById(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoriasReceitas> Post(CategoriasReceitas categoria, Guid userId)
        {
            try
            {
                _context.CategoriasReceitas.Add(categoria);

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

        public async Task<CategoriasReceitas> Put(Guid id, CategoriasReceitas categoriaPost, Guid userId)
        {
            try
            {
                var categorias = await _context.CategoriasReceitas.FindAsync(id);
                if (categorias == null) return null;

                categoriaPost.Id = categorias.Id;

                _context.Update(categoriaPost);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return await _context.CategoriasReceitas.FindAsync(id);
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
                var categoria = await _context.CategoriasReceitas.FindAsync(id);
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
