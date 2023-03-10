using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using API.Models;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class OrderService
    {


        public async Task<ActionResult<ShoppingCartDTO>> getShippingCart(B2bapiContext _db,int userId)
        {
            //search UsesrId using loginId
            List<ShoppingCartDTO> carts = new List<ShoppingCartDTO>();

            // string query = "SELECT a.userId,a.productId,a.qty,a.IsPreorder,ISNULL((select LoginId from [B2BAPI].[dbo].[Users] where UserId=a.[subAccount]),'') as subaccountId FROM [B2BAPI].[dbo].[Basket] as a where a.userId=" + userId+ " OR a.subAccount" + userId;
            var test = from b in _db.Baskets
                       let subAccount = (from u in _db.Users
                                         where b.UserId == u.UserId
                                         select u.LoginId).FirstOrDefault()
                       where (b.UserId == userId) || (b.SubAccount == userId)
                       select new ShoppingCartDTO
                       {
                           UserId  = b.UserId,
                           ProductId = b.ProductId,
                           Qty = b.Qty,
                           IsPreorder = b.IsPreorder,
                           SubAccountId = subAccount,
                       };

            Console.WriteLine(test);

           // var test = _db.Baskets.FromSqlRaw(query).ToList();
             Task<ShoppingCartDTO> final = null; 
            return await final;
        }


        public async Task<Boolean> postShippingCart(B2bapiContext _db, List<ShoppingCartDTO> model, string loginId)
        {


            //find userId 
            User user = _db.Users.FirstOrDefault(u => u.LoginId == loginId);
            int userId = user.UserId;
            int subaccount = user.SubAccount;


            var cart = new List<Basket>();
            foreach (var item in model)
            {
                // Check if the item already exists in the cart
                var existingItem = _db.Baskets.FirstOrDefault(i => i.ProductId == item.ProductId && i.UserId == userId);

                if (existingItem != null)
                {
                    // Update the quantity of the existing item in the cart
                    if (item.Qty != 0)
                    {
                        existingItem.Qty = item.Qty;
                        _db.SaveChanges();
                    }
                    else
                    {
                        _db.Baskets.Remove(existingItem);
                        _db.SaveChanges();
                    }
                }
                else
                {
                    if (item.Qty !=0)
                    {
                        // Add the new items to the cart
                        cart.Add(new Basket
                        {
                            UserId = userId,
                            Qty = item.Qty,
                            ProductId = item.ProductId,
                            IsPreorder = item.IsPreorder,
                            SubAccount = subaccount,
                        });
                    }
                }
            }

            // Save the updated cart back to the database 
            _db.Baskets.AddRange(cart);
            await _db.SaveChangesAsync();

            return true; 

        }

    }
}
