using meu_financeiro.API.Authorization;
using meu_financeiro.API.Entities;
using meu_financeiro.API.Helpers;
using meu_financeiro.API.Models.Users;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace meu_financeiro.API.Services
{
    public interface IReceitasService
    {
        IEnumerable<Receitas> GetAll(Guid userId);
        Receitas GetById(Guid id);
        Task<Receitas> Post(Receitas receita);
        Receitas Put(Guid id, Receitas receita);
        Receitas Delete(Guid id);

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
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<Receitas> GetAll(Guid userId)
        {
            return _context.Receitas.Where(r => r.UserId == userId).ToList();
        }

        public Receitas GetById(Guid id)
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

            throw new NotImplementedException();
        }

        public Receitas Put(Guid id, Receitas receita)
        {
            throw new NotImplementedException();
        }

        public Receitas Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
