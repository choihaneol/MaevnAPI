namespace API.Models.Dto
{
    public class ProductCategoryModel
    {
        public int Id { get; set; }

        public int ErpProgramId { get; set; }

        public string? ProductLine { get; set; }

        public string? StyleNumber { get; set; }

        public string? Colors { get; set; }

        public string? ShortDescription { get; set; }

        public string ProductUrl { get; set; }

        public decimal? PriceMin { get; set; }

        public decimal? PriceMax { get; set; }

        public decimal? PriceMean { get; set; }

        public bool? B2bActiveFlag { get; set; }

        public string? GarmentType { get; set; }
        public string? Fits { get; set; }
        public string? Sizes { get; set; }



    }
}




