namespace API.Models.Dto
{
    public class SizeDTO
    {
        public int Pid { get; set; }
        public string ItemSize { get; set; }
        public string UnitPrice { get; set; }
        public int discountRate { get; set; }
        public string qtyAvailable { get; set; }

        public string nextAvailDate { get; set; } //for pre-order, it is earliest arrival time
        public int IsPreorder { get; set; }
        public int IsNew { get; set; }

        public int erpProductId { get; set; }
        public int erpProgramId { get; set; }
    }
}
