using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        [Key]
        public int E_Id { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Only alphabet allowed!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "name length should be beween 3 to 20")]
        [Display(Name = "Expense Title")]
        public string E_Title { get; set; }

        [Required]
        [Display(Name = "Expense Description")]
        public string E_Description { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Only Positive value")]
        [Display(Name = "Expense Amount")]
        public int E_Amount { get; set; }

        public DateTime E_Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please select atleast one category") ]
        [Display(Name = "category")]
        public int C_Id { get; set; }

        [ForeignKey("C_Id")]
        public virtual Category? Category { get; set; }  
    }
}
