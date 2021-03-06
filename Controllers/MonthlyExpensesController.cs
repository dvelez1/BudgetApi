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
    public class MonthlyExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MonthlyExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlyExpesesByMasMonthlyExpensesId(int masMonthlyExpensesId)
        {
            try
            {
                var monthlyExpenses = await _context.MonthlyExpenses
                    .Where(s => s.MasMonthlyExpensesId == masMonthlyExpensesId).
                    ToListAsync();
                // me-> Monthly Expense / mase ->MasExpenses
                var monthlyExpenseViewModels = monthlyExpenses.
                    Join(_context.MasExpenses, me => me.MasExpensesId, mase => mase.MasExpensesId,
                    (me, mase) => new { me, mase }).
                    Where(m => m.me.MasExpensesId == m.mase.MasExpensesId).
                    Select(m => new MonthlyExpenseViewModel
                    {
                        MontlyExpensesId = m.me.MontlyExpensesId,
                        MasMonthlyExpensesId = m.me.MasMonthlyExpensesId,
                        MasExpensesId = m.me.MasExpensesId,
                        MasExpensesDescription = m.mase.Description,
                        Budget = m.me.Budget,
                        Payment = m.me.Payment,
                    }).ToList();

                if (monthlyExpenseViewModels == null)
                    return NotFound();

                return Ok(monthlyExpenseViewModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{montlyExpensesId}")]
        public async Task<IActionResult> UpdatetMonthlyExpeses(int montlyExpensesId, [FromBody] MonthlyExpense monthlyExpense)
        {
            try
            {
                if (montlyExpensesId != monthlyExpense.MontlyExpensesId)
                    return NotFound();

                var entity = await _context.MonthlyExpenses.FindAsync(montlyExpensesId);

                if (entity == null)
                    return NotFound();

                _context.Entry(entity).CurrentValues.SetValues(monthlyExpense);

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
