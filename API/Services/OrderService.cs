using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class OrderService
    {

        public async Task<Boolean> postCart(List<ShoppingCartDTO> model,B2bapiContext _db)
        {

            var listInsertproductvalue = new List<Basket>();

            foreach(var item in model)
            {
                listInsertproductvalue.Add(new Basket
                {
                    UserId = item.UserId,
                    Qty = item.Qty,
                    ProductId = item.ProductId,
                    IsPreorder = item.IsPreorder,
                });
            }

            _db.Baskets.AddRange(listInsertproductvalue);
            await _db.SaveChangesAsync();

            return true; 

        }

    }
}
