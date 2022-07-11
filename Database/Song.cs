using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pesmarica;

namespace Pesmarica.Database
{
    public class Song
    {
        public int song_id { get; set; }
        public int artist_id { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public string artwork { get; set; }
        public string lyrics { get; set; }
        public string genre { get; set; }


    }
}
