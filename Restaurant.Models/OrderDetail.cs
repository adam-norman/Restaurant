using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
   public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId")]
        public MenuItem  MenuItem  { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
