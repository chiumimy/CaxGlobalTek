using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldChampion
{
    public class CommonClass
    {

    }

    public class NameValuePair
    {
        public NameValuePair(string name, string value)
        {
            NAME = name;
            VALUE = value;
        }
        public string NAME { get; set; }
        public string VALUE { get; set; }
    }
}