using Microsoft.AspNetCore.Mvc;
using ShopModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopDemo.Components
{
    public class ProductViewComponent : ViewComponent
    {
        public ProductViewComponent()
        {
        }

        //public IViewComponentResult Inovke()
        //{
        //    List<MenuViewModel> menus = new List<MenuViewModel>();
        //    for (int i = 1; i < 10; i++)
        //    {
        //        menus.Add(new MenuViewModel() { id = i, name = i.ToString(), url = "" });

        //    }
        //    return View(menus);
        //}

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

            var content = string.Empty;

            try
            {
                HttpClient _httpClient = new HttpClient();

                var response = await _httpClient.GetAsync("http://localhost:5002/api/product");

                if (!response.IsSuccessStatusCode)
                {
                    return View(products);
                }

                content = await response.Content.ReadAsStringAsync();
            }
            catch
            {

            }


            //if (content == "Product")
            {
                products =new List<ProductViewModel>
                {

                    new ProductViewModel { id=1, title = "Killing Is My Business... and Business Is Good!", url = "https://upload.wikimedia.org/wikipedia/en/thumb/5/54/Combat_KIMB.jpg/220px-Combat_KIMB.jpg", date = new DateTime(1985, 6, 12) },

                    new ProductViewModel { id=2, title = "Peace Sells... but Who's Buying?", url = "https://en.wikipedia.org/wiki/File:Megadeth_-_Peace_Sells..._But_Who%27s_Buying-.jpg", date = new DateTime(1989, 9, 19) },

                    new ProductViewModel { id=3, title = "Rust in Peace", url = "https://upload.wikimedia.org/wikipedia/en/thumb/d/dc/Megadeth-RustInPeace.jpg/220px-Megadeth-RustInPeace.jpg", date = new DateTime(1990, 9, 24) },

                    new ProductViewModel { id=4, title = "L'Enfant Sauvage", url = "https://upload.wikimedia.org/wikipedia/en/thumb/8/86/Gojira_-_L%27Enfant_Sauvage_cover.jpg/220px-Gojira_-_L%27Enfant_Sauvage_cover.jpg", date = new DateTime(2012, 6, 26) },

                    new ProductViewModel { id=5, title = "The Way of All Flesh", url = "https://upload.wikimedia.org/wikipedia/en/thumb/1/16/Gojira_-_The_Way_of_All_Flesh_-_2008.jpg/220px-Gojira_-_The_Way_of_All_Flesh_-_2008.jpg", date = new DateTime(2008, 10, 13) }

                };
            }

            return View(products);
        }
    }
}
