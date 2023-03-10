using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LR3_WebDesign.Models;

namespace LR3_WebDesign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BankSystemContext _context;

        public AccountsController(BankSystemContext context)
        {
            _context = context;
        }


        private static AccountDTO ToAccountDTOMap(Account acc)
        {
            return new AccountDTO()
            {
                AccountNumber = acc.AccountNumber,
                Usreou = acc.Usreou,
                Itn = acc.Itn,
                CreditSum = acc.CreditSum,
                Balance = acc.Balance,
                Currency = acc.Currency,
                //BankName = _context.Banks.Where(x => x.Usreou == acc.Usreou).Select(x => x.Name).FirstOrDefault(),
                //CustomerName = _context.Customers.Where(x => x.Itn == acc.Itn).Select(x => x.FirstName).FirstOrDefault()
                BankName = acc.UsreouNavigation.Name,
                CustomerName = acc.ItnNavigation.LastName
            };
        }


        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var accountsDTO = _context.Accounts
            .Include(x => x.ItnNavigation)
            .Include(y => y.UsreouNavigation)
            .Select(x => ToAccountDTOMap(x));
            return await accountsDTO.ToListAsync();
        }


        //public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        //{
        //  if (_context.Accounts == null)
        //  {
        //      return NotFound();
        //  }
        //    //var account = await _context.Accounts.ToListAsync();
        //    var account = _context.Accounts.Include(a => a.ItnNavigation).Include(a => a.UsreouNavigation);

        //    return await account.ToListAsync();
        //}

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(string id, AccountDTO acc)
        {
            var bank = _context.Banks.Where(x => x.Usreou == acc.Usreou).FirstOrDefault();
            var customer = _context.Customers.Where(x => x.Itn == acc.Itn).FirstOrDefault();
            _context.Entry(new Account
            {
                AccountNumber = id,
                Usreou = acc.Usreou,
                Itn = acc.Itn,
                Currency = acc.Currency,
                Balance = acc.Balance,
                CreditSum = acc.CreditSum,
                ItnNavigation = _context.Customers.Where(x => x.Itn == acc.Itn)
                .FirstOrDefault(),
                UsreouNavigation = _context.Banks.Where(x => x.Usreou == acc.Usreou)
                .FirstOrDefault()
            }).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        //public async Task<IActionResult> PutAccount(string id, Account account)
        //{
        //    if (id != account.AccountNumber)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(account).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AccountExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(AccountDTO acc)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'BankSystemContext.Accounts'  is null.");
            }
            var bank = _context.Banks.Where(x => x.Usreou == acc.Usreou).FirstOrDefault();
            var customer = _context.Customers.Where(x => x.Itn == acc.Itn).FirstOrDefault();
            _context.Accounts.Add(new Account
            {
                AccountNumber = acc.AccountNumber,
                Usreou = acc.Usreou,
                Itn = acc.Itn,
                Currency = acc.Currency,
                Balance = acc.Balance,
                CreditSum = acc.CreditSum,
                ItnNavigation = _context.Customers.Where(x => x.Itn == acc.Itn)
                .FirstOrDefault(),
                UsreouNavigation = _context.Banks.Where(x => x.Usreou == acc.Usreou)
                .FirstOrDefault()
            });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountExists(acc.AccountNumber))
                {
                    return Conflict();
                }
            }

            return CreatedAtAction("GetAccount", new { id = acc.AccountNumber }, acc);
        }



        //public async Task<ActionResult<Account>> PostAccount(Account account)
        //{
        //  if (_context.Accounts == null)
        //  {
        //      return Problem("Entity set 'BankSystemContext.Accounts'  is null.");
        //  }
        //    _context.Accounts.Add(account);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (AccountExists(account.AccountNumber))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetAccount", new { id = account.AccountNumber }, account);
        //}

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(string id)
        {
            return (_context.Accounts?.Any(e => e.AccountNumber == id)).GetValueOrDefault();
        }
    }
}
