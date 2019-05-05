using System;
using System.Collections.Generic;
using System.Text;
using TestJob.Interfaces;
using TestJob.Model;

namespace TestJob.Helpers
{
    /// <summary>
    /// Ответ на запрос
    /// </summary>
    public class Response : IResponse
    {
        public Profile Bag { get  ; private set; }
        public ResponseType ResponseType { get; private set; }

        public Response(ResponseType responseType) {
            this.ResponseType = responseType;
        }

        public Response(ResponseType responseType, Profile bag)
        {
            this.ResponseType = responseType;
            Bag = bag;
        }
    }
}
