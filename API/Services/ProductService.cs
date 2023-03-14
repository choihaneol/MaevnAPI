using API.Models;
using API.Models.Dto;
using API.Services;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;

namespace API.Services
{

    public class ProductService
    {


        public async Task<ProductDetailDTO> getProductDetail(Models.B2bapiContext _db, string? styleNumber)
        {

            string query = "Select * from ProductCategory where StyleNumber=" + styleNumber;
            var test = _db.ProductCategories.FromSqlRaw(query).First();

            ProductDetailDTO res = new ProductDetailDTO()
            {
                ProductLine = test.ProductLine,
                StyleNumber = test.StyleNumber,
                ShortDescription = test.ShortDescription,
                LongDescription= test.LongDescription,
                PriceMax = test.PriceMax,
                PriceMin = test.PriceMin,
                FitList = test.Fits.Split(',').ToList(),
                InseamList = test.InseamLengths.Split(',').ToList(),
                colorSizes = JsonConvert.DeserializeObject<ColorSizesDTO>(test.ColorSizes),
                ImageLinks = JsonConvert.DeserializeObject<ImageLinkDTO>(test.ImageLinks)
            };

            Console.WriteLine(test);

            Task<ProductDetailDTO> final = Task.FromResult(res);
            return await final;

        }

        public async Task<List<ProductListDTO>> getProductList(Models.B2bapiContext _db, APIResponseDTO _response, int programId)
        {

            List<ProductListDTO> productListObject = new List<ProductListDTO>();

            //search UsesrId using loginId
            var categoryProducts = (from b in _db.ProductCategories
                                           where (b.ErpProgramId == programId) 
                                           select b).ToList();


            foreach(var item in categoryProducts)
            {

                if (item.B2bActiveFlag == true)  // it need to be changed to true 
                {

                    var colorsize = JsonConvert.DeserializeObject<dynamic>("["+item.ColorSizes+"]");

                    List<string> colors = new List<string>();
                    List<string> sizes = new List<string>();
                    
                    foreach (var newdata in colorsize)
                    {
                        foreach (JProperty jobj in newdata)
                        {
                            if(jobj.Name == "colorCode")
                            {
                                colors.Add(jobj.Value.ToString());
                            }
                            if (jobj.Name == "sizes")
                            {
                                foreach (var newsize in jobj.Value)
                                {
                                    foreach (JProperty sobj in newsize)
                                    {

                                        if(sobj.Name == "ItemSize")
                                        {
                                            sizes.Add(sobj.Value.ToString());
                                        }

                                    }
                                     
                                }
                            }
                        }

                         
                    }
                    var fittmp = item.Fits;
                    List<string> fit = fittmp.Split(',').ToList();
                    var inseamtmp = item.InseamLengths;
                    List<string> inseam = inseamtmp.Split(',').ToList();

                    productListObject.Add(new ProductListDTO
                    {
                        Id = item.Id,
                        ErpProgramId = item.ErpProgramId,
                        ProductLine = item.ProductLine,
                        StyleNumber = item.StyleNumber,
                        ShortDescription = item.ShortDescription,
                        GarmentType = item.GarmentType,
                        PriceMin = item.PriceMin,
                        PriceMax = item.PriceMax,
                        Colors = colors.Distinct().ToList(),
                        Sizes = sizes.Distinct().ToList(),
                        Fits = fit,
                        InseamLengths = inseam,
                        defaultColorCode = item.DefaultColorCode,
                        IsPreorder = item.IsPreorder,
                        IsNew = item.IsNew,
                        ListImageUrl = item.ListImageUrl,
                    });
                }

            }

            Task<List<ProductListDTO>> final = Task.FromResult(productListObject);
            return await final;
        }




