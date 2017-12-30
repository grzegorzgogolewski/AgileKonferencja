using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AgileKonferencja
{
    // struktura do przechowywania informacji o prelegencie
    struct Prelegent
    {
        public string NazwaPrelegenta;
        public string OpisPrelegenta;
        public string ZdjeciePrelegenta;
    };

    /// <summary>
    /// klasa zawierająca metody do połączenia z bazą danych mySQL i pobierania z niej danych
    /// </summary>
    class MySqlTools
    {
        private readonly string _connectionString; // parametry połączenia

        /// <summary>
        /// konstruktor klasy, który przekazuje parametry połączenia
        /// </summary>
        /// <param name="connectionString">parametry połączenia</param>
        public MySqlTools(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// metoda zwracająca liczbę prelegentów w bazie
        /// </summary>
        /// <returns></returns>
        public int GetSpeakerCount()
        {
            int result;

            MySqlConnection sqlConn = new MySqlConnection(_connectionString);
            string queryString = "SELECT COUNT(*) FROM `wp_posts` WHERE id in (select object_id from wp_term_relationships where term_taxonomy_id = 6)";
            try
            {
                sqlConn.Open();
                MySqlCommand sqlcmd = new MySqlCommand(queryString, sqlConn);
                result = Convert.ToInt32( sqlcmd.ExecuteScalar());
                sqlConn.Close();
            }
            catch (Exception)
            {
                return 0;
            }

            return result;

        }

        /// <summary>
        /// metoda zwracjąca listę prelegentów wraz z ich atrybutami
        /// </summary>
        /// <returns></returns>
        public List<Prelegent> GetSpeakerInfo ()
        {

            List<Prelegent> prelegenci = new List<Prelegent>();

            MySqlConnection sqlConn = new MySqlConnection(_connectionString);
            string queryString = "SELECT id, post_title, post_content, (select guid from wp_posts where id = META.meta_value) FROM `wp_posts` POST, wp_postmeta META WHERE POST.id = META.post_id AND META.meta_key = '_thumbnail_id' AND id in (select object_id from wp_term_relationships where term_taxonomy_id = 6)";

            try
            {
                sqlConn.Open();
                MySqlCommand sqlcmd = new MySqlCommand(queryString, sqlConn);

                MySqlDataReader rdr = sqlcmd.ExecuteReader();
                while (rdr.Read())
                {
                    Prelegent prelegent;

                    prelegent.NazwaPrelegenta = rdr[1].ToString();
                    prelegent.OpisPrelegenta = rdr[2].ToString();
                    prelegent.ZdjeciePrelegenta = rdr[3].ToString();

                    prelegenci.Add(prelegent);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            sqlConn.Close();

            return prelegenci;
        }


    }
}