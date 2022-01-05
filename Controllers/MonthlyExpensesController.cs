using BudgetApi.Context;
using BudgetApi.Models;
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
    public class MonthlyExpensesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public MonthlyExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("api/[controller]/GetMasMonthlyExpeses")]
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

        [HttpPost]
        [Route("api/[controller]/GetMasMonthlyExpesesById")]
        public async Task<IActionResult> GetMasMonthlyExpesesById(MasMonthlyExpense masMonthlyExpense)
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
        [Route("api/[controller]/PostMasMonthlyExpenses")]
        public async Task<IActionResult> PostMasMonthlyExpenses([FromBody] MasMonthlyExpense masMonthlyExpense)
        {
            try
            {
                _context.MasMonthlyExpenses.Add(masMonthlyExpense);
                int masMonthlyExpensesIdentity = await _context.SaveChangesAsync();

                var masExpenses = await _context.MasExpenses.
                    Where(s => s.BiweeklyNumber == masMonthlyExpense.BiweeklyNumber).
                    ToListAsync();

                var result = await PostMonthlyExpensesAsync(masMonthlyExpensesIdentity, masExpenses);

                if (result)
                    return Ok(new { message = "Transaction successfully!" });
                else
                    return BadRequest("Error");
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
        private async Task<bool> PostMonthlyExpensesAsync(int masMonthlyExpensesIdentity, List<MasExpense> masExpenses)
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

    }
}
