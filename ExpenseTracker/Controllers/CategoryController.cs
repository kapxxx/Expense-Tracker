using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ExpenseTracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        // ----------------------this will show all Category--------------------
       
        public IActionResult Index()
        { 
            var data = _context.Categories.ToList();       
            return View(data);
        }
        
        //Get request
        public IActionResult Create()
        {
            if (_context.TotalExpenseLimit.IsNullOrEmpty())
            {
                TempData["Warning"] = "First Set Your Expnse Limit..!";
                return Redirect("/Dashboard/Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            
              var tel = _context.TotalExpenseLimit.Take(1).Single();
              var sumofcolum = _context.Categories.Select(a => a.C_Expense_Limit).Sum();
              var total_sum = sumofcolum + category.C_Expense_Limit;

              if (tel.Total_ExpenseLimit < total_sum)
              {
                    var masage = $"You can't set your expense limit more then your Total Expense Limit. If you want then first expend your Total Expense Limit!";
                    TempData["Limit"] = masage;
                    return View(category);
              } 
            
            if (!ModelState.IsValid)
            {            
                return View(category);
            }

            if(_context.Categories.Any(k=>k.C_Name == category.C_Name))
            {
                ModelState.AddModelError("C_Name ", "Category name is allready exist !");
                return View(category);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["suscces"] = "Category Added successfully..!";
            return Redirect("/Dashboard/Index");
        }

        
       // ----------------------------------update-----------------------------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cat = _context.Categories.FirstOrDefault(x => x.C_Id == id);
            if (cat != null)
            {
                var viewModel = new UpdateCategoryViewModel()
                {
                    C_Id = cat.C_Id,
                    C_Name = cat.C_Name,
                    C_Expense_Limit = cat.C_Expense_Limit

                };
                return View(viewModel);
              // return Ok(viewModel);

            }
           // return RedirectToAction("Index");
           return Redirect("/Dashboard/Index");

        }
      //  -----------------------------update-----------------------
         [HttpPost]
         public IActionResult Edit(UpdateCategoryViewModel category, int id)
         {
            if (_context.TotalExpenseLimit.IsNullOrEmpty())
            {
                TempData["Warning"] = "First Set Your Expnse Limit..!";
                return Redirect("/Dashboard/Index");
            }
            else
            {
                var tel = _context.TotalExpenseLimit.Take(1).Single();
                var sumofcolum = _context.Categories.Select(a => a.C_Expense_Limit).Sum();
                var previus_C_limit = _context.Categories.FirstOrDefault(x => x.C_Id == id);
                var demo = previus_C_limit.C_Expense_Limit;
                sumofcolum = sumofcolum - demo;
                var total_sum = sumofcolum + category.C_Expense_Limit;
                if (tel.Total_ExpenseLimit < total_sum)
                {
                    var masage = $"Your Total Expense Limit is {tel.Total_ExpenseLimit} & You can't set your expense limit more then your Total Expense Limit.!";
                    TempData["Limit"] = masage;
                    return View(category);
                }
            }
            if (ModelState.IsValid)
            {
                var dbcategoty = _context.Categories.FirstOrDefault(s => s.C_Id.Equals(id));
                dbcategoty.C_Name = category.C_Name;
                dbcategoty.C_Expense_Limit = category.C_Expense_Limit;
                _context.SaveChanges();
                TempData["updated"] = "Category Update Succesfully..!";
                return Redirect("/Dashboard/Index");
            }
            return View(category);
        }

        // ------------------Delete--------------------
        public IActionResult Delete(int id)
        {
            var deleterecord = _context.Categories.Find(id);

            _context.Categories.Remove(deleterecord);
            _context.SaveChanges();
            TempData["deleted"] = "Category Deleted...!";
            return Redirect("/Dashboard/Index");
        }
        

    }
}
 