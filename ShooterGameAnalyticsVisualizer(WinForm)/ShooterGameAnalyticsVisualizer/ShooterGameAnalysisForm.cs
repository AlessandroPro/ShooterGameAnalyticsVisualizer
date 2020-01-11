using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace ShooterGameAnalyticsVisualizer
{
    // A WinForm that shows analytical data based on play sessions of Shooter Game Tutorial
    public partial class ShooterGameAnalysisForm : Form
    {
        private Bitmap circleGradient;  // circle gradiant grapic used for plotting deaths on heat map
        private Bitmap heatMap;         // the heat map of player death positions

        public ShooterGameAnalysisForm()
        {
            InitializeComponent();
            circleGradient = new Bitmap(100, 100);
            heatMap = new Bitmap(500, 500);
            CreateCircle();
            createHeatmap(0, false);
        }

        // Fills a bitmap with a gradient circle, used as a plot point in the heat map
        private void CreateCircle()
        {
            //Initializes the circle bitmap with a black background
            for (int i = 0; i < circleGradient.Width; i++)
            {
                for (int j = 0; j < circleGradient.Height; j++)
                {
                    circleGradient.SetPixel(i, j, Color.Black);
                }
            }

            int radius = circleGradient.Width / 2; 

            // Set each pixel within the circle
            for (int x = 0; x < radius; x++)
            {
                int yAtRadius = (int)Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(x, 2));

                for (int y = -yAtRadius; y < yAtRadius; y++)
                {
                    // The greater the distance from the center, the 
                    // greater dimmer the colour is, based on an exponential function
                    float distance = (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    float exponent = 14 * (1 - distance / radius);
                    int red = Math.Min(255, (int)Math.Pow(exponent, 2));
                    Color colorG = Color.FromArgb(red, 0, 0);
                    circleGradient.SetPixel(x + radius, y + radius, colorG);
                    circleGradient.SetPixel(-x + radius, y + radius, colorG);
                }
            }
        }

        // Fills a bitmap with the heatmap, including the backgroung and plot points,
        // for all of the player death XZ positions in the specified game session
        private void createHeatmap(int gameSessionId, bool plotDeaths)
        {
            //Initializes the heatMap with a full background color
            for (int i = 0; i < heatMap.Width; i++)
            {
                for (int j = 0; j < heatMap.Height; j++)
                {
                    heatMap.SetPixel(i, j, Color.Black);
                }
            }

            // Plot the circle gradients for each death
            if(plotDeaths)
            {
                PlotDeathsOnHeatmap(gameSessionId);
            }

            //Draws a diamond to represent the game map shape
            for (int i = 0; i < heatMap.Height; i++)
            {
                int middle = heatMap.Width / 2;
                int numWhite = i + 1;
                if (numWhite > middle)
                {
                    numWhite = heatMap.Width - numWhite;
                }

                for (int j = 0; j < heatMap.Width; j++)
                {
                    int left = middle - numWhite;
                    int right = middle + numWhite;

                    if ((j + 1) < left || (j + 1) > right)
                    {
                        heatMap.SetPixel(j, i, Color.Gray);
                    }
                }
            }

            // set heatmap PictureBox to this heatmap bitmap
            this.playerDeathHeatmap.Image = heatMap;
        }

        // Plots each of the player death positions on the heatmap, for the given game session
        private void PlotDeathsOnHeatmap(int gameSessionId)
        {
            List<Vector2> deathPositions = DatabaseReader.GetPlayerDeathPositions(gameSessionId);
            // the game "map" is a diamond within a square
            int mapStartPosX = -36; // observed from the Unity scene
            int mapStartPosZ = -36; // observed from the Unity scene
            int mapSize = 72; //side dimesion of the map (its square width in the Unity scene)

            foreach (Vector2 pos in deathPositions)
            {
                int xHeatMapPos = (int)((pos.X - mapStartPosX) / mapSize * heatMap.Width);
                int yHeatMapPos = heatMap.Height - (int)((pos.Y - mapStartPosZ) / mapSize * heatMap.Height);

                int xStart = xHeatMapPos - circleGradient.Width / 2;
                int yStart = yHeatMapPos - circleGradient.Height / 2;

                // Stamps a gradient circle at the current death position
                // by copying the colour of each pixel from the circleGradient bitmap
                for (int i = 0; i < circleGradient.Width; i++)
                {
                    for (int j = 0; j < circleGradient.Height; j++)
                    {
                        int x = xStart + i;
                        int y = yStart + j;

                        // Ensures the pixel that is being coloured is within the bounds of the heat map
                        if (x >= 0 && x < heatMap.Width && y >= 0 && y < heatMap.Height)
                        {
                            Color circleColor = circleGradient.GetPixel(i, j);
                            // The gradient circle is made of varying shades of red, with rest of the bitmap being
                            // black, so to prevent copying the corners of the square image, this check is made
                            if (circleColor.R != 0)
                            {
                                Color currentColor = heatMap.GetPixel(x, y);
                                // additive blending of pixel colours if multiple gradient circles are overlapping
                                int red = Math.Min(255, currentColor.R + circleColor.R);

                                Color newColor = Color.FromArgb(red, circleColor.G, circleColor.B);
                                heatMap.SetPixel(x, y, newColor);
                            }
                        }
                    }
                }
            }
        }

        // Open a dialogue file menu for the user to choose a database. Once selected, it is loaded,
        // and the data shown is defaulted to "All" game sessions
        private void OpenDatabase_Click(object sender, EventArgs e)
        {
            using (var selectFileDialog = openFileDialog)
            {
                if (selectFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = selectFileDialog.FileName;
                    System.IO.FileInfo fInfo = new System.IO.FileInfo(selectFileDialog.FileName);

                    string path = fInfo.FullName;
                    databaseLabel.Text = "Database: " + fInfo.Name;
                    DatabaseReader.connectionString = "Data Source =" + path;

                    // Get the game session id list, with the first item in the list being "All" ids
                    List<ComboBoxItem> gameInstanceData = DatabaseReader.RetriveGameSessionIDs();

                    // Set the source of the combo box to the list of game session ids, for the user to choose from
                    GameSessionList.DisplayMember = "Text";
                    GameSessionList.ValueMember = "Value";
                    GameSessionList.DataSource = gameInstanceData;

                    SetDataViewTable();

                    // Plots the heat map
                    createHeatmap((int)GameSessionList.SelectedValue, true); 
                }
            }
        }

        // Runs when the user changes the selected game session id from the combo box drop down list
        private void GameSessionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDataViewTable();
            createHeatmap((int)GameSessionList.SelectedValue, true);
        }

        // Sets the database table shown in the DataGridView, based on which radio button is currently selected (Player or Enemy)
        private void SetDataViewTable()
        {
            int selectedGameSession = (int)GameSessionList.SelectedValue;

            string tableName = "player_round_stats";
            string tableDesc = "Player stats in each round";

            if (enemiesRadioButton.Checked)
            {
                tableName = "enemy_round_stats";
                tableDesc = "Stats for all enemies in each round";
            }

            if (selectedGameSession > 0)
            {
                tableNameLabel.Text = tableDesc + " from game session " + selectedGameSession.ToString() + ":";
            }
            else
            {
                tableNameLabel.Text = tableDesc + " from all game sessions:";
            }

            DataTable dataTable = DatabaseReader.GetDataInSession(tableName, selectedGameSession);
            databaseTableView.DataSource = dataTable;
            playerRoundsLabel.Text = "Number of rounds(player deaths): " + dataTable.Rows.Count.ToString();
        }

        // If the Player radio button is checked or unchecked, the gridBoxView (the data table shown)
        // is changed to reflect the selection (Player or Enemy)
        private void PlayerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Ensures there is a selected game session
            if(GameSessionList.SelectedItem != null)
            {
                SetDataViewTable();
            }
        }
    }
}

