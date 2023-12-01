using FindBook.Core.Interfaces;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindBook.Services.Implementations
{
    public class BookService 
    {
        private readonly IGraphClient _client;

        public BookService(IGraphClient client)
        {
            this._client = client;
        }

    }
}
