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
		private readonly Middleware_dbContext _context;

		public OdooAccountService(Middleware_dbContext context)
		{
			_context = context;
		}

		public async Task<Result<OdooAccount>> GetFirstAccount()
		{
			var account = await _context.OdooAccounts.FirstOrDefaultAsync();
			if (account is null)
			{
				return Result<OdooAccount>.Failure("No Odoo Accounts found!");
			}
			return Result<OdooAccount>.Success(account);
		}
		public Task<List<OdooAccount>> LoadAllAccountAsync()
		{
			return Task.Run(() => _context.OdooAccounts.ToList());
		}

		public async Task<bool> DeleteAsync(OdooAccount account)
		{
			var entry = await _context.OdooAccounts.FindAsync(account.Id);
			if (entry != null)
			{
				_context.OdooAccounts.Remove(entry);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<bool> AddAsync(OdooAccount account)
		{
			_context.OdooAccounts.Add(account);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
