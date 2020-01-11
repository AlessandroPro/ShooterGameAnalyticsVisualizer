using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEditor;

//Used for recording and updating game data in an SQLite db
public static class DatabaseManager
{
    private static string connectionString = "URI=file:" + 
                                             Application.dataPath + 
                                            "/Database/ShooterGameAnalytics.db"; // Path to the database

    // Adds a new game session id to the game_session table 
    public static int AddNewGameSession()
    {
        using (IDbConnection conn = new SqliteConnection(connectionString))
        {
            conn.Open();

            IDbCommand dbcmd = conn.CreateCommand();
            string command = "INSERT INTO game_session (id) VALUES (null)"; // autoincrements id
            dbcmd.CommandText = command;


            dbcmd.ExecuteNonQuery();

            dbcmd.Dispose();
            dbcmd = null;
            conn.Close();
        }

        return GetLatestEntryID("game_session");
    }

    // Adds a new game round id for this particlar game session in the game_round_id table
    public static int AddNewGameRound(int gameSessionId)
    {
        using (IDbConnection conn = new SqliteConnection(connectionString))
        {
            conn.Open();

            IDbCommand dbcmd = conn.CreateCommand();
            string command = "INSERT INTO game_round (id, game_session_id) VALUES (null, @game_session_id)"; 
            dbcmd.CommandText = command;

            IDbDataParameter gameSessionIdParameter = dbcmd.CreateParameter();
            dbcmd.Parameters.Add(gameSessionIdParameter);
            gameSessionIdParameter.ParameterName = "game_session_id";
            gameSessionIdParameter.DbType = DbType.Int32;
            gameSessionIdParameter.Value = gameSessionId;

            dbcmd.ExecuteNonQuery();

            dbcmd.Dispose();
            dbcmd = null;
            conn.Close();
        }

        return GetLatestEntryID("game_round");
    }

    // Adds a record to player_round_stats for the player stats for this game round
    public static void AddPlayerStatsRecord(int gameRoundId, Vector2 deathPos, float secondsSurvived, int shotsFired, int score)
    {
        using (IDbConnection conn = new SqliteConnection(connectionString))
        {
            conn.Open();

            IDbCommand dbcmd = conn.CreateCommand();
            string command = "INSERT INTO player_round_stats " +
                             "(game_round_id, death_x_pos, death_z_pos, seconds_survived, shots_fired, score) " +
                             "VALUES (@game_round_id, @death_x_pos, @death_z_pos, @seconds_survived, @shots_fired, @score)";
            dbcmd.CommandText = command;


            IDbDataParameter gameRoundIdParameter = dbcmd.CreateParameter();
            IDbDataParameter deathXPosParameter = dbcmd.CreateParameter();
            IDbDataParameter deathZPosParameter = dbcmd.CreateParameter();
            IDbDataParameter secondsSurvivedParameter = dbcmd.CreateParameter();
            IDbDataParameter shotsFiredParameter = dbcmd.CreateParameter();
            IDbDataParameter scoreParameter = dbcmd.CreateParameter();

            dbcmd.Parameters.Add(gameRoundIdParameter);
            dbcmd.Parameters.Add(deathXPosParameter);
            dbcmd.Parameters.Add(deathZPosParameter);
            dbcmd.Parameters.Add(secondsSurvivedParameter);
            dbcmd.Parameters.Add(shotsFiredParameter);
            dbcmd.Parameters.Add(scoreParameter);

            gameRoundIdParameter.ParameterName = "game_round_id";
            gameRoundIdParameter.DbType = DbType.Int32;
            gameRoundIdParameter.Value = gameRoundId;

            deathXPosParameter.ParameterName = "death_x_pos";
            deathXPosParameter.DbType = DbType.Single;
            deathXPosParameter.Value = deathPos.x;

            deathZPosParameter.ParameterName = "death_z_pos";
            deathZPosParameter.DbType = DbType.Single;
            deathZPosParameter.Value = deathPos.y;

            secondsSurvivedParameter.ParameterName = "seconds_survived";
            secondsSurvivedParameter.DbType = DbType.Single;
            secondsSurvivedParameter.Value = secondsSurvived;

            shotsFiredParameter.ParameterName = "shots_fired";
            shotsFiredParameter.DbType = DbType.Int32;
            shotsFiredParameter.Value = shotsFired;

            scoreParameter.ParameterName = "score";
            scoreParameter.DbType = DbType.Int32;
            scoreParameter.Value = score;

            dbcmd.ExecuteNonQuery();

            dbcmd.Dispose();
            dbcmd = null;
            conn.Close();
        }
    }

    // Adds a record to enemy_round_stats for the stats fpr all enemies for this game round
    public static void AddEnemyStatsRecord(int gameRoundId, float avgSecondsSurvived, float avgDistanceKilled, int totalKilled)
    {
        using (IDbConnection conn = new SqliteConnection(connectionString))
        {
            conn.Open();

            IDbCommand dbcmd = conn.CreateCommand();
            string command = "INSERT INTO enemy_round_stats " +
                             "(game_round_id, avg_seconds_survived, avg_distance_killed, total_killed) " +
                             "VALUES (@game_round_id, @avg_seconds_survived, @avg_distance_killed, @total_killed)";
            dbcmd.CommandText = command;


            IDbDataParameter gameRoundIdParameter = dbcmd.CreateParameter();
            IDbDataParameter avgSecondsSurvivedParameter = dbcmd.CreateParameter();
            IDbDataParameter avgDistanceKilledParameter = dbcmd.CreateParameter();
            IDbDataParameter totalKilledParameter = dbcmd.CreateParameter();


            dbcmd.Parameters.Add(gameRoundIdParameter);
            dbcmd.Parameters.Add(avgSecondsSurvivedParameter);
            dbcmd.Parameters.Add(avgDistanceKilledParameter);
            dbcmd.Parameters.Add(totalKilledParameter);


            gameRoundIdParameter.ParameterName = "game_round_id";
            gameRoundIdParameter.DbType = DbType.Int32;
            gameRoundIdParameter.Value = gameRoundId;

            avgSecondsSurvivedParameter.ParameterName = "avg_seconds_survived";
            avgSecondsSurvivedParameter.DbType = DbType.Single;
            avgSecondsSurvivedParameter.Value = avgSecondsSurvived;

            avgDistanceKilledParameter.ParameterName = "avg_distance_killed";
            avgDistanceKilledParameter.DbType = DbType.Single;
            avgDistanceKilledParameter.Value = avgDistanceKilled;

            totalKilledParameter.ParameterName = "total_killed";
            totalKilledParameter.DbType = DbType.Int32;
            totalKilledParameter.Value = totalKilled;

            dbcmd.ExecuteNonQuery();

            dbcmd.Dispose();
            dbcmd = null;
            conn.Close();
        }
    }

    // Returns the id of the latest entry from the specified table (assuming an auto-incremented table)
    public static int GetLatestEntryID(string tableName)
    {
        int id = 0;

        using (IDbConnection conn = new SqliteConnection(connectionString))
        {
            conn.Open();

            IDbCommand dbcmd = conn.CreateCommand();
            string command = "SELECT seq FROM sqlite_sequence WHERE name = \"" + tableName + "\"";
            dbcmd.CommandText = command;

            IDataReader reader = dbcmd.ExecuteReader();
            if(reader.Read())
            {
                id = reader.GetInt32(0);
            }

            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            conn.Close();
        }

        return id;
    }
}
