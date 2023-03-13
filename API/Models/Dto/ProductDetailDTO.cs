using Microsoft.Identity.Client;
using System.Diagnostics.Contracts;

namespace API.Models.Dto
{
    public class ProductDetailDTO
    {
        //productCategory table
        public string ProductLine { get; set; }
        public string StyleNumber { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public List<string> FitList { get; set; }
        public List<string> InseamList { get; set; }

        public ColorSizesDTO? colorSizes { get; set; }
        public ImageLinkDTO? ImageLinks { get; set; }

    }
}
