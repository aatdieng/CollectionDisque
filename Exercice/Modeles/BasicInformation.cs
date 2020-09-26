using System;
using System.Collections.Generic;

namespace Exercice
{
    public class BasicInformation
    {
        public long id { get; set; }

        public long master_id { get; set; }

        public string master_url { get; set; }

        public string thumb { get; set; }

        public string cover_image { get; set; }

        public string title { get; set; }

        public int year { get; set; }

        public List<Format> formats { get; set; }

        public List<Label> labels { get; set; }

        public List<Artist> artists { get; set; }

        public List<string> genres { get; set; }

        public List<string> styles { get; set; }


    }
}
