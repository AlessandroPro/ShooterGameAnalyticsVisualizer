using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collects statistics for the current game round
public class GameRoundStats : MonoBehaviour
{
    static int gameSessionId = 0;
    private int gameRoundId = 0;

    //Enemies
    private float enemySurvivalTimeSum = 0;
    private float enemyKillDistanceSum = 0;
    private int totalEnemiesKilled = 0;

    //Player
    private float playerSurvivalTime = 0;
    private int shotsFired = 0;
    private int totalScore = 0;
    private Vector2 playerDeathPos;

    private void Awake()
    {
        // ensures the following code executes only once per application run
        if(gameSessionId == 0)
        {
            //Record new game session
            gameSessionId = DatabaseManager.AddNewGameSession();
        }

        //Record new game round
        gameRoundId = DatabaseManager.AddNewGameRound(gameSessionId);

        playerDeathPos = new Vector2(0, 0);
    }

    // Updates the player statistics for the round, called when the player dies
    public void UpdatePlayerStatsOnDeath(float spawnTime, Vector3 deathPos, int numShots, int score)
    {
        playerSurvivalTime = Time.time - spawnTime;
        playerDeathPos.Set(deathPos.x, deathPos.z);
        shotsFired = numShots;
        totalScore = score;
    }

    // Updates the statistics for all enemies in this round, called every time an enemy dies
    public void UpdateEnemyStatsOnDeath(Transform player, Transform enemy, float spawnTime)
    {
        Vector2 playerPosXZ = new Vector2(player.position.x, player.position.z);
        Vector2 enemyPosXZ = new Vector2(enemy.position.x, enemy.position.z);

        enemySurvivalTimeSum += Time.time - spawnTime;
        enemyKillDistanceSum += (enemyPosXZ - playerPosXZ).magnitude;
        totalEnemiesKilled++;
    }

    // Called when the player dies (game round ends), and adds a record to the player and enemy stats tables
    public void RecordStatsInDatabase()
    {
        DatabaseManager.AddPlayerStatsRecord(gameRoundId, playerDeathPos, playerSurvivalTime, shotsFired, totalScore);

        // If no enemies died in this game round, set these stats to -1
        float avgEnemySecondsSurvived = -1f;
        float avgEnemyKillDistance = -1f;
        if (totalEnemiesKilled > 0)
        {
            avgEnemySecondsSurvived = enemySurvivalTimeSum / totalEnemiesKilled;
            avgEnemyKillDistance = enemyKillDistanceSum / totalEnemiesKilled;
        }

        DatabaseManager.AddEnemyStatsRecord(gameRoundId, avgEnemySecondsSurvived, avgEnemyKillDistance, totalEnemiesKilled);
    }
}
