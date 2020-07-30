using System.Collections.Generic;
using Weighsoft.Models;

namespace Weighsoft.Services
{
    public interface IAPIService
    {
        List<ProductModel> GetProducts();

        List<WasteTypeModel> GetWasteTypes();

        double GetPrice();
    }
}
