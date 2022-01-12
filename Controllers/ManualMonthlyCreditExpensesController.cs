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
    public class ManualMonthlyCreditExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ManualMonthlyCreditExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetManualMonthlyCreditExpensesByMasMonthlyExpensesId(int masMonthlyExpensesId)
        {
            try
            {
                var manualMonthlyCreditExpenses = await _context.ManualMonthlyCreditExpenses
                    .Where(s => s.MasMonthlyExpensesId == masMonthlyExpensesId).ToListAsync();

                return Ok(manualMonthlyCreditExpenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateManualCreditMonthlyExpenses([FromBody] ManualMonthlyCreditExpense manualMonthlyCreditExpense)
        {
            try
            {
                _context.ManualMonthlyCreditExpenses.Add(manualMonthlyCreditExpense);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Transaction inserted successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{manualMonthlyCreditExpensesId}")]
        public async Task<IActionResult> UpdatetManualMonthlyCreditExpenses(int manualMonthlyCreditExpensesId, [FromBody] ManualMonthlyCreditExpense manualMonthlyCreditExpense)
        {
            try
            {
                if (manualMonthlyCreditExpensesId != manualMonthlyCreditExpense.ManualMonthlyCreditExpensesId)
                    return NotFound();

                var entity = await _context.ManualMonthlyCreditExpenses.FindAsync(manualMonthlyCreditExpensesId);

                if (entity == null)
                    return NotFound();

                _context.Entry(entity).CurrentValues.SetValues(manualMonthlyCreditExpense);

                await _context.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
