namespace API.Models.Dto
{
    public class SizeDTO
    {
        public int Pid { get; set; }
        public string Size { get; set; }
        public string Price { get; set; }
        public string AvailableQty { get; set; }
        public string ETA { get; set; } //for pre-order, it is earliest arrival time
        public int IsPreorder { get; set; }
        public int IsNew { get; set; }
        public int DiscountRate { get; set; }
    }
}
