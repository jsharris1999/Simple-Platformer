using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Platformer.Sprite;
using System.Windows.Input;

namespace Platformer
{
    public partial class Engine : Form
    {
        public static Character character = new Character(100,100);
        public static Sprite canvas = new Sprite();
        public static Enemy Zerg = new Enemy(500, 100);
        public static Rectangle rect = new Rectangle(0, 0, 1400, 900, 200);
        public static int enemyCount = 0;
        public static bool win = false;
        public static bool lose = false;
        public static TextSprite text = new TextSprite(0, 0, "Victory!");
        public static TextSprite loss = new TextSprite(0, 0, "Defeat!\nPress r to restart.");
        public static int lastUpdate = 0;
        public static Engine form;
        public Thread ActThread;
        public Thread PaintThread;
        public static int fps = 60;
        public static double running_fps = 60.0;
        public static bool rendering = false;
        public static bool updating = false;
        public static bool resetting = false;
        public static int gameState = 0;
        public static Goal goal =new Goal(0,0);
        public static int level = 1;
        public static String map = Properties.Resources.level1;
        public static int width;
        public static int height;


        public static void Builder()
        {
            //String map = Properties.Resources.level1;
            win = false;
            Character.bulletnum = 0;
            String[] lines = map.Split('\n');
            width = lines[0].Length - 1;
            height = lines.Length;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    PictureSprite background = new PictureSprite(Properties.Resources.wall2, i * 100, j * 100);
                    canvas.add(background);
                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (lines[j][i] == 'g')
                    {
                        goal = new Goal(i * 100, j * 100);
                        canvas.add(goal);
                        // Console.WriteLine("asdf");
                    }
                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (lines[j][i] == 'w')
                    {
                        Wall wall = new Wall(i * 100, j * 100);
                        canvas.csAdd(wall);
                    }
                }
            }
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    
                    
