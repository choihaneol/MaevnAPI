

using API.Models;
using API.Models.Dto;
using API.Services;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;

namespace API.Services
{

    public class ProductService
    {

        public async Task<List<ProductCategoryModel>> getCategoryProduct(Models.B2bapiContext _db, APIResponse _response, List<ProductCategory> categoryProducts, int programId)
        {

            List<ProductCategoryModel> categoryObject = new List<ProductCategoryModel>();
            for (int i = 0; i < categoryProducts.Count(); i++)
            {

                if (categoryProducts[i].B2bActiveFlag == true)  // it need to be changed to true 
                {
                    var propertyValue = categoryProducts[i].StyleNumber;
                    var propertyValue2 = 10;

                    Console.WriteLine();

                    var url = _db.ProductImages
                        .FromSqlRaw($"Select * from ProductImage where StyleNumber = '{propertyValue}' AND TypeId = '{propertyValue2}'").ToList();

                    var colortmp = categoryProducts[i].Colors;
                    List<string> color = colortmp.Split(',').ToList();
                    var fittmp = categoryProducts[i].Fits;
                    List<string> fit = fittmp.Split(',').ToList();
                    var sizetmp = categoryProducts[i].Sizes;
                    List<string> size = sizetmp.Split(',').ToList();
                    var inseamtmp = categoryProducts[i].InseamLengths;
                    List<string> inseam = inseamtmp.Split(',').ToList();

                    categoryObject.Add(new ProductCategoryModel
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

                        ProductUrl = url[0].ProductUrl,
                        //ProductUrl = "https://maevn-images.s3.us-east-2.amazonaws.com/MaevnUniforms/products/" + categoryProducts[i].StyleNumber + "blk.jpg", // defulat image url column should be added to productCategory table 
                    });
                }
            }

            Task<List<ProductCategoryModel>> final = Task.FromResult(categoryObject);
            return await final;
        }



        public async Task<List<ProductCategory>> filter(Models.B2bapiContext _db, int programId, string? garmentType, string? color, string? fit, string? size, string? inseam, decimal? priceFrom, decimal? priceTo)
        {

            // query need to be modifired to below
            // where (erpProgramId =6 and [Sizes] like '%XXS%') or (erpProgramId =6 and [Sizes] like '%2XLP%')

            int check = 0;
            string query = "Select * from ProductCategory where ErpProgramId = " + programId;
            if (!string.IsNullOrEmpty(garmentType))
            {
                query += " and GarmentType ='" + garmentType + "'";
            }
          
            if (!string.IsNullOrEmpty(color))
            {
                List<string> colorlist = color.Split(',').ToList();
                query += " and Colors like '%";

                for (int i = 0; i < colorlist.Count; i++)
                {
                    query += colorlist[i] + "%";
                }
                query += "'";
            }

            if (!string.IsNullOrEmpty(fit))
            {
                List<string> fitlist = fit.Split(',').ToList();
                query += " and Fits like '%";

                for (int i = 0; i < fitlist.Count; i++)
                {
                    query += fitlist[i] + "%";
                }
                query += "'";
            }

            if (!string.IsNullOrEmpty(size))
            {
                List<string> sizelist = size.Split(',').ToList();
                query += " and Sizes like '%";

                for (int i = 0; i < sizelist.Count; i++)
                {
                    query += sizelist[i] + "%";
                }
                query += "'";
            }

            if (!string.IsNullOrEmpty(inseam))
            {
                List<string> inseamlist = inseam.Split(',').ToList();
                query += " and InseamLengths like '%";

                for (int i = 0; i < inseamlist.Count; i++)
                {
                    query += inseamlist[i] + "%";
                }
                query += "'";
            }

            if (!string.IsNullOrEmpty(priceFrom.ToString()) && !string.IsNullOrEmpty(priceTo.ToString()))
            {
                query += " and (PriceMin+PriceMax)/2 > " + priceFrom + " and (PriceMin+PriceMax)/2 < " + priceTo;
                check++;
            }

            var test = _db.ProductCategories.FromSqlRaw(query).ToList();


            Task<List<ProductCategory>> final = Task.FromResult(test);
            return await final;

        }
    }
}
