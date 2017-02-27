using System;
using System.Collections.Generic;
using System.Text;

namespace Parroter.Parrot
{
    public class ParrotMessage
    {
        public ParrotMessage(string resource, string arg = null)
        {
            Resource = resource;
            Arg = arg;
            Request = Resource.EndsWith("set", StringComparison.CurrentCultureIgnoreCase)
                ? $@"SET {Resource}?arg={Arg?.ToLower()}"
                : $@"GET {Resource}";
        }

        public string Resource { get; }

        public string Arg { get; }

        public string Request { get; }

        public byte[] GetRequest()
        {
            var length = Request.Length + 3;
            var message = new List<byte>(length);

            // Header
            message.Add((byte)(length >> 8));
            message.Add((byte)(length >> 0));
            message.Add(0x80);

            // Body
            message.AddRange(Encoding.ASCII.GetBytes(Request));

            return message.ToArray();
        }
    }
}
