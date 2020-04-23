using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfConfigProvider
{
    public static class EfConfigExtensions
    {
        public static IConfigurationBuilder AddEfConfig(this IConfigurationBuilder builder, Action<DbContextOptionsBuilder> option)
        {
            return builder.Add(new EfConfigSource(option));
        }
    }
}
