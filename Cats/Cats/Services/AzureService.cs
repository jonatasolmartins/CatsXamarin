﻿using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Services
{
    public class AzureService<T>
    {
        IMobileServiceClient Client;        IMobileServiceTable<T> Table;        public AzureService()
        {
            string MyAppServiceURL = "http://xamarincatsrcs.azurewebsites.net";
            Client = new MobileServiceClient(MyAppServiceURL);
            Table = Client.GetTable<T>();        }
        public Task<IEnumerable<T>> GetTable()
        {
            return Table.ToEnumerableAsync();
        }
    }
}
