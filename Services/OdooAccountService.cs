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

		public async Task<Result<TOdooAccount>> GetFirstAccount()
		{
			var account = await _context.TOdooAccounts.FirstOrDefaultAsync();
			if (account is null)
			{
				return Result<TOdooAccount>.Failure("No Odoo Accounts found!");
			}
			return Result<TOdooAccount>.Success(account);
		}
		public Task<List<TOdooAccount>> LoadAllAccountAsync()
		{
			return Task.Run(() => _context.TOdooAccounts.ToList());
		}

		public async Task<bool> DeleteAsync(TOdooAccount account)
		{
			var entry = await _context.TOdooAccounts.FindAsync(account.Id);
			if (entry != null)
			{
				_context.TOdooAccounts.Remove(entry);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<bool> AddAsync(TOdooAccount account)
		{
			_context.TOdooAccounts.Add(account);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
