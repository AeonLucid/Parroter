using System;

namespace Parroter.Parrot.Resource
{
    internal class NotifyEventArgs : EventArgs
    {
        public NotifyEventArgs(ResourceType resource, string path)
        {
            Resource = resource;
            Path = path;
        }

        public ResourceType Resource { get; }

        public string Path { get; }
    }
}
