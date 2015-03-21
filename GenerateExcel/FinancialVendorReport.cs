using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateExcel
{
    public class FinancialVendorReport
    {
        private string vendorName;
        private decimal incomes;
        private decimal expenses;
        private decimal totalTaxes;
        private decimal financialResult;

        public FinancialVendorReport(string vendorName, decimal incomes, decimal expenses, decimal totalTaxes )
        {
            this.vendorName = vendorName;
            this.incomes = incomes;
            this.expenses = expenses;
            this.totalTaxes = totalTaxes;

            this.financialResult = incomes - expenses - totalTaxes;
        }

        public string VendorName 
        {
            get { return this.vendorName; } 
        }

        public decimal Incomes
        {
            get { return this.incomes; }
        }

        public decimal Expenses
        {
            get { return this.expenses; }
        }

        public decimal TotalTaxes
        {
            get { return this.totalTaxes; }
        }

        public decimal FinancialResult
        {
            get { return this.financialResult; }
        }

    }
}
