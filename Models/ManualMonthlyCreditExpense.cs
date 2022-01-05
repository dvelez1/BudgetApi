using System;
using System.Collections.Generic;

#nullable disable

namespace BudgetApi.Models
{
    public partial class ManualMonthlyCreditExpense
    {
        public int ManualMonthlyCreditExpensesId { get; set; }
        public int MasMonthlyExpensesId { get; set; }
        public decimal Cost { get; set; }
        public decimal Payment { get; set; }
        public string Description { get; set; }

        public virtual MasMonthlyExpense MasMonthlyExpenses { get; set; }
    }
}
