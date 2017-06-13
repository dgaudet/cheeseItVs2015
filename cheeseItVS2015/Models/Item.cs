using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace cheeseItVS2015.Models
{
    [XmlRoot("Items")]
    public class ItemCollection
    {
        [XmlElement("Item")]
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public string BestBeforeDate { get; set; }
        public string DaysToSell { get; set; }
        public string Price { get; set; }
        public string Type { get; set; }
    }

    public class Container
    {
        public Item[] Items { get; set; }
    }
}