                    if (lines[j][i] == 'c')
                    {
                        character = new Character(i*100, j*100);
                        character.alive = true;
                        canvas.csAdd(character);
                    }

                }

            }
            canvas.add(rect);
            canvas.add(text);
        }

        public Sprite Canvas
        {
            set { canvas = value; }
            get { return canvas; }
        }

        public static void WinCon()
        {
            if (character.GoalCollision((int)goal.X,(int)goal.Y) == true)
            {
                win = true;
            }
            else
            {
                win = false;
            }
        }

        public Engine()
        {
            DoubleBuffered = true;
            form = this;
            Size = new Size(form.ClientSize.Width, form.ClientSize.Height);
            StartPosition = FormStartPosition.CenterScreen;
            PaintThread = new Thread(new ThreadStart(render));
            ActThread = new Thread(new ThreadStart(update));
            PaintThread.Start();
            ActThread.Start();
            canvas.add(rect);
            canvas.add(text);
            canvas.add(loss);
        }

        public static void reset()
        {
            canvas.RemoveAll();
            resetting = true;
            Builder();
            /*
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    PictureSprite background = new PictureSprite(Properties.Resources.wall2, i * 100, j * 100);
                    canvas.add(background);
                }
            }
            Goal goal = new Goal(1100, 500);
            canvas.add(goal);
            character = new Character(100, 100);
            character.alive = true;
            canvas.csAdd(character);
            for (int i = 0; i < 13; i++)
            {
                Wall wall = new Wall(i * 100, 0);
                canvas.csAdd(wall);
                wall = new Wall(i * 100, 600);
                canvas.csAdd(wall);
            }
            for (int i = 0; i < 7; i++)
            {
                Wall wall = new Wall(0, i * 100);
                canvas.csAdd(wall);
                wall = new Wall(1200, i * 100);
                canvas.csAdd(wall);
            }
            //canvas.csAdd(Zerg);
            canvas.add(rect);
            canvas.add(text);
            canvas.add(loss);
            enemyCount = 1;*/
            resetting = false;
        }

        public static void render()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                running_fps = .9 * running_fps + .1 * 1000.0 / (temp - now).TotalMilliseconds;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                if (resetting) continue;
                if (win)
                {
                    Console.WriteLine(level);
                    level += 1;
                    if(level == 2)
                    {
                        map = Properties.Resources.level2;
                        reset();
                    }
                    if (level == 3)
                    {
                        map = Properties.Resources.level3;
                        reset();
                    }
                    if (level == 4)
                    {
                        map = Properties.Resources.level4;
                        reset();
                    }
                    if (level == 5)
                    {
                        map = Properties.Resources.level5;
                        reset();
                    }
                    if (level == 6)
                    {
                        map = Properties.Resources.level6;
                        reset();
                    }
                    if (level == 7)
                    {
                        map = Properties.Resources.level7;
                        reset();
                    }
                    if (level == 8)
                    {
                        map = Properties.Resources.level8;
                        reset();
                    }
                    if (level == 9)
                    {
                        map = Properties.Resources.level9;
                        reset();
                    }
                    if (level == 10)
                    {
                        map = Properties.Resources.level10;
                        reset();
                    }
                    if (level == 11)
                    {
                        map = Properties.Resources.winscreen;
                        reset();
                        rect.setColor(Color.FromArgb(200, Color.Gainsboro));
                        rect.setVisibility(true);
                        text.changeLocation((form.ClientSize.Width), (form.ClientSize.Height));
                        text.setVisibility(true);
                        
                    }
                }
                else if (!character.alive && !win)
                {
                    rect.setColor(Color.FromArgb(200, Color.Red));
                    rect.setVisibility(true);
                    loss.changeLocation((form.ClientSize.Width / 2) - 50, (form.ClientSize.Height / 2) - 50);
                    loss.setVisibility(true);
                    lose = true;
                }
                else
                {
                    rect.setVisibility(false);
                    text.setVisibility(false);
                    loss.setVisibility(false);
                    win = false;
                    lose = false;
                }
                rendering = true;
                form.Invoke(new MethodInvoker(form.Refresh));
                rendering = false;
            }
        }

        public static void update()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                if (resetting) continue;
                updating = true;
                WinCon();
                canvas.update();
                lastUpdate++;
                if (!rendering)
                {
                    updating = true;
                    lastUpdate = 0;
                    canvas.queueClear();
                    canvas.updateAllTracking();
                    updating = false;
                }
            }
        }

        

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (Cursor.Position.X >= 100 && Cursor.Position.X <= 400)
            {
                if (Cursor.Position.Y >= 100 && Cursor.Position.Y <= 200)
                {
                    gameState += 1;
                    Console.WriteLine(gameState);
                    if(gameState==1)
                    {
                        Builder();
                    }
                }
            }

        }

        protected override void OnMouseEnter(EventArgs e)
        {
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Left)
            {
                character.image = Properties.Resources.guy2;
                character.Vx = -5;
            }
            else if (e.KeyCode == Keys.Right)
            {
                character.image= Properties.Resources.guy;
                character.Vx = 5;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (PhysicsSprite.inAir == false)
                {
                    character.Vy = -6;
                    PhysicsSprite.inAir = true;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                character.Vy = 5;
            }
            else if (e.KeyCode == Keys.Z)
            {
                character.shoot(1);
            }
            else if (e.KeyCode == Keys.X)
            {
                character.shoot(2);
            }
            else if (e.KeyCode == Keys.R)
            {
                reset();
            }
            else if (e.KeyCode == Keys.A)
            {
                if (Character.bulletnum >= 1)
                {
                    foreach (Sprite s in FriendlyBullet.fsprites)
                    {
                            s.Kill();
                    }
                        
                }
                Character.bulletnum = 0;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Right)
            {
                character.Vx += -5;
                // Console.WriteLine("right ");
            }
            if (e.KeyCode == Keys.Left)
            {
                character.Vx += 5;
                // Console.WriteLine("left ");
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        /*
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            fixScale();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            fixScale();
        }


        public void fixScale()
        {
            canvas.Scale = Math.Min(ClientSize.Width, ClientSize.Height) / (Math.Max(height, width) * 100.0f);
            canvas.X = (ClientSize.Width - (100 * width * canvas.Scale)) / 2;
            canvas.Y = (ClientSize.Height - (100 * height * canvas.Scale)) / 2;
        }
        */
        

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ActThread.Abort();
            PaintThread.Abort();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            canvas.render(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
