using SmartMarketDeskop.Integrated.Interfaces.Categories;
using SmartMarketDeskop.Integrated.Interfaces.Products.Product;
using SmartMarketDeskop.Integrated.Interfaces.Workers.Position;
using SmartMarketDeskop.Integrated.Interfaces.Workers.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Interfaces
{
    public interface IUnitOfWork
    {
        ICategory category { get; set; }
        IWorkerService worker { get; set; }
        IProductService product { get; set; }
        IPositionService position { get; set; }
    }
}
