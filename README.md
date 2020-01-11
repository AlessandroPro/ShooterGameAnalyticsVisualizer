Alessandro Profenna
Oct. 6 2019


Some notes:

DATABASE:

The database is called ShooterGameAnalytics.db and is located in
the zip file folder in SurvivalShooterTutorial\Assets\Database.

I created 4 tables:

game_session: records an id for each new "game session" (that is, each time the game application is run) <br>
game round: records an id for each new "game round" in a particular game session (when the player dies, a new round starts) <br>
player_round_stats: At the end of each game round, the statistics of the player are recorded here <br>
enemy_round_stats: At the end of each game round, the statistics for all enemies as a collective are recorded here <br>

3 of the stats for enemies that are recorded per round: average distance to player when killed, average survival time, number killed.

UNITY PROJECT:

I made two new scripts in SurvivalShooterTutorial\Assets\Database:

1) DatabaseManager.cs: Static class with all of the calls to the database for recording data
2) GameRoundStats.cs: Attached to an empty game object called "GameRoundStats" that is referenced in the game's scripts to keep track of
game statistics (updated when the player dies, an enemy dies, etc.)

Also, some small changes were added to existing script, just small bits where GameRoundStats needed to be updated. All extra code in these
scripts is appended with a "//@@" comment for easy searchng. Scripts touched are EnemyHealth.cs, PlayerHealth.cs, and PlayerShooting.cs

WINFORM

For the visualizer, you must open the database (there is only one db file in SurvivalShooterTutorial\Assets\Database).
Once it is loaded, you can choose to view statistics from all game sessions or choose a specific one.
As discussed with you, a DataGridView was used instead of a PropertyGrid.
The UI logic is all in ShooterGameAnalysisForm.cs and the database querying is in DatabaseReader.cs.
There is also a game icon.

Thanks!

![GameMapView_TopDown](https://user-images.githubusercontent.com/15040875/72210401-c398f780-3488-11ea-9605-1c4f7d691109.png)

![GameView](https://user-images.githubusercontent.com/15040875/72210402-c4318e00-3488-11ea-9179-f4392ede7fd3.png)

![VisualizerTool](https://user-images.githubusercontent.com/15040875/72210403-c4318e00-3488-11ea-959d-7074718ce5fc.PNG)

