using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfConfigProvider
{
    public class EfConfigSource : IConfigurationSource
    {
        private readonly Action<DbContextOptionsBuilder> optionsAction;

        public EfConfigSource(Action<DbContextOptionsBuilder> optionsAction)
        {
            this.optionsAction = optionsAction;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EfConfigurationProvider(optionsAction);
        }
    }
}
