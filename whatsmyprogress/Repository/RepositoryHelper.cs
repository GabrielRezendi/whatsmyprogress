using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace whatsmyprogress.Repository
{
    public class RepositoryHelper
    {
        public static ByteArrayContent GetByteArrayContent(object entity)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entity));
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }
    }
}
