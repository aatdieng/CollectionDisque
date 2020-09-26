using System;

namespace Exercice
{
    public class CollectionMusique
    {
        public long id { get; set; }

        public long instance_id { get; set; }

        public DateTime date_added { get; set; }

        public int rating { get; set; }

        public BasicInformation basic_information { get; set; }

        public int nombreTotalArticlesDanslaCollection { get; set; }
    }
}
