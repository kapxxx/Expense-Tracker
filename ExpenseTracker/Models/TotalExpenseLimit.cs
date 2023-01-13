using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class TotalExpenseLimit
    {
        [Key]
        public int Total_ExpenseLimit_Id { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Only Positive value")]
        public int Total_ExpenseLimit { get; set; }
    }
}
