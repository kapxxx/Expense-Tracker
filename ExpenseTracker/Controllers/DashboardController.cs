using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public DashboardController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index(int? id)
        {
                if (_context.TotalExpenseLimit.IsNullOrEmpty())
                {
                    TempData["Limit"] = 0;
                }
                else
                {
                var tel = _context.TotalExpenseLimit.Take(1).Single();
                if (tel == null)
                {
                    TempData["Limit"] = 0;
                }
                else
                {
                    var masage = tel.Total_ExpenseLimit;
                    var limitId = tel.Total_ExpenseLimit_Id;

                    TempData["Limit"] = masage;
                    TempData["LimitId"] = limitId;

                }
            }

              

            var YouTotalExpense = (from j in _context.Expenses select j.E_Amount).Sum();
            TempData["YouTotalExpense"] = YouTotalExpense;

            MultyClass m = new MultyClass();
            if (id==null)
            {      
                m.cat = _context.Categories.ToList();
                m.exp = _context.Expenses.ToList();
            }
            else
            {
                m.cat = _context.Categories.ToList();
                m.exp = _context.Expenses.Where(a=> a.C_Id==id).ToList();
            }

            return View(m);
        }

        public PartialViewResult GetExpenseBycategory(int Categoryid)
        {
            var exp = _context.Expenses.Where(exp => exp.C_Id == Categoryid).ToList();
          //  TempData["exp"]=exp;
            return PartialView("_expenses", exp);
        }
    }
}
