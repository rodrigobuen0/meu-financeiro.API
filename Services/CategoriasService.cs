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
    public interface ICategoriasService
    {
        IEnumerable<Categorias> GetAll(Guid userId);
        Categorias GetById(Guid id, Guid userId);
        Task<Categorias> Post(Categorias categoria, Guid userId);
        Task<Categorias> Put(Guid id, Categorias categoriaPost, Guid userId);
        Task<bool> Delete(Guid id, Guid userId);

    }

    public class CategoriasService : ICategoriasService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public CategoriasService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<Categorias> GetAll(Guid userId)
        {
            return _context.Categorias.Where(r => r.UserId == userId).ToList();
        }

        public Categorias GetById(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Categorias> Post(Categorias categoria, Guid userId)
        {
            try
            {
                _context.Categorias.Add(categoria);

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

        public async Task<Categorias> Put(Guid id, Categorias categoriaPost, Guid userId)
        {
            try
            {
                var categorias = await _context.Categorias.FindAsync(id);
                if (categorias == null) return null;

                categoriaPost.Id = categorias.Id;

                _context.Update(categoriaPost);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return await _context.Categorias.FindAsync(id);
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
                var categoria = await _context.Categorias.FindAsync(id);
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
