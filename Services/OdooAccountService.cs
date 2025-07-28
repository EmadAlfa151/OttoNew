using CoreSystem.DAL.Context;
using CoreSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using OttoNew.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OttoNew.Services
{
	public class OdooAccountService
	{
		private readonly  AppDbContext _context;

		public OdooAccountService( AppDbContext context)
		{
			_context = context;
		}

		public async Task<Result<tOdooAccount>> GetFirstAccount()
		{
			var account = await _context.tOdooAccounts.FirstOrDefaultAsync();
			if (account is null)
			{
				return Result<tOdooAccount>.Failure("No Odoo Accounts found!");
			}
			return Result<tOdooAccount>.Success(account);
		}
		public Task<List<tOdooAccount>> LoadAllAccountAsync()
		{
			return Task.Run(() => _context.tOdooAccounts.ToList());
		}

		public async Task<bool> DeleteAsync(tOdooAccount account)
		{
			var entry = await _context.tOdooAccounts.FindAsync(account.Id);
			if (entry != null)
			{
				_context.tOdooAccounts.Remove(entry);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<bool> AddAsync(tOdooAccount account)
		{
			_context.tOdooAccounts.Add(account);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
