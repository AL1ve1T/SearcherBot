using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherBotAPI
{
    public class YouTubeSearchResult
    {
        // Title of video/channel/playlist
        public string Title { get; set; }

        // ID that YouTube uses to identify the reffered resource
        public string Link { get; set; }
    }
}
