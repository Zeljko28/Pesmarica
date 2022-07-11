using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pesmarica.Database;

namespace Pesmarica
{
    class GLOBALS
    {
        public static string PATH = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().IndexOf("\\bin"));
        public static List<Song> ALL_SONGS = SQLiteDataAccess.LoadSongs();
        public static int SELECTED_SONG_ID = -1;
        public static Song SELECTED_SONG = null;

        public static int SELECTED_USER_ID = -1;

        public static string USERNAME { get; set; }
        public static string PASSWORD { get; set; }
        public static string TYPE { get; set; }

        public static string SEARCH = "";
    }
}
