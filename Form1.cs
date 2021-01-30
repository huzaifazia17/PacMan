using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace SimplePacman
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Sets the text of the window to "Pacman"
            this.Text = "Pacman";
            //Sets the height of the window to sHeight
            this.Height = sHeight;
            //Sets the height of the window to sWidth
            this.Width = sWidth;
            //events for drawing to screen and keydown events
            this.DoubleBuffered = true;
            //changes background to black
            this.BackColor = Color.Black;
        }

        //movement timer
        Timer timer;
        //player rectangle
        Rectangle player;
        //wall rectangle list
        List<Rectangle> wall = new List<Rectangle>();
        //gold rectangle list
        List<Rectangle> gold = new List<Rectangle>();
        //enemy1 rectangle
        Rectangle enemy1;
        //enemy2 rectangle
        Rectangle enemy2;
        //enemy3 rectangle
        Rectangle enemy3;
        //player last spot rectangle
        Rectangle lastSpot;
        //music sound
        SoundPlayer music = new SoundPlayer();
        //height of screen
        const int sHeight = 800;
        //width of screen
        const int sWidth = 1000;
        //enemy1 speed
        int enemy1dx = 35;
        //enemys speed
        int enemy2dx = -35;
        //enemy3 speed
        int enemy3dx = 35;
        //player score
        int score = 0;
        //player dx speed
        int dx = 0;
        //player dy speed
        int dy = 0;
        //fliptrack for rotating player
        int flipTrack = 2;
        //player picture
        Image pacman = Image.FromFile(Application.StartupPath + @"/pacman.png", true);
        //enemy picture
        Image enemyImage2 = Image.FromFile(Application.StartupPath + @"/enemy2.png", true);
        //enemy picture
        Image enemyImage3 = Image.FromFile(Application.StartupPath + @"/enemy3.png", true);

        private void Form1_Load(object sender, EventArgs e)
        {
            //sets music to music player
            music.SoundLocation = Application.StartupPath + @"\music.wav";
            //plays music
            music.PlayLooping();
            //calls addObnjects method to add the objects
            addObjects();

            //makes paint event
            this.Paint += Form1_Paint;
            //makes keydown event
            this.KeyDown += Form1_KeyDown;
            //Creates a timer
            timer = new Timer();
            //Creates method for timer
            timer.Tick += Timer_Tick;
            //Sets the timer interval to 1000/12
            timer.Interval = 1000 / 12;
            //Starts timer
            timer.Start();
        }

        //method for adding all objects on the screen
        private void addObjects()
        {
            //adds player
            player = new Rectangle(25, 25, 50, 50);
            //adds enemy1
            enemy1 = new Rectangle(475, 175, 50, 50);
            //adds enemy2
            enemy2 = new Rectangle(225, 525, 50, 50);
            //adds enemy3
            enemy3 = new Rectangle(725, 525, 50, 50);
            //adds a barrier outside wall
            wall.Add(new Rectangle(0, 0, ClientSize.Width, 25));
            //adds a barrier outside wall
            wall.Add(new Rectangle(0, 0, 25, ClientSize.Height));
            //adds a barrier outside wall
            wall.Add(new Rectangle(0, ClientSize.Height - 25, ClientSize.Width, 25));
            //adds a barrier outside wall
            wall.Add(new Rectangle(ClientSize.Width - 25, 0, 25, ClientSize.Height));
            //adds a barrier inside wall
            wall.Add(new Rectangle(225, 0, 50, 500));
            //adds a barrier inside wall
            wall.Add(new Rectangle(475, 250, 50, 500));
            //adds a barrier inside wall
            wall.Add(new Rectangle(725, 0, 50, 500));

            //runs 17 times
            for (int j = 1; j < (ClientSize.Height / 50) + 1; j++)
            {
                //runs 21 times
                for (int i = 1; i < (ClientSize.Width / 50) + 1; i++)
                {
                    //adds a gold rectangle at the x coord of i and y coord of j
                    gold.Add(new Rectangle((i * 50) - 5, (j * 50) - 5, 10, 10));
                }
            }
        }

        //paint event method to color all objects
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //goes through all wall rectangles
            for (int i = 0; i < wall.Count; i++)
            {
                //paints wall darkblue
                e.Graphics.FillRectangle(Brushes.DarkBlue, wall[i]);
            }
            //goes through all gold rectangles
            for (int i = 0; i < gold.Count; i++)
            {
                //paints gold gold
                e.Graphics.FillRectangle(Brushes.Gold, gold[i]);
            }
            //adds the pacman image to the player rectangle
            e.Graphics.DrawImage(pacman, player);
            //adds the enemy image to the enemy rectangle
            e.Graphics.DrawImage(enemyImage3, enemy1);
            //adds the enemy image to the enemy rectangle
            e.Graphics.DrawImage(enemyImage2, enemy2);
            //adds the enemy image to the enemy rectangle
            e.Graphics.DrawImage(enemyImage3, enemy3);
            //writes score on the screen
            e.Graphics.DrawString("Score: " + Convert.ToString(score), new Font("Arial", 16), new SolidBrush(Color.White), 0, 0);
        }

        //event for setting the direction of the player by the arrow keys
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //checks if up arrow is pressed
            if (e.KeyCode == Keys.Up)
            {
                //sets dx to 0
                dx = 0;
                //sets dy to -25
                dy = -25;

                //if fliptrack is 3
                if (flipTrack == 3)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    //sets fliptrack to 4
                    flipTrack = 4;
                }
                //if fliptrack is 2
                else if (flipTrack == 2)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    //sets fliptrack to 4
                    flipTrack = 4;
                }
                //if fliptrack is 1
                else if (flipTrack == 1)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    //sets fliptrack to 4
                    flipTrack = 4;
                }
            }
            //checks if down arrow is pressed
            if (e.KeyCode == Keys.Down)
            {
                //sets dx to 0
                dx = 0;
                //sets dy to 25
                dy = 25;

                //if fliptrack is 4
                if (flipTrack == 4)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    //sets fliptrack to 3
                    flipTrack = 3;
                }
                //if fliptrack is 2
                else if (flipTrack == 2)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    //sets fliptrack to 3
                    flipTrack = 3;
                }
                //if fliptrack is 1
                else if (flipTrack == 1)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    //sets fliptrack to 3
                    flipTrack = 3;
                }
            }
            //checks if right arrow is pressed
            if (e.KeyCode == Keys.Right)
            {
                //sets dx to 25
                dx = 25;
                //sets dy to 0
                dy = 0;

                //if fliptrack is 1
                if (flipTrack == 4)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    //sets fliptrack to 2
                    flipTrack = 2;
                }
                //if fliptrack is 1
                else if (flipTrack == 3)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    //sets fliptrack to 2
                    flipTrack = 2;
                }
                //if fliptrack is 1
                else if (flipTrack == 1)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    //sets fliptrack to 2
                    flipTrack = 2;
                }
            }
            //checks if left arrow is pressed
            if (e.KeyCode == Keys.Left)
            {
                //sets dx to -25
                dx = -25;
                //sets dy to 
                dy = 0;

                //if fliptrack is 4
                if (flipTrack == 4)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    //sets fliptrack to 1
                    flipTrack = 1;
                }
                //if fliptrack is 3
                else if (flipTrack == 3)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    //sets fliptrack to 1
                    flipTrack = 1;
                }
                //if fliptrack is 2
                else if (flipTrack == 2)
                {
                    //rotates player accoringly
                    pacman.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    //sets fliptrack to 1
                    flipTrack = 1;
                }
            }
        }

        //timer event method which runs the game
        private void Timer_Tick(object sender, EventArgs e)
        {
            //sets lastSpot to player rectangle
            lastSpot = player;
            //moves x of player
            player.X += dx;
            //moves y of player
            player.Y += dy;
            //moves enemy1
            enemy1.X += enemy1dx;
            //moves enemy2
            enemy2.X += enemy2dx;
            //moves enemy3
            enemy3.X += enemy3dx;

            //calls collisionChecks method to check for collisions
            collisionChecks();

            //checks if score is greater than or equal to 236
            if (score >= 236)
            {
                //stops timer
                timer.Stop();
                //shows game won message box
                MessageBox.Show("Congrats, you won!");
                //ends program
                Application.Exit();
            }

            //checks if player has hit an enemy
            if(player.IntersectsWith(enemy1) || player.IntersectsWith(enemy2) || player.IntersectsWith(enemy3))
            {
                //stops timer
                timer.Stop();
                //stops music
                music.Stop();
                //shows game over message box
                MessageBox.Show("Game Over!");
                //exits application
                Application.Exit();
            }

            //refreshes screen
            this.Invalidate();
        }

        //collisionChecks method for checking for any collisions
        private void collisionChecks()
        {
            //runs through wall list
            for (int j = 0; j < wall.Count; j++)
            {
                //checks if player has hit wall
                if (player.IntersectsWith(wall[j]))
                {
                    //moves player to lastspot
                    player = lastSpot;
                }
                //checks if enemy1 has hit wall
                if (enemy1.IntersectsWith(wall[j]))
                {
                    //changes enemy direction
                    enemy1dx = enemy1dx * -1;
                }
                //checks if enemy2 has hit wall
                if (enemy2.IntersectsWith(wall[j]))
                {
                    //changes enemy direction
                    enemy2dx = enemy2dx * -1;
                }
                //checks if enemy3 has hit wall
                if (enemy3.IntersectsWith(wall[j]))
                {
                    //changes enemy direction
                    enemy3dx = enemy3dx * -1;
                }
                
                //runs through gold list
                for (int i = 0; i < gold.Count; i++)
                {
                    //checks if gold has hit wall
                    if (gold[i].IntersectsWith(wall[j]))
                    {
                        //removes gold
                        gold.RemoveAt(i);
                        //subtracts one from i
                        i--;
                    }
                    //checks if gold has hit player
                    if (gold[i].IntersectsWith(player))
                    {
                        //adds one to score
                        score++;
                        //removes gold
                        gold.RemoveAt(i);
                        //subtracts one from i
                        i--;
                    }
                }
            }
        }
    }
}
