using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    public class FriendlyBullet : PhysicsSprite
    {
        public static List<FriendlyBullet> fsprites = new List<FriendlyBullet>();
        public FriendlyBullet(int x, int y) : base(Properties.Resources.block, x, y)
        {
            setMotionModel(1);
            Vx = 10f;
            fsprites.Add(this);
        }

        public override void Kill()
        {
            base.Kill();
            Engine.canvas.csRemove(this);
        }

        public void killCharacter()
        {
            X += Vx;
            List<CollisionSprite> list = getCollisions();
            X -= Vx;
            foreach (CollisionSprite s in list)
            {
                if (s.GetType() == typeof(Enemy))
                {
                    s.Kill();
                }
            }
            //if (list.Count > 0) this.Kill();
        }

        public override void act()
        {

            base.act();
            killCharacter();
        }
    }
}