using System;

namespace Parroter.Parrot.Exceptions
{
    internal class UnknownNotificationException : Exception
    {
        public UnknownNotificationException(string message) : base(message)
        {
        }
    }
}
