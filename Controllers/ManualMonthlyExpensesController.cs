using BudgetApi.Context;
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
        [Route("api/[controller]/GetManualMonthlyExpenses")]
        public async Task<IActionResult> GetManualMonthlyExpenses()
        {
            try
            {
                var manualMonthlyExpenses = await _context.ManualMonthlyExpenses.ToListAsync();
                return Ok(manualMonthlyExpenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/GetManualMonthlyExpensesByMasMonthlyExpensesId")]
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


        

    }
}
