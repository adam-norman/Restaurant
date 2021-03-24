using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant.Models
{
   public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [Display(Name = "Order Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double OrderTotal { get; set; }
        [Required]
        [Display(Name = "Pick Up Time")]
        public DateTime PickUpTime { get; set; }
        [NotMapped]
        [Display(Name = "Pick Up Date")]
        public DateTime PickUpDate { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string Comments { get; set; }
        [Display(Name = "Pick Up Name")]
        public string PickUpName { get; set; }
        public string PhoneNumber { get; set; }
        public string TransactionId { get; set; }
    }
}
