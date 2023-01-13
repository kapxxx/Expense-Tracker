using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Controllers
{
    public class TotalExpenseLimitController : Controller
    {
        private readonly AppDbContext _context;

        public TotalExpenseLimitController(AppDbContext context)
        {
            _context = context;
        }
        // ----------------------this will show all Category--------------------

        public IActionResult Index()
        {
            var data = _context.TotalExpenseLimit.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            var list = _context.TotalExpenseLimit.ToList();
            if (list.Count > 0)
            {
                TempData["Warning"] = "You can't add more then 1 expense limit";
                return Redirect("/Dashboard/Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(TotalExpenseLimit totalexpenselimit)
        {
            

            if (!ModelState.IsValid)
            {
                return View(totalexpenselimit);
            }

            _context.TotalExpenseLimit.Add(totalexpenselimit);
            _context.SaveChanges();
            TempData["TotalExpeselimit"] = "Expense Limit Added successfully..!";
            return Redirect("/Dashboard/Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (_context.TotalExpenseLimit.IsNullOrEmpty())
            {
                TempData["Warning"] = "First Set Your Expnse Limit..!";
                return Redirect("/Dashboard/Index");
            }
            var tl = _context.TotalExpenseLimit.FirstOrDefault(x => x.Total_ExpenseLimit_Id == id);
            if (tl != null)
            {
                var viewModel = new TotalExpenseLimit()
                {
                    Total_ExpenseLimit_Id = tl.Total_ExpenseLimit_Id,
                    Total_ExpenseLimit = tl.Total_ExpenseLimit
                };
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        //  -----------------------------update-----------------------
        [HttpPost]
        public IActionResult Edit(TotalExpenseLimit tel, int id)
        {
            if (ModelState.IsValid)
            {
                var dbtl = _context.TotalExpenseLimit.FirstOrDefault(x => x.Total_ExpenseLimit_Id.Equals(id));
                dbtl.Total_ExpenseLimit = tel.Total_ExpenseLimit;
                _context.SaveChanges();
                return Redirect("/Dashboard/Index");
            }

            return View(tel);
        }

        // ------------------Delete--------------------
        public IActionResult Delete(int id)
        {
            var deleterecord = _context.TotalExpenseLimit.Find(id);

            _context.TotalExpenseLimit.Remove(deleterecord);
            _context.SaveChanges();
            return Redirect("/Dashboard/Index");
        }


    }
}
