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
    public class ManualMonthlyExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ManualMonthlyExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetManualMonthlyExpensesByMasMonthlyExpensesId(int masMonthlyExpensesId)
        {
            try
            {
                var manualMonthlyExpenses = await _context.ManualMonthlyExpenses
                    .Where(s => s.MasMonthlyExpensesId == masMonthlyExpensesId).ToListAsync();

                return Ok(manualMonthlyExpenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateManualMonthlyExpenses([FromBody] ManualMonthlyExpense manualMonthlyExpense)
        {
            try
            {
                _context.ManualMonthlyExpenses.Add(manualMonthlyExpense);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Transaction inserted successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{manualMonthlyExpensesId}")]
        public async Task<IActionResult> UpdatetManualMonthlyExpenses(int manualMonthlyExpensesId, [FromBody] ManualMonthlyExpense manualMonthlyExpense)
        {
            try
            {
                if (manualMonthlyExpensesId != manualMonthlyExpense.ManualMonthlyExpensesId)
                    return NotFound();

                var entity = await _context.ManualMonthlyExpenses.FindAsync(manualMonthlyExpensesId);

                if (entity == null)
                    return NotFound();

                _context.Entry(entity).CurrentValues.SetValues(manualMonthlyExpense);

                await _context.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{manualMonthlyExpensesId}")]
        public async Task<IActionResult> Delete(int manualMonthlyExpensesId)
        {
            try
            {
                var manualMonthlyExpense = await _context.ManualMonthlyExpenses.FindAsync(manualMonthlyExpensesId);

                if (manualMonthlyExpense == null)
                    return NotFound();

                _context.ManualMonthlyExpenses.Remove(manualMonthlyExpense);
                await _context.SaveChangesAsync();

                return Ok(manualMonthlyExpense);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
