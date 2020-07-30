using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Weighsoft.Models;

namespace Weighsoft.Services
{
    public class APIService : IAPIService
    {
        public List<ProductModel> GetProducts()
        {
            string url = "http://testapi.weighsoft.co.uk/data/Products";

            List<ProductModel> products = new List<ProductModel>();                    

            try
            {
                JArray array = GetJArray(url);

                foreach (JObject item in array)
                {
                    int id = Convert.ToInt32(item.GetValue("Id"));
                    string description = item.GetValue("Description").ToString();

                    products.Add(new ProductModel { Id = id, Description = description });
                }

                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WasteTypeModel> GetWasteTypes()
        {
            string url = "http://testapi.weighsoft.co.uk/data/WasteTypes";

            List<WasteTypeModel> wasteTypes = new List<WasteTypeModel>();

            try
            {
                JArray array = GetJArray(url);

                foreach (JObject item in array)
                {
                    int id = Convert.ToInt32(item.GetValue("Id"));
                    string description = item.GetValue("Description").ToString();

                    wasteTypes.Add(new WasteTypeModel { Id = id, Description = description });
                }

                return wasteTypes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double GetPrice() 
        {
            double price = 0;

            //the api only has one price, for any value. so i'm always using that one
            string url = "http://testapi.weighsoft.co.uk/data/Price/0";          

            try
            {
                string theResult = string.Empty;
                var client = new HttpClient();
                var getProductTask = client.GetAsync(url)

                .ContinueWith(response =>
                {
                    var result = response.Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readResult = result.Content.ReadAsStringAsync();
                        readResult.Wait();
                        theResult = readResult.Result;
                    }
                });

                getProductTask.Wait();
                theResult = theResult.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
                dynamic val = JObject.Parse(theResult);
                price = val.Value;              

                return price;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private JArray GetJArray(string url)
        {
            string theResult = string.Empty;
            var client = new HttpClient();
            var getProductTask = client.GetAsync(url)

            .ContinueWith(response =>
            {
                var result = response.Result;
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var readResult = result.Content.ReadAsStringAsync();
                    readResult.Wait();
                    theResult = readResult.Result;
                }
            });

            getProductTask.Wait();

            return JArray.Parse(theResult);
        }
    }
}