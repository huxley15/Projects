using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject2
{

    public partial class FormGameNormal : Form
    {
        ScoreRepository scoreRepository;

        //goright and goleft used for player box movement
        bool goLeft;
        bool goRight;
        //bool isGameOver; //removed because button now used for restarting new game

        int score; //keeps track of score during the game

        //used for ball movement
        int ballX; 
        int ballY;

        //used to adjust the speed of player bar
        int playerSpeed;

        //using random to generate different colors for the boxes and to randomize the ball speed after every interaction
        Random rnd = new Random();

        //using an array to place the picture boxes for ease of setting up a new game
        PictureBox[] blockArray;
        public FormGameNormal()
        {
            InitializeComponent();

            //PlaceBlocks(); commented out due to creation of menu and buttons to initialize game
        }
        private void setupGame()
        {
            //hide unnecessary controls for playing of game
            //controls need to be disabled to allow key presses to only focus on player block
            //groupBoxButtons.Visible = false; switched to a panel to remove border, aesthetic purpose only
            //groupBoxButtons.Enabled = false;
            panelButtons.Visible = false;
            panelButtons.Enabled = false;
            groupPlayerInfo.Visible = false;
            groupPlayerInfo.Enabled = false;
            dataGridScores.Visible = false;
            dataGridScores.Enabled = false;

            //isGameOver = false; removed because new game function changed to button click
            score = 0;
            ballX = 5;
            ballY = 5;
            playerSpeed = 12;
            txtScore.Text = "Score: " + score;

            //string highName = scoreRepository.HighName().ToString();
            //string highScore = scoreRepository.HighScore().ToString();
            //string highName = scoreRepository.HighPlayer().Name;
            //string highScore = scoreRepository.HighPlayer().Score.ToString();
            //lblHighScore.Text = "High Score: " + highName + " - " + highScore;
            
            //intialize player and ball in neutral positions
            ball.Left = 350;
            ball.Top = 467;

            player.Left = 315;
            player.Top = 509;

            //begin timer to get controls moving
            gameTimer.Start();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    //assign random colors to block backgrounds for a more fun aesthetic
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }
        }

        private void gameOver(string message)
        {
            //isGameOver = true;
            gameTimer.Stop();
            //change label text to show real time score value
            txtScore.Text = "Score: " + score + " " + message;

            //show the buttons so the user can choose to leave, save score, or play again
            //groupBoxButtons.Visible = true; switched to panel
            //groupBoxButtons.Enabled = true;
            panelButtons.Visible = true;
            panelButtons.Enabled = true;
            btnNew.Visible = true;
            btnSave.Visible = true;
            btnStart.Visible = false;
            dataGridScores.Visible = true;
            dataGridScores.Enabled = true;
        }

        private void PlaceBlocks()
        {
            //create new array of 20 pictureboxes
            blockArray = new PictureBox[20];

            //variable to account for number of picture boxes per row
            int a = 0;

            //variables determining placement of boxes, can be tweaked to shift depending on form size
            int top = 50;
            int left = 60;

            for (int i = 0; i < blockArray.Length; i++)
            {
                //creating new instance of picturebox
                blockArray[i] = new PictureBox();
                blockArray[i].Height = 32;
                blockArray[i].Width = 100;
                blockArray[i].Tag = "blocks";
                blockArray[i].BackColor = Color.White; //will be assigned random color in setup game

                //if row has reached 5 blocks which is the max I want per row
                if (a == 5)
                {
                    //shift the y axis placement down by 50
                    top = top + 50;
                    left = 60; //reset x axis position to line up with first block 
                    a = 0; //reset block number to zero to start placing 5 blocks again
                }
                if (a < 5)
                {
                    a++; //increment a to keep placing blocks in the row
                    blockArray[i].Left = left;
                    blockArray[i].Top = top;
                    this.Controls.Add(blockArray[i]); //place a picturebox
                    left = left + 130; //shift over to the right for the next box placement
                }
            }
            //immediately move to start the game after blocks are placed
            setupGame();
        }

        private void removeBlocks()
        {
            //in the event that the game ends and blocks remain, remove the excess blocks
            foreach (PictureBox x in blockArray)
            {
                this.Controls.Remove(x);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //pull data from DB into datagrid
            scoreRepository = new ScoreRepository();
            dataGridScores.DataSource=scoreRepository.ReadScores();

            //scoreRepository.HighPlayer();
            lblHighScore.Text = "High Score: " + scoreRepository.HighPlayer().Name + " - " + scoreRepository.HighPlayer().Score.ToString();

            //when the player first loads the game I don't want these to be visible
            groupPlayerInfo.Visible = false;
            btnSave.Visible = false;
            btnNew.Visible = false;
            dataGridScores.Visible = false;

        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            //allows player to move left until reaching left bound
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            //allows player to move right until reaching right edge
            if (goRight == true && player.Left<638)
            {
                player.Left += playerSpeed;
            }

            //moves ball 
            ball.Left += ballX;
            ball.Top += ballY;

            //if ball reaches left or right edges, reverse the direction
            if (ball.Left < 0 || ball.Left > 709)
            {
                ballX = -ballX;
            }
            //if ball reaches the top of the window, reverse direction
            if (ball.Top < 0)
            {
                ballY = -ballY;
            }
            //if ball comes in contact with plater block, reverse direction
            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                //warp ball to above player block if interaction hits at weird angle to avoid glitch
                ball.Top = 463;
                //generate a new random ball speed after interaction
                ballY = rnd.Next(5, 12) * -1;
                
                if (ballX < 0)
                {
                    ballX = rnd.Next(5, 12) * -1;
                }
                else
                {
                    ballX = rnd.Next(5, 12);
                }
            }

            foreach (Control x in this.Controls) 
            {
                if (x is PictureBox && (string)x.Tag == "blocks") //focuses only on controls that are blocks
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds)) //when ball comes in contact with block, remove block and increment score
                    {
                        score += 1;

                        ballY = -ballY;

                        this.Controls.Remove(x);
                    }
                }
            }

            if (score == 20)
            {
                //end game message
                gameOver("Win! Success! Glory!");
                
            }
            if (ball.Top > 587)
            {
                //lose message
                gameOver("Loss! Failure! Shame!");
                
            }
        }

        private void keyisDown(object sender, KeyEventArgs e)
        {
            //change left/right variables to determine player block movement
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyisUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            //if (e.KeyCode == Keys.Enter && isGameOver == true)
            //{
            //    removeBlocks();
            //    PlaceBlocks();
            //}
        }

        

        private void btnNew_Click(object sender, EventArgs e)
        {
            //start new game
            removeBlocks();
            PlaceBlocks();
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            //return to main menu form
            MenuForm f2 = new MenuForm();
            this.Hide();
            f2.Show();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            PlaceBlocks();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataGridScores.Visible = true;
            groupPlayerInfo.Visible = true;
            groupPlayerInfo.Enabled = true;

            //autofill the ID with the next available number, disallow user editing of ID
            txtID.Text=(scoreRepository.GetMaxID() + 1).ToString();
            txtID.ReadOnly = true;

            //autofill score block with score earned and disallow editing
            txtPlayerScore.Text = Convert.ToString(score);
            txtPlayerScore.ReadOnly = true;
        }
        private void RefreshData()
        {
            txtPlayerScore.Clear();
            txtName.Clear();
            txtID.Clear();

            dataGridScores.DataSource = null;
            dataGridScores.DataSource=scoreRepository.ReadScores();
            //dataGridScores.Sort(dataGridScores.Columns[2], ListSortDirection.Descending);
            //dataGridScores.Columns[1].Name = "Rank";

            groupPlayerInfo.Visible=false;
            //btnSave.Visible=false;

            SetGridHeightWidth(dataGridScores, 350, 350);
        }
        public void SetGridHeightWidth(DataGridView grd, int maxWidth, int maxHeight)
        {
            //allows datagrid to only be as large as needed to encapsulate data
            var height = 26;
            foreach (DataGridViewRow row in grd.Rows)
            {
                if (row.Visible)
                {
                    height += row.Height;
                }
            }
            if (height > maxHeight)
            {
                height = maxHeight;
            }
            grd.Height = height;

            var width = 60;
            foreach (DataGridViewColumn col in grd.Columns)
            {
                if (col.Visible)
                    width += col.Width;
            }

            if (width > maxWidth)
                width = maxWidth;

            grd.Width = width;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //verify all fields contain data before trying to submit to database
            if (!string.IsNullOrWhiteSpace(txtID.Text) && !string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtPlayerScore.Text))
            {
                if (scoreRepository.FindScore(txtName.Text) != null) //check if player has submitted a score previously
                {
                    //locate record for player
                    var name = txtName.Text;
                    var scoretoupdate = scoreRepository.FindScore(name);
                    //only score is being updated
                    scoretoupdate.Score = Convert.ToInt32(txtPlayerScore.Text);
                    //save updated score to DB
                    scoreRepository.UpdateScore(name, scoretoupdate);
                    int rank = scoreRepository.PlayerRanking(name);
                    MessageBox.Show($"{name}'s record has been updated with a new ranking of {rank}!");
                }

                else
                {
                    //assign values to table data members
                    var newscore = new PlayerScore();
                    newscore.Name = txtName.Text;
                    newscore.Score = Convert.ToInt32(txtPlayerScore.Text);

                    //save new score to DB
                    scoreRepository.AddScore(newscore);
                    int rank = scoreRepository.PlayerRanking(newscore.Name);
                    MessageBox.Show($"{newscore.Name}'s score has been saved! {newscore.Name} has achieved a rank of {rank}");
                }
                
            }
            else
            {
                MessageBox.Show("Please enter a name before submitting your score! :)");
                txtName.Focus(); //focusing on the Name box because the other 
            }
            //clear the forms
            RefreshData();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                //ensure that player is filling in a name when trying to submit a score
                txtName.BackColor = Color.Fuchsia;
                MessageBox.Show("Please enter a name before submitting your score! :)");
                txtName.Focus();
            }
            else
            {
                txtName.BackColor= Color.White;
            }
        }
    }
}
