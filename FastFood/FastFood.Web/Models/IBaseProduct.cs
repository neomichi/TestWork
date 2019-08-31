using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public interface IBaseProduct
    {
        string Description { get; }
        int Price { get; }
        CategoryType ProductType { get; }

        int MaxCount { get; }


    }
}