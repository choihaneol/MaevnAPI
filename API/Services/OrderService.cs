﻿using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using API.Models;
using Microsoft.IdentityModel.Abstractions;

namespace API.Services
{
    public class OrderService
    {

<<<<<<< Updated upstream

        public async Task<ActionResult<List<ShoppingCartDTO>>> getCart(B2bapiContext _db, string loginId)
        {
=======
        public async Task<ActionResult<List<ShoppingCartDTO>>> getShippingCart(B2bapiContext _db, int? userId)
        {
            //search UsesrId using loginId
            List<ShoppingCartDTO> carts = (from b in _db.Baskets
                                           let subAccount = (from u in _db.Users
                                                             where b.UserId == u.UserId
                                                             select u.LoginId).FirstOrDefault()
                                           where (b.UserId == userId) || (b.SubAccount == userId)
                                           select new ShoppingCartDTO
                                           {
                                               UserId = b.UserId,
                                               ProductId = b.ProductId,
                                               Qty = b.Qty,
                                               IsPreorder = b.IsPreorder,
                                               SubAccountId = subAccount,
                                           }).ToList();
>>>>>>> Stashed changes

            //search UsesrId using loginId
            List<ShoppingCartDTO> cart = new List<ShoppingCartDTO>();


            if ( loginId == null)
            {
                
            }

            User user = _db.Users.FirstOrDefault(u => u.LoginId == loginId);
            string userId = user.UserId.ToString();
            //search parent account using Subaccount(rno)


            if (user.SubAccount == 0) //if admin
            {

            }else //if subaccount
            {

            }



            //call all items in store's basket

            //return item list


            string query;
           // var test = _db.Baskets.FromSqlRaw(query).ToList();
             Task<List<ShoppingCartDTO>> final = Task.FromResult(cart);
            return await final;
        }


        public async Task<Boolean> postCart(B2bapiContext _db, List<ShoppingCartDTO> model, string loginId)
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
                    if (item.Qty != 0)
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
