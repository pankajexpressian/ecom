﻿namespace Catalog.API.Data
{
    public class MongodbConnectionSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

    }
}
