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
    public interface IContasService
    {
        IEnumerable<Contas> GetAll(Guid userId);
        Contas GetById(Guid id, Guid userId);
        Task<Contas> Post(Contas conta, Guid userId);
        Task<Contas> Put(Guid id, Contas contaPost, Guid userId);
        Task<bool> Delete(Guid id, Guid userId);

    }

    public class ContasService : IContasService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public ContasService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<Contas> GetAll(Guid userId)
        {
            return _context.Contas.Where(r => r.UserId == userId).ToList();
        }

        public Contas GetById(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Contas> Post(Contas conta, Guid userId)
        {
            try
            {
                _context.Contas.Add(conta);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return conta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Contas> Put(Guid id, Contas contaPost, Guid userId)
        {
            try
            {
                var contas = await _context.Contas.FindAsync(id);
                if (contas == null) return null;

                contaPost.Id = contas.Id;

                _context.Update(contaPost);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return await _context.Contas.FindAsync(id);
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
                var conta = await _context.Contas.FindAsync(id);
                if (conta == null) throw new Exception("Conta Não Encontrada");

                _context.Remove(conta);
                return await _context.SaveChangesAsync()>0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
