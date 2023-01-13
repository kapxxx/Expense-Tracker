using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
    {
        public class UpdateExpenceViewModel
        {
            [Key]
            public int E_Id { get; set; }

        [Required]
 
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Only alphabet allowed!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "name length should be beween 3 to 20")]
        [Display(Name = "Expense Title")]
        public string E_Title { get; set; }

        [Required]
        public string E_Description { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Only Positive value")]
        [Display(Name = "Expense Amount")]
        public int E_Amount { get; set; }

            public DateTime E_Date { get; set; } = DateTime.Now;

            [Display(Name = "Categorys")]
            public int C_id { get; set; }

            [ForeignKey("C_id")]
            public virtual Category? Category { get; set; }
        }
    }

