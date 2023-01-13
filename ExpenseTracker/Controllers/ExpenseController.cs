using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _context;

        public ExpenseController(AppDbContext context)
        {
            _context = context;
        }
        //------------------------this will show all the expenses----------------------
        public IActionResult Index()
        {
            var data = _context.Expenses.Include(n=> n.Category).ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult GetExpenseBycategoryId(int Categoryid)
        {
            var exp = _context.Expenses.Where(exp => exp.C_Id == Categoryid).ToList();
             return View(exp);
        }

        public IActionResult Create()
        {
            
            var items = _context.Categories.Select(x => new Category()
            {
                C_Id = x.C_Id,
                C_Name = x.C_Name
            }).ToList();

            ViewBag.cat_list = new SelectList(items, "C_Id", "C_Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Expense exp)
        {
            if (!ModelState.IsValid)
            {
                if (exp.Category == null)
                {
                    var items2 = _context.Categories.Select(x => new Category()
                    {
                        C_Id = x.C_Id,
                        C_Name = x.C_Name
                    }).ToList();
                    ViewBag.cat_list = new SelectList(items2, "C_Id", "C_Name");
                    return View(exp);

                }
                var items = _context.Categories.Select(x => new Category()
                {
                    C_Id = x.C_Id,
                    C_Name = x.C_Name
                }).ToList();
                ViewBag.cat_list = new SelectList(items, "C_Id", "C_Name");
                return View(exp);    
            }

            var  cat_obj = (from i in _context.Categories where i.C_Id==exp.C_Id select new { i.C_Expense_Limit }).Single();
            var cat_limit = cat_obj.C_Expense_Limit;
            var sumofcolum = (from j in _context.Expenses where j.C_Id == exp.C_Id select j.E_Amount).Sum(); 
            var total_sum = sumofcolum + exp.E_Amount;

            if(total_sum > cat_limit)
            {

                var masage = $"You Reached Your Category Limit & If You Want to add Amount then Expend your Category limit First !";
                TempData["XLimit"] = masage;

                var items = _context.Categories.Select(x => new Category()
                {
                    C_Id = x.C_Id,
                    C_Name = x.C_Name
                }).ToList();
                ViewBag.cat_list = new SelectList(items, "C_Id", "C_Name");

                return View(exp);
            }

            
            _context.Expenses.Add(exp);
            _context.SaveChanges();
            TempData["suscces"] = "Expense Added successfully..!";
            return Redirect("/Dashboard/Index");
        }

        // -------------------------update table-----------------------

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cat = _context.Expenses.FirstOrDefault(x => x.E_Id == id);
            if (cat != null)
            {

                var viewModel = new UpdateExpenceViewModel()
                {
                    E_Id = cat.E_Id,
                    E_Title = cat.E_Title,
                    E_Amount = cat.E_Amount,
                    E_Description = cat.E_Description,
                    C_id = cat.C_Id,
                    
                };

                var items = _context.Categories.Select(x => new Category()
                {
                    C_Id = x.C_Id,
                    C_Name = x.C_Name
                }).ToList();

                ViewBag.cat_list = new SelectList(items, "C_Id", "C_Name");
                
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(UpdateExpenceViewModel expense, int id)
        {
            var cat_obj = (from i in _context.Categories where i.C_Id == expense.C_id select new { i.C_Expense_Limit }).Single();
            var cat_limit = cat_obj.C_Expense_Limit;

            var sumofcolum = (from j in _context.Expenses where j.C_Id == expense.C_id select j.E_Amount).Sum();
            var row= _context.Expenses.FirstOrDefault(x => x.E_Id == id);
            var value = row.E_Amount;

            sumofcolum = sumofcolum - value;

            var total_sum = sumofcolum + expense.E_Amount;

            if (total_sum > cat_limit)
            {

                var masage = $"You Reached Your Category Limit & If You Want to add Amount then Expend your Category limit First !";
                TempData["XLimit"] = masage;

                var i = _context.Categories.Select(x => new Category()
                {
                    C_Id = x.C_Id,
                    C_Name = x.C_Name
                }).ToList();
                ViewBag.cat_list = new SelectList(i, "C_Id", "C_Name");

                return View(expense);
            }
            if (ModelState.IsValid)
            {
                var dbexpense = _context.Expenses.FirstOrDefault(s => s.E_Id.Equals(id));
                dbexpense.E_Title = expense.E_Title;
                dbexpense.E_Description = expense.E_Description;
                dbexpense.E_Amount = expense.E_Amount;
                dbexpense.C_Id = expense.C_id;

                _context.SaveChanges();
                TempData["updated"] = "Expense Update Succesfully..!";

                return Redirect("/Dashboard/Index");

                // return RedirectToAction("Index");
            }

            var items = _context.Categories.Select(x => new Category()
            {
                C_Id = x.C_Id,
                C_Name = x.C_Name
            }).ToList();

            ViewBag.cat_list = new SelectList(items, "C_Id", "C_Name");
            return View(expense);
        }

        // ------------------Delete--------------------
        public IActionResult Delete(int id)
        {
            var deleterecord = _context.Expenses.Find(id);

            _context.Expenses.Remove(deleterecord);
            _context.SaveChanges();
            TempData["deleted"] = "Expense Deleted...!";
            return Redirect("/Dashboard/Index");

        }

    }
}
