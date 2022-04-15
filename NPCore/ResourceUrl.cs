using System;
using System.Collections.Generic;
using System.Text;

namespace NPCore
{
    public enum ResourceType
    {
        Relative,
        Absolute,
        Both
    }

    public class ResourceUrl
    {
        public List<(string, string, ResourceType)> Items = new List<(string, string, ResourceType)>();

        public ResourceUrl(string Url, ResourceType Type = ResourceType.Relative)
        {
            Items.Add(("<ALL>", Url, Type));
        }
        
        public ResourceUrl(string Theme, string Url, ResourceType Type = ResourceType.Relative)
        {
            Items.Add((Theme, Url, Type));
        }

        public ResourceUrl(List<(string, string, ResourceType)> Items)
        {
            this.Items = Items;
        }
    }
}
