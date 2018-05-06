using Ninject.Modules;
using StoreData;
using StoreData.Infrastructure;
using StoreData.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBL
{
    public class DependencyBL: NinjectModule
    {
        private StoreContext context;

        public DependencyBL(StoreContext context)
        {
            this.context = context;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(context);
        }
    }
}
