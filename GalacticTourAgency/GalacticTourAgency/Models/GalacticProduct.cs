using GalacticTourAgency.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GalacticTourAgency.Models
{
    public class GalacticProduct
    {
        public int Id { get; set; }
        [Required (ErrorMessage="Ürün adı gereklidir")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Ürün adı 3-100 karakter arasında olmalıdır")]
        public string Name { get; set; }

        [Range(0.01,10000, ErrorMessage = "Fiyat 0.01-10000 arasında olmalıdır")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Gezegen adı gereklidir")]
        [RegularExpression("^(Merkür|Venüs|Mars)$", ErrorMessage = "Geçerli bir gezegene ait ürün değil")]
        public string Planet { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Üretim Tarihi")]
        public DateTime ManifacturingDate { get; set; }

        [Range(1,10)]
        [Display(Name = "Galaktik Puan")]
        public int GalacticRating { get; set; }

        [GalacticElementComposition(minElements:2, maxElements :5)]
        public string Composition { get; set; }
        public GalacticCordinate Cordinate { get; set; }
    }
}
