using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatformGame
{
	public partial class Form1 : Form
	{

		bool goLeft, goRight, jumping, isGameOver;
		int jumpSpeed;
		int force;
		int score = 0;
		int playerSpeed = 7;

		int horizontalSpeed = 4;
		int verticalSpeed = 3;

		int enemyOneSpeed = 3;
		int enemyTwoSpeed = 1;


		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox5_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox6_Click(object sender, EventArgs e)
		{

		}

		private void MainGameTimerEvent(object sender, EventArgs e)
		{
			TxtScore.Text = "Score:" + score;

			Player.Top += jumpSpeed;

			if (goLeft)
			{
				Player.Left -= playerSpeed;
			}
			if (goRight)
			{
				Player.Left += playerSpeed;
			}
			if (jumping && force < 0)
			{
				jumping = false;
			}
			if (jumping)
			{
				jumpSpeed = -8;
				force -= 4;
			}
			else {
				jumpSpeed = 10;
			}

			foreach (Control x in this.Controls)
			{

				if (x is PictureBox)
				{
					if ((string)x.Tag == "Platform")
					{
						if (Player.Bounds.IntersectsWith(x.Bounds))
						{
							force = 8;
							Player.Top = x.Top - Player.Height;

							if ((string)x.Name == "horizontalPlatform" && !goLeft  || (string)x.Name == "horizontalPlatform" && !goRight)
							{
								Player.Left -= horizontalSpeed;
							}
						}
						x.BringToFront();
					}
					if ((string)x.Tag == "Coin")
					{
						if (Player.Bounds.IntersectsWith(x.Bounds) && x.Visible)
						{
							x.Visible = false;
							score++;
						}
					}

					if ((string)x.Tag == "Enemy")
					{
						if (Player.Bounds.IntersectsWith(x.Bounds))
						{
							gameTimer.Stop();
							isGameOver = true;
							TxtScore.Text = "Score: " + score + Environment.NewLine + "You Hit an enemy GAME OVER";
						}
					}
				}
			}
			horizontalPlatform.Left -= horizontalSpeed;
			if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
			{
				horizontalSpeed *= -1;
			}

			verticalPlatform.Top += verticalSpeed;
			if (verticalPlatform.Top < 119 || verticalPlatform.Top > 277)
			{
				verticalSpeed *= -1;
			}

			EnemyOne.Left -= enemyOneSpeed;
			if (EnemyOne.Left<pictureBox5.Left|| EnemyOne.Left+ EnemyOne.Width> pictureBox5.Left+pictureBox5.Width) {
				enemyOneSpeed *= -1;
			}

			EnemyTwo.Left -= enemyTwoSpeed;
			if (EnemyTwo.Left < pictureBox2.Left || EnemyTwo.Left + EnemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
			{
				enemyTwoSpeed *= -1;
			}

			if (Player.Top + Player.Height > this.ClientSize.Height +50)
			{
				TxtScore.Text = "Score: " + score + Environment.NewLine + "You fell to your death!";
				gameTimer.Stop();
				isGameOver = true;

			}

			if (Player.Bounds.IntersectsWith(Exit.Bounds) && score == 21)
			{
				gameTimer.Stop();
				isGameOver = true;
				TxtScore.Text = "You Win!";
			}
		}

		private void Player_Click(object sender, EventArgs e)
		{
		

		}

		private void KeyIsDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Left)
			{
				goLeft = true;
			}
			if (e.KeyCode == Keys.Right)
			{
				goRight = true;
			}
			if (e.KeyCode == Keys.Space && jumping == false)
			{
				jumping = true;
			}
		}

		private void KeyIsUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Left)
			{
				goLeft = false;
			}
			if (e.KeyCode == Keys.Right)
			{
				goRight = false;
			}
			if (jumping == true)
			{
				jumping = false;
			}
			if (e.KeyCode == Keys.Enter && isGameOver)
			{
				RestartGame();
			}
		}

		private void RestartGame()
		{
			goLeft = false;
			goRight = false;
			jumping = false;
			isGameOver = false;
			score = 0;
			TxtScore.Text = "Score:" + score;

			foreach (Control x in this.Controls)
			{
				if (x is PictureBox && x.Visible == false)
				{
					x.Visible = true;
				}
			}

			//reset player platforms and  enemies
			Player.Left = 24;
			Player.Top = 277;

			EnemyTwo.Left = 148;
			EnemyTwo.Top = 258;

			EnemyOne.Left = 126;
			EnemyOne.Top = 148;

			verticalPlatform.Top = 205;

			horizontalPlatform.Left = 118;

			gameTimer.Start();

		}
	}
}
