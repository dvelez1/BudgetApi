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
                    .Where(s=>s.Year == masMonthlyExpense.Year 
                    && s.Month == masMonthlyExpense.Month 
                    && s.BiweeklyNumber == masMonthlyExpense.BiweeklyNumber).FirstOrDefaultAsync();

                return Ok(masMonthlyExpenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/PostMasMonthlyExpenses")]
        public async Task<IActionResult> PostMasMonthlyExpenses([FromBody] MasMonthlyExpense masMonthlyExpense)
        {
            try
            {
                _context.MasMonthlyExpenses.Add(masMonthlyExpense);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Transaction successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
