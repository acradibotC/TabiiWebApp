using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabii.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Range(1, 10000000000, ErrorMessage ="Price should be beetween $1 and $10000000000")]
        public double Price { get; set; }
        [Display(Name = "Food Type")]
        public int FoodTypeId {  get; set; }
        [ForeignKey("FoodTypeId")]
        public FoodType FoodType { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set;}
        public Category Category { get; set; }

    }
}
