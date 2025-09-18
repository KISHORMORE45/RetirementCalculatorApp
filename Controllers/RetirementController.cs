using Microsoft.AspNetCore.Mvc;
using RetirementCalculatorApp.Models;
using RetirementCalculatorApp.Services;

namespace RetirementCalculatorApp.Controllers
{
    public class RetirementController : Controller
    {
        private readonly RetirementService _service;

        public RetirementController()
        {
            _service = new RetirementService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new RetirementModel());
        }

        [HttpPost]
        public IActionResult Index(RetirementModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _service.CalculateRetirement(model.currentAge,
                                                              model.RetirementAge,
                                                              model.LifeExpectancy,
                                                              model.CurrentMonthlyExpense,
                                                              (double)model.InflationRate,
                                                              (double)model.PreRetirementReturn,
                                                              (double)model.PostRetirementReturn,
                                                              model.CurrentSavings,
                                                              model.MonthlySIP,
                                                              model.AnnualSipIncreasePercent
                                                            );


                    model.ExpenseAtRetirement = result.expense;
                    model.CorpusRequired = result.corpus;
                    model.FutureValueOfInvestments = result.fv;
                    model.GapOrSurplus = result.gap;
                    model.StepUpFutureValue = result.stepUpFV;
                }
                catch (Exception ex)
                {
                    model.ErrorMessage = ex.Message;
                }
            }
            return View(model);
        }
    }
}
