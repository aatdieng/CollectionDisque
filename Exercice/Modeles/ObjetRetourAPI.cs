using System;
using System.Collections.Generic;

namespace Exercice
{
    public class ObjetRetourAPi
    {
        public Pagination pagination { get; set; }

        public List<CollectionMusique> releases { get; set; }

        public int NombreTotalArticles { get; set; }

    }
}
