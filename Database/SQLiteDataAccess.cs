using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Pesmarica;
using Dapper;
using System.Diagnostics;
using Pesmarica.Database;
using System.Windows;

namespace Pesmarica.Database
{
    public class SQLiteDataAccess
    {

        public static List<Song> LoadSongs(int id = -1, string search = "", string sort = "", int year = -1)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "SELECT * FROM Song";
                if (id != -1) { query = "SELECT * FROM Song WHERE song_id=" + id; }
                else if (!search.Trim().Equals(""))
                {
                    search = "%" + search + "%";
                    search = '"' + search + '"';
                    query = "SELECT * FROM Song WHERE title LIKE " + search + " OR genre LIKE " + search + "OR artist_id=(SELECT artist_id FROM Artist WHERE artist_name LIKE" + search + "OR year LIKE " + search + ")";
                }
                else if (!sort.Equals("")) 
                {
                    sort = '"' + sort + '"';
                    query = "SELECT * FROM Song ORDER BY " + sort + " ASC";
                }

                var output = cnn.Query<Song>(query, new DynamicParameters());
                cnn.Close();

                for(int i = 0; i < output.ToList().Count; i++) 
                {
                    output.ToList()[i].artwork = GLOBALS.PATH + "//Assets//" + output.ToList()[i].artwork;
                }

                return output.ToList();
            }
        }

        public static List<Artist> LoadArtists(int id = -1, string name = "")
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "SELECT * FROM Artist";
                if (id != -1)
                {
                    query = "SELECT * FROM Artist WHERE artist_id=" + id;
                }
                else if (name != "") 
                {
                    string nm = '"' + name + '"';
                    query = "SELECT * FROM Artist WHERE artist_name=" + nm;
                }
                var output = cnn.Query<Artist>(query, new DynamicParameters());
                cnn.Close();
                if (output.ToList().Count == 0) 
                {
                    return null;
                }
                
                return output.ToList();
            }
        }

        public static List<RegularUser> LoadUsers()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select * from Regular";
                var output = cnn.Query<RegularUser>(query, new DynamicParameters());

                cnn.Close();

                return output.ToList();
            }
        }

        public static bool checkIfExist(User user)
        {

            string userType = user.GetUserType();
            userType = userType.ToLower();
            userType = userType.Substring(0, 1).ToUpper() + userType.Substring(1, userType.Length - 1);

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                string query = "SELECT * FROM " + userType + " WHERE username = @username AND password = @password";
                SQLiteCommand cmd = new SQLiteCommand(query, cnn);
                cmd.Parameters.AddWithValue("@username", user.GetUsername());
                cmd.Parameters.AddWithValue("@password", user.GetPassword());
                SQLiteDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Close();
                    cnn.Close();
                    return true;
                }
                else
                {
                    rdr.Close();
                    cnn.Close();
                    return false;
                }
            }
        }

        public static void saveArtist(Artist artist)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute(" insert into Artist (artist_name)  values (@artist_name)", artist);
                cnn.Close();
            }
        }

        public static void saveSong(Song song)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute(" insert into Song (artist_id, title, year, lyrics, artwork, genre)  values (@artist_id, @title, @year, @lyrics, @artwork, @genre)", song);
                cnn.Close();
            }
        }

        public static void updateSong(Song song)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute(" update Song set title=@title, year=@year, lyrics=@lyrics, artwork=@artwork, genre=@genre where song_id=@song_id", song);
                cnn.Close();
            }
        }



        public static bool checkIfUsernameExists(User user)
        {
            string userType = user.GetUserType();
            userType = userType.ToLower();
            userType = userType.Substring(0, 1).ToUpper() + userType.Substring(1, userType.Length - 1);

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                string query = "SELECT * FROM " + userType + " WHERE username = @username";
                SQLiteCommand cmd = new SQLiteCommand(query, cnn);
                cmd.Parameters.AddWithValue("@username", user.GetUsername());
                SQLiteDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Close();
                    cnn.Close();
                    return true;
                }

                else
                {
                    rdr.Close();
                    cnn.Close();
                    return false;
                }
            }
        }

        public static void saveUser(User regular)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute(" insert into Regular (name, lastname, username, password, type)  values (@name ,@lastname, @username, @password, @userType)", regular);
                cnn.Close();
            }
        }

        public static void updateUsersPassword(User user)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                if (user.GetUserType().ToLower() == "regular")
                {
                    cnn.Execute("update Regular set password = @password  where username = @username ", user);
                }
                else
                {
                    cnn.Execute("update Administrator set password = @password  where username = @username ", user);
                }
                cnn.Close();
            }

        }

        public static void DeleteSong(Song song)
        {
            if (song != null) 
            {
                using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    
                    cnn.Open();
                    cnn.Execute(" DELETE FROM Song WHERE song_id=@song_id", song);
                    cnn.Close();
                    GLOBALS.SELECTED_SONG = null;
                    MessageBox.Show("Uspešno ste obrisali pesmu!");
                }
            }

            else
            {
                MessageBox.Show("Niste selektovali pesmu!");
            }
        }

        public static void DeleteUser(int id)
        {
            if (id != -1)
            {
                using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    RegularUser r = new RegularUser();
                    r.regularUserId = id;
                    cnn.Open();
                    cnn.Execute(" DELETE FROM Regular WHERE regularUserId=@regularUserId", r);
                    cnn.Close();
                    GLOBALS.SELECTED_USER_ID = -1;
                    MessageBox.Show("Uspešno ste obrisali korisnika!");
                }
            }
            else
            {
                MessageBox.Show("Niste selektovali korisnika!");
            }
        }


        public static string LoadConnectionString()
        {
            string connectionString = @"Data Source =" + GLOBALS.PATH + "\\Database\\Pesmarica.db; Version = 3";
            return connectionString;
        }


    }
}
