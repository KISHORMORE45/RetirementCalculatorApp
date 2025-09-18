namespace RetirementCalculatorApp.Services
{
    public class RetirementService
    {
        public (decimal expense, decimal corpus, decimal fv, decimal gap, decimal stepUpFV) CalculateRetirement
                                        (int currentAge, int retirementAge, int lifeExpectancy,
                                        decimal currentExpense, double inflationRate,
                                        double preReturn, double postReturn,
                                        decimal currentSavings, decimal sip,
                                        double annualSipIncreasePercent)
        {
            try
            {
                // Calculate the number of years left until retirement
                int yearsToRetirement = retirementAge - currentAge;
                if (yearsToRetirement <= 0)
                {
                    throw new Exception("Retirement age must be greater than current age.");
                }

                // Step 1: Calculate the expected monthly expense at the time of retirement, adjusted for inflation
                decimal expenseAtRetirement = currentExpense * (decimal)Math.Pow(1 + inflationRate / 100, yearsToRetirement);

                // Step 2: Calculate the corpus (lump sum at retirement) required at retirement to cover expenses until life expectancy
                int postRetirementYears = lifeExpectancy - retirementAge;
                int monthsPostRetirement = postRetirementYears * 12;
                double postRate = postReturn / 12 / 100;

                // Present value of an annuity formula to calculate required corpus
                double corpus = (double)expenseAtRetirement * ((1 - Math.Pow(1 + postRate, -monthsPostRetirement)) / postRate);

                // Step 3: Calculate the future value of current savings and SIPs at retirement
                int monthsToRetirement = yearsToRetirement * 12;
                double preRate = preReturn / 12 / 100;
                // Future value of SIPs (recurring investments)
                double fvSip = (double)sip *
                               ((Math.Pow(1 + preRate, monthsToRetirement) - 1) / preRate) * (1 + preRate);

                // Future value of current savings (lump sum)
                double fvSavings = (double)currentSavings * Math.Pow(1 + preRate, monthsToRetirement);
                double totalFV = fvSip + fvSavings;

                // Step 4: Calculate the gap between required corpus and projected future value
                // Without Step-Up SIP
                decimal gap = (decimal)corpus - (decimal)totalFV;


                // 5. Step-Up SIP FV (Annual increment in SIP)
                double stepUpFV = 0;
                double yearlySip = (double)sip * 12; // annual SIP
                double annualReturn = preReturn / 100;

                for (int year = 1; year <= yearsToRetirement; year++)
                {
                    stepUpFV += yearlySip * Math.Pow(1 + annualReturn, yearsToRetirement - year + 1);
                    yearlySip *= (1 + annualSipIncreasePercent / 100); // Increase SIP each year
                }
                stepUpFV += fvSavings; // Add savings



                // Return rounded results for expense at retirement, required corpus, future value, and gap
                return (Math.Round(expenseAtRetirement, 2),
                        Math.Round((decimal)corpus, 2),
                        Math.Round((decimal)totalFV, 2),
                        Math.Round(gap, 2),
                    Math.Round((decimal)stepUpFV, 2)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Retirement calculation: {ex.Message}");

            }

        }
    }
}
