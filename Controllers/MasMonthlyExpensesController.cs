using BudgetApi.Context;
using BudgetApi.Models;
using BudgetApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasMonthlyExpensesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public MasMonthlyExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("GetMasMonthlyExpeses")]
        public async Task<IActionResult> GetMasMonthlyExpeses()
        {
            try
            {
                var masMonthlyExpenses = await _context.MasMonthlyExpenses.ToListAsync();
                return Ok(masMonthlyExpenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetYearWithBudget")]
        public async Task<IActionResult> GetYearWithBudget()
        {
            try
            {
                var masMonthlyExpenses = await _context.MasMonthlyExpenses.ToListAsync();
                var yearsWithBudgetViewModels = masMonthlyExpenses.Select(m => new YearsWithBudgetViewModel
                {
                    Year = m.Year,
                }).ToList();
                return Ok(yearsWithBudgetViewModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("GetMasMonthlyExpensesByParameters")]
        public async Task<IActionResult> GetMasMonthlyExpensesByParameters([FromBody] MasMonthlyExpense masMonthlyExpense)
        {
            try
            {
                var masMonthlyExpenses = await _context.MasMonthlyExpenses
                    .Where(s => s.Year == masMonthlyExpense.Year
                    && s.Month == masMonthlyExpense.Month
                    && s.BiweeklyNumber == masMonthlyExpense.BiweeklyNumber).
                    FirstOrDefaultAsync();

                return Ok(masMonthlyExpenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create new Entry on table MasMonthlyExpenses 
        /// </summary>
        /// <param name="masMonthlyExpense"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("api/[controller]/CreatetMasMonthlyExpenses")]
        public async Task<IActionResult> CreatetMasMonthlyExpenses([FromBody] MasMonthlyExpense masMonthlyExpense)
        {
            try
            {
                _context.MasMonthlyExpenses.Add(masMonthlyExpense);
                await _context.SaveChangesAsync();
                // Return identity after insert Operation
                int masMonthlyExpensesIdentity = masMonthlyExpense.MasMonthlyExpensesId;

                var masExpenses = await _context.MasExpenses.
                    Where(s => s.BiweeklyNumber == masMonthlyExpense.BiweeklyNumber).
                    ToListAsync();

                var result = await CreatetMonthlyExpensesAsync(masMonthlyExpensesIdentity, masExpenses);

                if (result)
                    return Ok(new { message = "Transaction inserted successfully!" });
                else
                {
                    // TODO: Pending Rollback Implementation
                    return BadRequest("Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create New Entry on table MonthlyExpenses. Method called from PostMasMonthlyExpenses
        /// </summary>
        /// <param name="masMonthlyExpensesIdentity"></param>
        /// <param name="masExpenses"></param>
        /// <returns></returns>
        private async Task<bool> CreatetMonthlyExpensesAsync(int masMonthlyExpensesIdentity, List<MasExpense> masExpenses)
        {
            try
            {
                List<MonthlyExpense> monthlyExpenses = await _context.MonthlyExpenses.ToListAsync();

                foreach (var item in masExpenses)
                {
                    monthlyExpenses.Add(new MonthlyExpense { MasMonthlyExpensesId = masMonthlyExpensesIdentity, MasExpensesId = item.MasExpensesId });
                }

                await _context.MonthlyExpenses.AddRangeAsync(monthlyExpenses);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPut]
        //[Route("api/[controller]/UpdatetMasMonthlyExpenses")]
        public async Task<IActionResult> UpdatetMasMonthlyExpenses(int MasMonthlyExpensesId, [FromBody] MasMonthlyExpense masMonthlyExpense)
        {
            try
            {
                if (MasMonthlyExpensesId != masMonthlyExpense.MasMonthlyExpensesId)
                    return NotFound();

                var entity = await _context.MasMonthlyExpenses.FindAsync(MasMonthlyExpensesId);

                if (entity == null)
                    return NotFound();

                _context.Entry(entity).CurrentValues.SetValues(masMonthlyExpense);

                await _context.SaveChangesAsync();

                return Ok(new { message = "Transaction updated successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
