WebAPI .Net 5.0 / Entity Framework 5.0
Objective: Manage home Budget by Year/Month/Biweekly
****************************
Database Main compound Key
Year: ####
Month: 1 to 12
Biweekly:  1 or 2
****************************
Tables:
[mas_monthly_expenses] - Main or Parent Table: Year/Month/Biweekly/Income
[mas_expenses] - Master table of Biweekly expenses (debts & savings)
[monthly_expenses] - Debts and saving register by Biweekly. Come from [mas_expenses] 
[manual_monthly_expenses] - additional expenses Biweekly. 
[manual_monthly_credit_expenses] - Biweekly creidt expenses
