using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant.Models
{
   public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250 ,ErrorMessage ="you shouldn't exceed 250 characters")]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required,Range(1,int.MaxValue,ErrorMessage ="Price should be greater than 1$")]
        public double Price { get; set; }
        [Display(Name ="Category Type")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category  Category { get; set; }


        [Display(Name = "Food Type")]
        public int FoodTypeId { get; set; }
        [ForeignKey("FoodTypeId")]
        public FoodType FoodType { get; set; }
    }
}
