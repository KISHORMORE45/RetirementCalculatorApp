using System.ComponentModel.DataAnnotations;

namespace RetirementCalculatorApp.Models
{
    public class RetirementModel
    {
        [Required]
        [Range(18, 100, ErrorMessage = "Current age must be between 18 and 100.")]
        public int currentAge { get; set; }         // in years

        [Required]
        [Range(18, 100, ErrorMessage = "Retirement age must be between 18 and 100.")]
        public int RetirementAge { get; set; }      // in years

        [Required]
        [Range(1, 120, ErrorMessage = "Life expectancy must be between 1 and 120.")]
        public int LifeExpectancy { get; set; }     // in years (How long you expect to live after retirement)

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Current monthly expense must be non-negative.")]
        public decimal CurrentMonthlyExpense { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Inflation rate must be between 0 and 100.")]
        public decimal InflationRate { get; set; }      // in percentage (The expected yearly increase in cost of living)

        [Required]
        [Range(0, 100, ErrorMessage = "Pre-retirement return must be between 0 and 100.")]
        public decimal PreRetirementReturn { get; set; } // in percentage (The expected yearly return on investments before retirement)

        [Required]
        [Range(0, 100, ErrorMessage = "Post-retirement return must be between 0 and 100.")]
        public decimal PostRetirementReturn { get; set; } // in percentage (The expected yearly return on investments after retirement)

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Current savings must be non-negative.")]
        public decimal CurrentSavings { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Monthly SIP must be non-negative.")]
        public decimal MonthlySIP { get; set; }



        // New property for Step-Up SIP
        public double AnnualSipIncreasePercent { get; set; } // in percentage (The expected yearly increase in SIP amount)  



        // Outputs
        public decimal ExpenseAtRetirement { get; set; }
        public decimal CorpusRequired { get; set; }         // Total amount needed at retirement to cover expenses
        public decimal FutureValueOfInvestments { get; set; }
        public decimal GapOrSurplus { get; set; }           // Positive value indicates a surplus, negative indicates a gap
        public string ErrorMessage { get; set; } = string.Empty;

        public decimal StepUpFutureValue { get; set; }      // New property
    }
}
