using BudgetApi.Context;
using BudgetApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public MasExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("CreateMasExpenses")]
        public async Task<IActionResult> CreateMasExpenses([FromBody] MasExpense masExpense)
        {
            try
            {
                _context.MasExpenses.Add(masExpense);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Transaction inserted successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{masExpensesId}")]
        public async Task<IActionResult> UpdatetMasExpenses(int masExpensesId, [FromBody] MasExpense masExpense)
        {
            try
            {
                if (masExpensesId != masExpense.MasExpensesId)
                    return NotFound();

                var entity = await _context.MasExpenses.FindAsync(masExpensesId);

                if (entity == null)
                    return NotFound();

                _context.Entry(entity).CurrentValues.SetValues(masExpense);

                await _context.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{masExpensesId}")]
        public async Task<IActionResult> Delete(int masExpensesId)
        {
            try
            {
                var entity = await _context.MasExpenses.FindAsync(masExpensesId);
                entity.Active = false;

                if (entity == null)
                    return NotFound();

                _context.Entry(entity).CurrentValues.SetValues(entity);

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
