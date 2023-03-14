namespace API.Models.Dto
{
    public class ProductListDTO
    {
        public int Id { get; set; }

        public int ErpProgramId { get; set; }

        public string? ProductLine { get; set; }

        public string? StyleNumber { get; set; }
        public string? ShortDescription { get; set; }

        public string? GarmentType { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }

        public List<string> Colors { get; set; }

        public List<string> Sizes { get; set; }

        public List<string> Fits { get; set; }

        public List<string> InseamLengths { get; set; }

        public string? defaultColorCode { get; set; }

        public int IsPreorder { get; set; }
        public int IsNew { get; set; }

        public string ListImageUrl { get; set; }

    }
}




