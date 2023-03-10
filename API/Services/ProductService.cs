using API.Models;
using API.Models.Dto;
using API.Services;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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


        public async Task<ProductCategory> getProductDetail(Models.B2bapiContext _db,string? styleNumber)
        {

            string query = "Select * from ProductCategory where StyleNumber="+ styleNumber;
            var test = _db.ProductCategories.FromSqlRaw(query).First();

            Console.WriteLine(test);

            Task<ProductCategory> final = Task.FromResult(test);
            return await final;

        }

        public async Task<List<ProductCategoryDTO>> getCategoryProduct(Models.B2bapiContext _db, APIResponseDTO _response, List<ProductCategory> categoryProducts, int programId)
        {

            List<ProductCategoryDTO> categoryObject = new List<ProductCategoryDTO>();
            for (int i = 0; i < categoryProducts.Count(); i++)
            {

                if (categoryProducts[i].B2bActiveFlag == true)  // it need to be changed to true 
                {
                    var propertyValue = categoryProducts[i].StyleNumber;
                    var propertyValue2 = 10;

                    Console.WriteLine();


                    var colortmp = categoryProducts[i].Colors;
                    List<string> color = colortmp.Split(',').ToList();
                    var fittmp = categoryProducts[i].Fits;
                    List<string> fit = fittmp.Split(',').ToList();
                    var sizetmp = categoryProducts[i].Sizes;
                    List<string> size = sizetmp.Split(',').ToList();
                    var inseamtmp = categoryProducts[i].InseamLengths;
                    List<string> inseam = inseamtmp.Split(',').ToList();

                    categoryObject.Add(new ProductCategoryDTO
                    {
                        Id = categoryProducts[i].Id,
                        ErpProgramId = categoryProducts[i].ErpProgramId,
                        StyleNumber = categoryProducts[i].StyleNumber,
                        ShortDescription = categoryProducts[i].ShortDescription,
                        ProductLine = categoryProducts[i].ProductLine,
                        Colors = categoryProducts[i].Colors,
                        Fits = categoryProducts[i].Fits,
                        Sizes = categoryProducts[i].Sizes,
                        InseamLength = categoryProducts[i].InseamLengths,


                        ColorList = color,
                        FitList = fit,
                        SizeList = size,
                        InseamLengthList = inseam,

                        GarmentType = categoryProducts[i].GarmentType,
                        PriceMin = categoryProducts[i].PriceMin,
                        PriceMax = categoryProducts[i].PriceMax,
                        PriceMean = (categoryProducts[i].PriceMin + categoryProducts[i].PriceMax / 2),
                        B2bActiveFlag = categoryProducts[i].B2bActiveFlag,

                        IsPreorder = categoryProducts[i].IsPreorder,
                        IsNew = categoryProducts[i].IsNew,
                        DiscountRate = categoryProducts[i].DiscountRate,
                        ImageLinks = JsonConvert.DeserializeObject<ImageLinkDTO>( categoryProducts[i].ImageLinks )


                        // ProductUrl = url[0].ProductUrl,
                        //ProductUrl = "https://maevn-images.s3.us-east-2.amazonaws.com/MaevnUniforms/products/" + categoryProducts[i].StyleNumber + "blk.jpg", // defulat image url column should be added to productCategory table 
                    }); ; ; ;
                }
            }

            Task<List<ProductCategoryDTO>> final = Task.FromResult(categoryObject);
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