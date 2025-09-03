using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace Football5._0
{
    public partial class GameForm : Form
    {
        int secondsLeft;

        int gameScore;

        Timer gameTimer;

        public BallEntity ball;

        FootballPlayerPicturesManager picturesManager;

        public GameForm()
        {
           secondsLeft = 69;

            gameScore = 0;

            InitializeComponent();
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();

        }

        public void Init()
        {
            picturesManager = new FootballPlayerPicturesManager(
                pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6);

            ball = new BallEntity(new Size(50, 50), 20, picturesManager);

            Timer timer1 = new Timer();
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(Update);
            timer1.Start();

            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            var playerType = ball.myPhysics.ApplyRules();

            if (playerType == PlayerType.GoalKeeper)
            {
                gameScore++;
            }
            else if (playerType == PlayerType.Attacker)
            {
                gameScore--;
            }

            if (gameScore >= 0)
            {
                label2.Text = $"rizz energy score: {gameScore}";
            }
            else
            {
                label2.Text = $"now it is cringe score: {Math.Abs(gameScore)}";
            }

            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            ball.Drawsprite(g);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ball.myPhysics.AddForceYQuickly(0.2f);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            secondsLeft--;

            if (secondsLeft > 0)
            {
                label1.Text = $"time: {secondsLeft} sec.";
            }
            else
            {
                gameTimer.Stop();
                button1.Enabled = false;

                if (gameScore >= 0)
                {
                    MessageBox.Show($"Game over, your rizz is: {gameScore}! Niiiiice!");
                }
                else
                {
                    MessageBox.Show($"Game over, your cringe level is: {gameScore}! Too cringy!");
                }

                Close();
            }
        }
    }
}
