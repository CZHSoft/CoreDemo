using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopModel
{
    public class MenuViewModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string url { get; set; }

        public MenuViewModel father { get; set; }

        public List<MenuViewModel> childs { get; set; }
    }
}
