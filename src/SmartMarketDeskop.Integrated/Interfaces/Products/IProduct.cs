﻿using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Interfaces.Products
{
    public interface IProduct : IRepository<ProductView>
    {
        public Task<List<ProductView>> GetProductsFullInformationAsync();
    }
}