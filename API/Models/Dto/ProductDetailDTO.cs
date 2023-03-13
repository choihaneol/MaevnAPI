using Microsoft.Identity.Client;
using System.Diagnostics.Contracts;

namespace API.Models.Dto
{
    public class ProductDetailDTO
    {
        //productCategory table
        public string Pid { get; set; }
        public string StyleNumber { get; set; }
        public string ProductLine { get; set; }
        public string PriceMin { get; set; }
        public string PriceMax { get; set; }
        public int DiscountRate { get; set; }
        public List<string> ColorList { get; set; }
        public List<string> FitList { get; set; }
        public List<string> InseamList { get; set; }


        //product tableP
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ColorCode { get; set; }
        public List<SizeDTO> sizeList { get; set; }
 

        //image urls
    
       
    }
}
