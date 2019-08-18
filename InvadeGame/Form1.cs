using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InvadeGame.Helpers;

namespace InvadeGame
{
    public partial class Form1 : Form
    {
        IList<Alien> listAlien = new List<Alien>();
        IList<Bullet> listBullet = new List<Bullet>();
        IList<Tent> listTent = new List<Tent>();

        Ship ship;

        SoundPlayer bullentSound = new SoundPlayer();
        SoundPlayer tentSound = new SoundPlayer();
        SoundPlayer boosSound = new SoundPlayer();
        public Form1()
        {
            InitializeComponent();
            CreateAlien();  //生成外星人
            CreateTent();   //生成障碍物
            CreateShip();   //生成飞船
            CreateGroupBox();  //生成游戏说明
            bullentSound.SoundLocation=@"D:\C#\Game\InvadeGame\Sounds\ASMAEXP.wav";
            tentSound.SoundLocation = @"D:\C#\Game\InvadeGame\Sounds\LAZER.wav";
            boosSound.SoundLocation = @"D:\C#\Game\InvadeGame\Sounds\BOSS5.wav";
        }

        private void CreateGroupBox()
        {
            GroupBox gb = new GroupBox();
            gb.Size = new Size(200, 300);
            gb.Location = new Point(this.Width-230, 50);
            gb.Text = "游戏说明";
            this.Controls.Add(gb);
            Label lb = new Label();
            lb.Dock = DockStyle.Fill;
            lb.Text = "   使用键盘的方向键来控制飞机的位置，空格键来发射子弹，" +
                      "击毁所有外星人即可获得游戏胜利！但是，当你碰到外星人或者障碍物，你会死亡！";
            gb.Controls.Add(lb);
            lb.Location = new Point(30, 50);
        }

        private void CreateTent()
        {
            for (int i = 0; i < 5; i++)
            {
                Point point = new Point(110 + (300 * i), 600);
                listTent.Add(new Tent(this, point));
            }
        }

        private void CreateBullent()
        {
            Point point = new Point(ship.Location.X+35, ship.Location.Y-50);
            //bullentSound.PlaySync();
            listBullet.Add(new Bullet(this, point));
        }

        private void CreateShip()
        {
            Point point = new Point(this.Width/2, this.Height-200);
            ship = new Ship(this, point);
        }

        private void CreateAlien()
        {
            Point point = new Point();
            for(int i = 0; i < 3; i++)
            {
                Alien warper = null;
                switch (i)
                {
                    case 0:
                        {
                            for (int j=0;j<13;j++)
                            {
                                point =new Point (70 + (120 * j), 0);
                                warper = new BlueSprite(this,point);
                                listAlien.Add(warper);
                                point = new Point(70 + (120 * j), 100);
                                warper = new BlueSprite(this, point);
                                listAlien.Add(warper);
                            }
                            break;
                        }
                    case 1:
                        {
                            for(int j = 0; j < 13; j++)
                            {
                                point = new Point(70 + (120 * j), 200);
                                warper = new GreenSprite(this, point);
                                listAlien.Add(warper);
                                point = new Point(70 + (120 * j), 300);
                                warper = new GreenSprite(this, point);
                                listAlien.Add(warper);
                            }
                            break;
                        }
                    case 2:
                        {
                            for (int j = 0; j < 13; j++)
                            {
                                point = new Point(70 + (120 * j), 400);
                                warper = new RedSprite(this, point);
                                listAlien.Add(warper);
                            }
                            break;
                        }
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if(keyData==Keys.Up||keyData==Keys.Down||keyData==Keys.Right||keyData == Keys.Left) 
            {
                Moveship(keyData);
                return false;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        private void Moveship(Keys k)
        {
            switch (k)
            {
                case Keys.Up:
                    {
                        if (ship.Location.Y > 0)
                        {
                            ship.GoUp();
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if (ship.Location.Y + ship.size.Height < this.Height-50)
                        {
                            ship.GoDown();
                        }
                        break;
                    }
                case Keys.Right:
                    {
                        if (ship.Location.X + ship.size.Width < this.Width - 250)
                        {
                            ship.GoRight();
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        if (ship.Location.X>0)
                        {
                            ship.GoLeft();
                        }
                        break;
                    }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                CreateBullent();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                TramsformAlien();
                CheckAlienReachBorder();
                foreach (Bullet bullet in listBullet)
                {
                    if (bullet.IsAlive == false) { continue; }
                    bullet.GoUp();
                    CheckDestroy(bullet);
                }
                CheckAlive();
                
            }
            catch 
            {
                return;
            }
            
        }

        /// <summary>
        /// 飞船是否存活
        /// </summary>
        private void CheckAlive()
        {
            foreach (Tent tent in listTent)
            {
                if (tent.IsAlive == false) { continue; }
                //碰到障碍物 游戏结束
                if (tent.CheckCollision(ship)) { GameOver(tent); }
            }
            foreach (Sprite sprite in listAlien)
            {
                if (sprite.IsAlive == false) { continue; }
                //碰到外星人游戏结束
                if (sprite.CheckCollision(ship)) { GameOver(sprite); }
            }
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        /// <param name="alien"></param>
        public void GameOver(Alien alien)
        {
            ship.IsAlive = false;
            alien.IsAlive = false;
            MessageBox.Show("Game Over!");
            listAlien.Clear();
            listTent.Clear();
            listTent.Clear();
            this.Controls.Clear();
        }

        /// <summary>
        /// 击中目标
        /// </summary>
        /// <param name="bullet"></param>
        private void CheckDestroy(Bullet bullet)
        {
            foreach (Tent tent in listTent)
            {
                if (tent.IsAlive == false) { continue; }
                if (tent.CheckCollision(bullet))
                {
                    tent.Hited();
                    //tentSound.PlaySync();
                    bullet.IsAlive = false;
                    listAlien.Remove(bullet);
                    break;
                }
            }
            foreach (Sprite sprite in listAlien)
            {
                if (sprite.IsAlive == false) { continue; }
                if (sprite.CheckCollision(bullet))
                {
                    sprite.IsAlive = false;
                    //boosSound.PlaySync();
                    bullet.IsAlive = false;
                    listAlien.Remove(bullet);
                    break;
                }
            }
        }

        /// <summary>
        /// 检查是否到达边缘
        /// </summary>
        private void CheckAlienReachBorder()
        {
            try
            {
                double screenWidth = this.Width - 300;
                foreach (Sprite sprite in this.listAlien)
                {
                    if (sprite.IsAlive == false) { continue; }
                    if (sprite.Location.X + sprite.size.Width >= screenWidth)
                    {
                        DownAlien(-1);
                    }
                    else if (sprite.Location.X<10)
                    {
                        DownAlien(1);
                    }
                }
            }
            catch
            {
                return;
            }
        }

       
        /// <summary>
        /// 外星人下降
        /// </summary>
        /// <param name="v"></param>
        private void DownAlien(int v)
        {
            foreach (Sprite sprite in this.listAlien)
            {
                sprite.locationSpeed = v;
                sprite.rotateSpeed = 10*v;
                sprite.Down();
            }
        }

        /// <summary>
        /// 外星人移动
        /// </summary>
        private void TramsformAlien()
        {
            try
            {
                foreach (Sprite sprite in this.listAlien)
                {
                    if (sprite.IsAlive == false) { continue; }
                    sprite.Move();
                }
            }
            catch
            {
                return;
            }
            
        }
    }
}
