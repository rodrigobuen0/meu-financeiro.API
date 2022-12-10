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
    public interface IReajusteSaldoService
    {
        IEnumerable<ReajustesSaldoContas> GetAll(Guid userId);
        IEnumerable<ReajustesSaldoContas> GetAllMes(Guid userId);
        ReajustesSaldoContas GetById(Guid id, Guid userId);
        Task<ReajustesSaldoContas> Post(ReajustesSaldoContas reajusteSaldoConta);
        Task<ReajustesSaldoContas> Put(Guid id, ReajustesSaldoContas reajusteSaldoContaPost, Guid userId);
        Task<bool> Delete(Guid id, Guid userId);

    }

    public class ReajusteSaldoService : IReajusteSaldoService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public ReajusteSaldoService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<ReajustesSaldoContas> GetAll(Guid userId)
        {
            return _context.ReajustesSaldoContas.Where(r => r.UserId == userId).ToList();
        }
        public IEnumerable<ReajustesSaldoContas> GetAllMes(Guid userId)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            var firstDayOfMonth = new DateOnly(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return _context.ReajustesSaldoContas.Where(r => r.UserId == userId && r.DataTransacao >= firstDayOfMonth && r.DataTransacao <= lastDayOfMonth).OrderByDescending(r => r.DataTransacao).ToList();
        }

        public ReajustesSaldoContas GetById(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ReajustesSaldoContas> Post(ReajustesSaldoContas reajusteSaldoConta)
        {
            try
            {
                _context.ReajustesSaldoContas.Add(reajusteSaldoConta);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return reajusteSaldoConta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReajustesSaldoContas> Put(Guid id, ReajustesSaldoContas reajusteSaldoContaPost, Guid userId)
        {
            try
            {
                var ReajustesSaldoContas = await _context.ReajustesSaldoContas.FindAsync(id);
                if (ReajustesSaldoContas == null) return null;

                reajusteSaldoContaPost.Id = ReajustesSaldoContas.Id;

                _context.Update(reajusteSaldoContaPost);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return await _context.ReajustesSaldoContas.FindAsync(id);
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
                var reajusteSaldoConta = await _context.ReajustesSaldoContas.FindAsync(id);
                if (reajusteSaldoConta == null) throw new Exception("reajusteSaldoConta Não Encontrada");

                _context.Remove(reajusteSaldoConta);
                return await _context.SaveChangesAsync()>0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
