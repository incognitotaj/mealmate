﻿using Mealmate.Application.Models.Base;

using System;

namespace Mealmate.Application.Models
{
    public class MenuItemModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset Created { get; set; }

        public int MenuId { get; set; }
        //public MenuModel Menu { get; set; }

        public MenuItemOptionModel MenuItemOption { get; set; }
    }
}