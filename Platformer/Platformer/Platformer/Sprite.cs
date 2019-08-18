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
    public class Sprite
    {
        private Sprite parent = null;

        //instance variable
        public float x;

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float y;

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        private float scale = 1;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private float rotation = 0;

        /// <summary>
        /// The rotation in degrees.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }


        public List<Sprite> children = new List<Sprite>();
        public List<Sprite> toAdd = new List<Sprite>();
        public List<Sprite> toRemove = new List<Sprite>();
        public List<CollisionSprite> cchildren = new List<CollisionSprite>();
        public List<CollisionSprite> ctoAdd = new List<CollisionSprite>();
        public List<CollisionSprite> ctoRemove = new List<CollisionSprite>();

        public virtual void Kill()
        {
            parent.remove(this);
        }

        //methods
        public void render(Graphics g)
        {
            Matrix original = g.Transform.Clone();
            g.TranslateTransform(x, y);
            g.ScaleTransform(scale, scale);
            g.RotateTransform(rotation);
            paint(g);
            foreach (Sprite s in children)
            {
                s.render(g);
            }
            g.Transform = original;
        }

        public void update()
        {
            act();
            foreach (Sprite s in children)
            {
                s.update();
            }
        }

        public virtual void paint(Graphics g)
        {

        }

        public virtual void act()
        {

        }

        public void add(Sprite s)
        {
            s.parent = this;
            toAdd.Add(s);
        }

        public void cAdd(CollisionSprite s)
        {
            ctoAdd.Add(s);
        }

        public void csAdd(CollisionSprite s)
        {
            add(s);
            cAdd(s);
        }

        public void remove(Sprite s)
        {
            toRemove.Add(s);
        }

        public void cRemove(CollisionSprite s)
        {
            ctoRemove.Add(s);
        }

        public void csRemove(CollisionSprite s)
        {
            remove(s);
            cRemove(s);
        }

        public void RemoveAll()
        {
            foreach (Sprite s in children)
            {
                remove(s);
            }
            foreach (CollisionSprite s in cchildren)
            {
                cRemove(s);
            }
        }

        public void queueClear()
        {
            foreach (Sprite s in toRemove)
            {
                children.Remove(s);
            }
            toRemove = new List<Sprite>();
            foreach (Sprite s in toAdd)
            {
                children.Add(s);
            }
            toAdd = new List<Sprite>();
            foreach (CollisionSprite s in ctoRemove)
            {
                cchildren.Remove(s);
                foreach (CollisionSprite c in cchildren)
                {
                    c.untrack(s);
                    s.untrack(c);
                }
            }
            ctoRemove = new List<CollisionSprite>();
            foreach (CollisionSprite s in ctoAdd)
            {
                cchildren.Add(s);
                foreach (CollisionSprite c in cchildren)
                {
                    c.track(s);
                    s.track(c);
                }
            }
            ctoAdd = new List<CollisionSprite>();
        }

        public void updateAllTracking()
        {
            foreach (CollisionSprite c in cchildren)
            {
                c.updateTracking();
            }
        }
    }

    public class TextSprite : Sprite
    {
        public String text;
        public Boolean visible;
        public int x;
        public int y;
        public int font = 24;

        public TextSprite(int x, int y, String text)
        {
            this.x = x;
            this.y = y;
            this.text = text;
            visible = false;
        }

        public void changeLocation(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void setVisibility(Boolean visible)
        {
            this.visible = visible;
        }

        public void fontResize(int width, int height)
        {
            font = (int)(12 + (12 * (Math.Min(width, height) / 705)));
        }

        public override void paint(Graphics g)
        {
            //base.paint(g);
            if (visible) g.DrawString(text, new Font("Herculanum", font), Brushes.Black, x, y);
        }
    }
}