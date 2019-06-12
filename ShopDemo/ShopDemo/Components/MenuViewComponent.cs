using Microsoft.AspNetCore.Mvc;
using ShopModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopDemo.Components
{
    public class MenuViewComponent : ViewComponent
    {
        public MenuViewComponent()
        {
        }

        public IViewComponentResult Inovke()
        {
            List<MenuViewModel> menus = new List<MenuViewModel>();
            for(int i=1;i<10;i++)
            {
                menus.Add(new MenuViewModel() { id = i, name = i.ToString(), url = "" });

            }
            return View(menus);
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            List<MenuViewModel> menus = new List<MenuViewModel>();

            var content = string.Empty;

            try
            {
                HttpClient _httpClient = new HttpClient();

                var response = await _httpClient.GetAsync("http://localhost:5001/api/menu/getmenu");

                if (!response.IsSuccessStatusCode)
                {
                    return View(menus);
                }

                content = await response.Content.ReadAsStringAsync();
            }
            catch
            {

            }
           

            if (content == "Menu")
            {
                for (int i = 1; i < 10; i++)
                {
                    menus.Add(new MenuViewModel() { id = i, name = i.ToString(), url = "" });

                }
            }

            return View(menus);
        }
    }
}
