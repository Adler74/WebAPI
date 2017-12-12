using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class URL
    {
        public string url { get; set; }

        public URL(string text)
        {
            this.url = text;
        }
    }
}