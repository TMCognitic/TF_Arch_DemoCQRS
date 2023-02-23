using System.ComponentModel.DataAnnotations;

namespace TF_Arch_DemoCQRS.Models.Forms
{
#nullable disable
    public class CreateProductForm
    {
        [Required]
        [StringLength(128, MinimumLength=1)]
        public string Nom { get; set; }
        [Required]
        [StringLength(600, MinimumLength = 1)]
        public string Description { get; set; }
        [Required]        
        public double Prix { get; set; }
    }
}
