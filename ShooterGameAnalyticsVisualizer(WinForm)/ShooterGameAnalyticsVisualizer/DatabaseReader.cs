using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Numerics;


namespace ShooterGameAnalyticsVisualizer
{
    // A class with functions for querying the ShooterGameAnalytics database tables
    class DatabaseReader
    {
        // Default location
        public static string connectionString = "Data Source=.\\ShooterGameAnalytics.db";

        // Returns all data from the specified table
        public static DataTable GetAllTableData(string tableName)
        {
            DataTable table = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string command = "SELECT * from " + tableName;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command, connection);
                adapter.Fill(table);
                connection.Close();
            }

            return table;
        }

        // Returns a list of all the game session ids, stored as ComboBoxItems
        // Note that the first item is "All" to rperesent all ids
        public static List<ComboBoxItem> RetriveGameSessionIDs()
        {
            DataTable table = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string command = "SELECT id FROM game_session";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command, connection);
                adapter.Fill(table);
                connection.Close();
            }

            List<ComboBoxItem> ids = new List<ComboBoxItem>(table.Rows.Count + 1);
            ids.Add( new ComboBoxItem("All", 0)); //first in list is option for All ids

            // Fills the rest of the list with the Game Session Ids from the table
            foreach (DataRow row in table.Rows)
            {
                int gameSessionId = Convert.ToInt32(row["id"]);
                ComboBoxItem gameSessionIdItem = new ComboBoxItem("Session #" + gameSessionId.ToString(), gameSessionId);
                ids.Add(gameSessionIdItem);
            }


            return ids;
        }

        // Returns a list of XZ positions of where the player died each round in the specified game session
        public static List<Vector2> GetPlayerDeathPositions(int gameSessionId)
        {
            DataTable table = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string gameSessionIdFilter = GetGameSessionFilter(gameSessionId);
                string command = "SELECT death_x_pos, death_z_pos FROM player_round_stats " + gameSessionIdFilter;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command, connection);
                adapter.Fill(table);
                connection.Close();
            }

            List<Vector2> deathPositions = new List<Vector2>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                if(!Convert.IsDBNull(row["death_x_pos"]))
                {
                    float xPos = Convert.ToSingle(row["death_x_pos"]);
                    float zPos = Convert.ToSingle(row["death_z_pos"]);
                    deathPositions.Add(new Vector2(xPos, zPos));
                }
            }

            return deathPositions;
        }

        // Returns the data from the specifed table for the specified game session,
        // If called with gameSessionId = 0, data for all game sessions is returned
        public static DataTable GetDataInSession(string tableName, int gameSessionId)
        {
            DataTable table = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string gameSessionIdFilter = GetGameSessionFilter(gameSessionId);
                string command = "SELECT * FROM " + tableName + " " + gameSessionIdFilter;

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command, connection);
                adapter.Fill(table);
                connection.Close();
            }

            return table;
        }

        // Generates the following SQL condition to append to a table query:
        // If a record's game_round_id is not associated with the specified gameSessionId, it is filtered out
        // If the game session id is equal to 0, then no ids are filtered
        private static string GetGameSessionFilter(int gameSessionId)
        {
            string gameSessionIdFilter = "";
            if (gameSessionId > 0)
            {
                gameSessionIdFilter =  "WHERE game_round_id IN (SELECT id FROM game_round WHERE game_session_id = " +
                                      gameSessionId.ToString() + " )";
            }

            return gameSessionIdFilter;
        }
    }
}
