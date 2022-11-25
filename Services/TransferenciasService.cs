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
    public interface ITransferenciasService
    {
        IEnumerable<Transferencias> GetAll(Guid userId);
        IEnumerable<Transferencias> GetAllMes(Guid userId);
        Transferencias GetById(Guid id, Guid userId);
        Task<Transferencias> Post(Transferencias transferencia);
        Task<Transferencias> Put(Guid id, Transferencias transferenciaPost, Guid userId);
        Task<bool> Delete(Guid id, Guid userId);

    }

    public class TransferenciasService : ITransferenciasService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public TransferenciasService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<Transferencias> GetAll(Guid userId)
        {
            return _context.Transferencias.Where(r => r.UserId == userId).ToList();
        }
        public IEnumerable<Transferencias> GetAllMes(Guid userId)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            var firstDayOfMonth = new DateOnly(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return _context.Transferencias.Where(r => r.UserId == userId && r.Data >= firstDayOfMonth && r.Data <= lastDayOfMonth).OrderByDescending(r => r.Data).ToList();
        }

        public Transferencias GetById(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Transferencias> Post(Transferencias transferencia)
        {
            try
            {
                _context.Transferencias.Add(transferencia);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return transferencia;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Transferencias> Put(Guid id, Transferencias transferenciaPost, Guid userId)
        {
            try
            {
                var transferencias = await _context.Transferencias.FindAsync(id);
                if (transferencias == null) return null;

                transferenciaPost.Id = transferencias.Id;

                _context.Update(transferenciaPost);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return await _context.Transferencias.FindAsync(id);
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
                var transferencia = await _context.Transferencias.FindAsync(id);
                if (transferencia == null) throw new Exception("Receita Não Encontrada");

                _context.Remove(transferencia);
                return await _context.SaveChangesAsync()>0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