        public async Task<List<ProductCategory>> filter(Models.B2bapiContext _db, int programId, string? garmentType, string? color, string? fit, string? size, string? inseam, decimal? priceFrom, decimal? priceTo)
        {


            int check = 0;
            string query = "Select * from ProductCategory where ";

            if (string.IsNullOrEmpty(garmentType) && string.IsNullOrEmpty(color)
                && string.IsNullOrEmpty(fit) && string.IsNullOrEmpty(size)
                && string.IsNullOrEmpty(inseam) && string.IsNullOrEmpty(priceFrom.ToString())
                && string.IsNullOrEmpty(priceTo.ToString()))
            {
                query += "erpProgramId = " + programId;
            }

            Console.WriteLine(query);


            if (!string.IsNullOrEmpty(garmentType))
            {
                List<string> garmentTypelist = garmentType.Split(',').ToList();

                if (garmentTypelist.Count > 0) { query += "("; }
                for (int i = 0; i < garmentTypelist.Count; i++)
                {
                    query += "erpProgramId = " + programId + " and GarmentType like '%" + garmentTypelist[i] + "%'";

                    if (i == garmentTypelist.Count - 1)
                    {

                    }
                    else
                    {
                        query += "or ";
                    }
                }
                if (garmentTypelist.Count > 0) { query += ")"; }

                if (!string.IsNullOrEmpty(color) || !string.IsNullOrEmpty(fit) || !string.IsNullOrEmpty(size) || !string.IsNullOrEmpty(inseam) || !string.IsNullOrEmpty(priceFrom.ToString()))
                {
                    query += "and ";
                }
            }

            Console.WriteLine(query);

            if (!string.IsNullOrEmpty(color))
            {
                List<string> colorlist = color.Split(',').ToList();

                if (colorlist.Count > 0) { query += "("; }
                for (int i = 0; i < colorlist.Count; i++)
                {

                    query += "id = " + programId + " and Colors like '%" + colorlist[i] + "%'";
                    if (i == colorlist.Count - 1)
                    {

                    }
                    else
                    {
                        query += "or ";
                    }
                }
                if (colorlist.Count > 0) { query += ")"; }

                if (!string.IsNullOrEmpty(fit) || !string.IsNullOrEmpty(size) || !string.IsNullOrEmpty(inseam) || !string.IsNullOrEmpty(priceFrom.ToString()))
                {
                    query += "and ";
                }
            }
            Console.WriteLine(query);


            if (!string.IsNullOrEmpty(fit))
            {
                List<string> fitlist = fit.Split(',').ToList();

                if (fitlist.Count > 0) { query += "("; }
                for (int i = 0; i < fitlist.Count; i++)
                {
                    query += "erpProgramId = " + programId + " and Fits like '%" + fitlist[i] + "%'";
                    if (i == fitlist.Count - 1)
                    {

                    }
                    else
                    {
                        query += "or ";
                    }
                }
                if (fitlist.Count > 0) { query += ")"; }

                if (!string.IsNullOrEmpty(size) || !string.IsNullOrEmpty(inseam) || !string.IsNullOrEmpty(priceFrom.ToString()))
                {
                    query += "and ";
                }
            }
            Console.WriteLine(query);

            if (!string.IsNullOrEmpty(size))
            {
                List<string> sizelist = size.Split(',').ToList();
                if (sizelist.Count > 0) { query += "("; }
                for (int i = 0; i < sizelist.Count; i++)
                {
                    query += "erpProgramId = " + programId + " and Sizes like '%" + sizelist[i] + "%'";
                    if (i == sizelist.Count - 1)
                    {

                    }
                    else
                    {
                        query += "or ";
                    }
                }
                if (sizelist.Count > 0) { query += ")"; }

                if (!string.IsNullOrEmpty(inseam) || !string.IsNullOrEmpty(priceFrom.ToString()))
                {
                    query += "and ";
                }
            }
            Console.WriteLine(query);

            if (!string.IsNullOrEmpty(inseam))
            {
                List<string> inseamlist = inseam.Split(',').ToList();
                if (inseamlist.Count > 0) { query += "("; }
                for (int i = 0; i < inseamlist.Count; i++)
                {
                    query += "erpProgramId = " + programId + " and InseamLengths like '%" + inseamlist[i] + "%'";
                    if (i == inseamlist.Count - 1)
                    {

                    }
                    else
                    {
                        query += "or ";
                    }
                }
                if (inseamlist.Count > 0) { query += ")"; }
                if (!string.IsNullOrEmpty(priceFrom.ToString()))
                {
                    query += "and ";
                }
            }
            Console.WriteLine(query);
            if (!string.IsNullOrEmpty(priceFrom.ToString()) && !string.IsNullOrEmpty(priceTo.ToString()))
            {
                query += "( erpProgramId = " + programId + " and ( PriceMin + PriceMax )/2 >= " + priceFrom + " and (PriceMin+PriceMax)/2 <= " + priceTo + ")";
            }




            var test = _db.ProductCategories.FromSqlRaw(query).ToList();
            Task<List<ProductCategory>> final = Task.FromResult(test);
            return await final;

        }
    }
}