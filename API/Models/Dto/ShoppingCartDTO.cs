namespace API.Models.Dto
{
    public class ShoppingCartDTO
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        public int IsPreorder { get; set; }

        public string SubAccountId { get; set; }
    }
}
