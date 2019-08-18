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

namespace Platformer
{
    public class Character : PhysicsSprite
    {
        public bool alive;
        public static Boolean isTouchingbool = false;
        public static int bulletnum = 0;
        //public DateTime lastshot = DateTime.Now;
        //public TimeSpan firerate = new TimeSpan(0, 0, 1);

        public Character(int x, int y) : base(Properties.Resources.guy, x, y)
        {
            setMotionModel(2);
            image = Properties.Resources.guy;
            X = x;
            Y = y;
            alive = true;
        }

      /*  public Boolean isTouching()
        {
            List<CollisionSprite> list = getCollisions();
            foreach (CollisionSprite s in list)
            {
                if (s.GetType() == typeof(FriendlyBullet))
                {
                    isTouchingbool = true;
                    return true;
                }
            }
            isTouchingbool = false;
            return false;
        }*/

        public override void act()
        {
            base.act();
            if ((Vx > 0 && Ax > 0) || (Vx < 0 && Ax < 0))
            {
                Vx = 0;
                Ax = 0;
            }
            if ((Vy > 0 && Ay > 0) || (Vy < 0 && Ay < 0))
            {
                Vy = 0;
                Ay = 0;
            }
        }

        public bool GoalCollision(int goalX, int goalY)
        {
            double c1x = X + (width / 2);
            double c1y = Y + (height / 2);
            double r1x = width / 2;
            double r1y = height / 2;
            double c2x = goalX + (50);
            double c2y = goalY + (50);
            double r2x = 50;
            double r2y = 50;
            if (Math.Abs(c1x - c2x) < (r2x + r1x))
            {
                if (Math.Abs(c1y - c2y) < (r2y + r1y))
                {
                    return isTouchingbool=true;
                }
            }
            return isTouchingbool=false;
        }

        public override void Kill()
        {
            base.Kill();
            alive = false;
        }

        public void shoot(int dir)
        {
            if (!alive) return;
            FriendlyBullet bullet = new FriendlyBullet((int)(X + 100), (int)(Y+50));
            if (bulletnum == 0)
            {
                //if (DateTime.Now - lastshot <= firerate) return;
                bullet.X = X + 100;
                bullet.Y = Y+50;
                bullet.Vx = 25f;
                if (dir == 1)
                {
                    bullet.X = X - width;
                    bullet.Vx *= -1;
                }
                else if (dir == 2)
                {
                    bullet.X = X + (width);
                }
                Engine.canvas.csAdd(bullet);
            } 
            bulletnum += 1;
            //lastshot = DateTime.Now;
        }

    }
}