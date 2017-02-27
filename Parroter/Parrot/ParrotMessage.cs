using System;
using System.Collections.Generic;
using System.Text;
using Parroter.Parrot.Resource;

namespace Parroter.Parrot
{
    internal class ParrotMessage
    {
        public ParrotMessage(ResourceType resource, string arg = null)
        {
            Resource = ResourceManager.Resources[resource];
            Arg = arg;
            Request = Resource.EndsWith("set", StringComparison.CurrentCultureIgnoreCase)
                ? $@"SET {Resource}?arg={Arg?.ToLower()}"
                : $@"GET {Resource}";
        }

        private string Resource { get; }

        private string Arg { get; }

        public string Request { get; }

        public byte[] GetRequest()
        {
            var length = Request.Length + 3;
            var message = new List<byte>(length)
            {
                // Header
                (byte) (length >> 8),
                (byte) (length >> 0), 0x80
            };
            
            // Body
            message.AddRange(Encoding.ASCII.GetBytes(Request));

            return message.ToArray();
        }
    }
}
