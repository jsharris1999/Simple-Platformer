﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    public class Enemy : PhysicsSprite
    {
        private Boolean left = true;
        private Random r = new Random();
        public DateTime lastshot = DateTime.Now;
        public TimeSpan firerate = new TimeSpan(0, 0, 0, 2, 50);

        public Enemy() : base(Properties.Resources.hydralisk)
        {
            Vx = 4f;
            Gy = 0.25f;
            setMotionModel(2);
            Engine.enemyCount++;
        }

        public Enemy(int x, int y) : base(Properties.Resources.hydralisk, x, y)
        {
            Vx = 4f;
            Gy = 0.25f;
            setMotionModel(2);
            Engine.enemyCount++;
        }

        public void Shoot()
        {
            Bullet bullet = new Bullet((int)(X + 2 * width * Scale * 1.1f), (int)(Y + height * Scale / 2));
            if (left)
            {
                bullet.X = X - width;
                bullet.Vx *= -1;
            }
            Engine.canvas.csAdd(bullet);
        }

        public bool isWall()
        {
            X += Vx;
            if (getCollisions().Count > 0)
            {
                X -= Vx;
                return true;
            }
            X -= Vx;
            return false;
        }

        public bool isCeiling()
        {
            Y += Vy;
            if (getCollisions().Count > 0)
            {
                Y -= Vy;
                return true;
            }
            Y -= Vy;
            return false;
        }

        public void killCharacter()
        {
            X += Vx;
            List<CollisionSprite> list = getCollisions();
            X -= Vx;
            foreach (CollisionSprite s in list)
            {
                if (s.GetType() == typeof(Character)) s.Kill();
            }
        }

        public override void act()
        {
            base.act();
            killCharacter();
            if (r.NextDouble() < .01) Vy = -20;
            if (isWall()) Vx *= -1;
            if (isCeiling()) Vy = 0;
            if (r.NextDouble() < .01)
            {
                Shoot();
                lastshot = DateTime.Now;
            }
            if (Vx < 0) left = true;
            if (Vx > 0) left = false;
            if (DateTime.Now - lastshot >= firerate)
            {
                Shoot();
                lastshot = DateTime.Now;
            }
        }

        public override void Kill()
        {
            Engine.enemyCount--;
            base.Kill();
        }

        public override void paint(Graphics g)
        {
            base.paint(g);
        }

    }
}
