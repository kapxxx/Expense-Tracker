using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
	public class UpdateCategoryViewModel
	{
        public int C_Id { get; set; }
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Only alphabet allowed!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "name length should be beween 3 to 20")]
        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        public string C_Name { get; set; }

        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Only Positive value")]

        [Required(ErrorMessage = "Category Expense limit is required")]
        [Display(Name = "Category Expense Limit")]
        public int C_Expense_Limit { get; set; }

        // relationsheep
        public List<Expense>? expenses { get; set; }
    }
}
