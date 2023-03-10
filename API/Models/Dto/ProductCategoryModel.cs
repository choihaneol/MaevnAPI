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
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public decimal? PriceMean { get; set; }
        public bool? B2bActiveFlag { get; set; }
        public string? GarmentType { get; set; }
        public string? Fits { get; set; }
        public string? Sizes { get; set; }
        public string? InseamLength { get; set; }

        public int IsPreorder { get; set; }
        public int IsNew { get; set; }
        public int DiscountRate { get; set; }

        public string? ImageLinks { get; set; }

        public List<string> ColorList { get; set; }
        public List<string> FitList { get; set; }
        public List<string> SizeList { get; set; }
        public List<string> InseamLengthList { get; set; }

        public string? ImageLinks { get; set; }

    }
}




