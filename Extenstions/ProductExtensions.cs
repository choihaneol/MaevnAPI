using API.Models.Dto;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.CompilerServices;
 
namespace API.Extenstions
{
    public static class ProductExtensions
    {
        public static IQueryable<ProductCategory> Filter(this IQueryable<ProductCategory> query,
            string programId, string garmentType, string color, string fit, string size, decimal? priceFrom, decimal? priceTo)
        {


            //parameters
            var productIdList = new List<String>();
            var garmentTypeList = new List<string>();
            var colorList = new List<string>();
            var fitList = new List<string>();
            var sizeList = new List<string>();
            var priceList = new List<string>();


            //list type parameters
            if (!string.IsNullOrEmpty(programId))
            {
                productIdList.AddRange(programId.Split(",").ToList());
            }
            if (!string.IsNullOrEmpty(garmentType))
            {
                garmentTypeList.AddRange(garmentType.Split(",").ToList());
            }
            //if (!string.IsNullOrEmpty(color))
            //{
            //    colorList.AddRange(color.Split(",").ToList());
            //}


            // if (!string.IsNullOrEmpty(color))
            //      query = query.Where(p => productIdList.Count == 0 || productIdList.Contains(p.ErpProgramId.ToString()));


            query = query.Where(p => productIdList.Count == 0 || productIdList.Contains(p.ErpProgramId.ToString()));


            query = query.Where(p => garmentTypeList.Count == 0 || garmentTypeList.Contains(p.GarmentType));





            return query;
        }
    }
}
