using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApi.Context
{

    [Route("api/[controller]")]
    [ApiController]
    public class MasterTablesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MasterTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/[controller]/GetMasExpensesList")]
        public async Task<IActionResult> GetMasExpensesList()
        {
            try
            {
                var masExpenses = await _context.MasExpenses.ToListAsync();
                return Ok(masExpenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetMasExpenses")]
        public async Task<IActionResult> GetMasExpenses(int id)
        {
            try
            {
                var masExpenses = await _context.MasExpenses.FindAsync(id);
                if (masExpenses == null)
                    return NotFound();
                    
                return Ok(masExpenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
