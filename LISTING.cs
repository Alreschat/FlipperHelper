using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flipper_Extended
{
    public class Property
    {
        public string name { get; set; }
        public List<List<object>> values { get; set; }
        public int displayMode { get; set; }
    }

    public class Category
    {
        public List<object> currency { get; set; }
    }

    public class Item
    {
        public bool verified { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public int ilvl { get; set; }
        public string icon { get; set; }
        public string league { get; set; }
        public string name { get; set; }
        public string typeLine { get; set; }
        public bool identified { get; set; }
        public string note { get; set; }
        public List<Property> properties { get; set; }
        public List<string> explicitMods { get; set; }
        public string descrText { get; set; }
        public int frameType { get; set; }
        public int stackSize { get; set; }
        public int maxStackSize { get; set; }
        public Category category { get; set; }
    }

    public class Stash
    {
        public string name { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Online
    {
        public string league { get; set; }
        public string status { get; set; }
    }

    public class Account
    {
        public string name { get; set; }
        public string lastCharacterName { get; set; }
        public Online online { get; set; }
        public string language { get; set; }
    }

    public class ExchangeItem
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class Item2
    {
        public string currency { get; set; }
        public double amount { get; set; }
        public int stock { get; set; }
        public string id { get; set; }
    }

    public class Price
    {
        public ExchangeItem exchange { get; set; }
        public Item2 item { get; set; }
    }

    public class Listing
    {
        public string method { get; set; }
        public DateTime indexed { get; set; }
        public Stash stash { get; set; }
        public string whisper { get; set; }
        public Account account { get; set; }
        public Price price { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public Item item { get; set; }
        public Listing listing { get; set; }
    }

    public class RootObject
    {
        public List<Result> result { get; set; }
    }
}
