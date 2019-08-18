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
    public class CollisionSprite : PictureSprite
    {
        public List<CollisionSprite> collisionsprites = new List<CollisionSprite>();
        public List<CollisionSprite> toTrack = new List<CollisionSprite>();
        public List<CollisionSprite> toUntrack = new List<CollisionSprite>();
        
        public List<CollisionSprite> TrackedSprites
        {
            get { return collisionsprites; }
        }

        public CollisionSprite(Image image) : base(image)
        {
            toTrack.Add(this);
        }

        public CollisionSprite(Image image,int x,int y) : base(image,x,y)
        {
            X = x;
            Y = y;
            toTrack.Add(this);
        }

        public void track(CollisionSprite s)
        {
            toTrack.Add(s);
        }

        public void untrack(CollisionSprite s)
        {
            toUntrack.Add(s);
        }

        public void updateTracking()
        {
            foreach (CollisionSprite s in toUntrack)
            {
                collisionsprites.Remove(s);
            }
            toUntrack = new List<CollisionSprite>();
            foreach (CollisionSprite s in toTrack)
            {
                collisionsprites.Add(s);
            }
            toTrack = new List<CollisionSprite>();
        }
        public List<CollisionSprite> getCollisions()
        {
            List<CollisionSprite> collisions = new List<CollisionSprite>();
            double c1x = X + (width / 2);
            double c1y = Y + (height / 2);
            double r1x = width / 2;
            double r1y = height / 2;
            foreach (CollisionSprite s in collisionsprites)
            {
                if (s == this) continue;
                double c2x = s.X + (s.width / 2);
                double c2y = s.Y + (s.height / 2);
                double r2x = s.width / 2;
                double r2y = s.height / 2;
                if(Math.Abs(c1x-c2x)<(r2x+r1x))
                {
                    if(Math.Abs(c1y-c2y)<(r2y+r1y))
                    {
                        collisions.Add(s);
                    }
                }
            }
            return collisions;
        }
    }
}
