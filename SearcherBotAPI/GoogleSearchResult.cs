﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherBotAPI
{
    public class GoogleSearchResult
    {
        // Title of the found link
        public string Title { get; set; }

        // Description of link
        public string Description { get; set; }

        // URL
        public string Link { get; set; }
    }
}
