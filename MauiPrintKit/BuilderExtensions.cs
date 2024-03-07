using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiPrintKit
{
    public static class BuilderExtensions
    {
        public static MauiAppBuilder UseMauiPrintKit(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<IPrintingService, PrintingService>();
            return builder;
        }
    }
}
