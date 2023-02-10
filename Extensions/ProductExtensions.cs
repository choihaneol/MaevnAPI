using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Models.Dto;

/*
namespace API.Extensions
{
    public class ProductExtensions
    {

        public static IQueryable<ProductCategory> Filter(this IQueryable<ProductCategoryModel> query, string prices, string garmentTypes, string colors, string fits, string sizes)
        {
            var priceList = new List<string>();
            var garmentTypeList = new List<string>();
            //var colorList = new List<string>();
            //var fitList = new List<string>();
            //var sizeList = new List<string>();

            if (!string.IsNullOrEmpty(garmentTypes))
                garmentTypeList.AddRange(garmentTypes.ToLower().Split(",").ToList());


            query = query.Where(p => priceList.Count == 0 || priceList.Contains(p.PriceMean.ToString()));
            query = query.Where(p => garmentTypeList.Count == 0 || garmentTypeList.Contains(p.GarmentType.ToLower()));
           


            return query;
        }


    }
}
*/