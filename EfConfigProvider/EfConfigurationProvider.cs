using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfConfigProvider
{
    public class EfConfigurationProvider : ConfigurationProvider
    {
        Action<DbContextOptionsBuilder> OptionsAction { get; }

        public EfConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ConfigurationContext>();
            OptionsAction(builder);

            using (var dbContext = new ConfigurationContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();

                if (dbContext.Values.Any())
                {
                    Data = dbContext.Values.ToDictionary(c => c.ID, c => c.Value);
                }
                else
                {
                    var configValues = new Dictionary<string, string> {
                        { "key1","value1"},
                        { "key2","value2"}
                    };

                    dbContext.Values.AddRange(configValues.Select(kv => new ConfigurationValue { ID = kv.Key, Value = kv.Value }));

                    dbContext.SaveChanges();

                    Data = configValues;
                }
            }
        }
    }
}
