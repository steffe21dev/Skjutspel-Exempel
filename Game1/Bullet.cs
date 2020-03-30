using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Bullet
    {

        public Vector2 pos { get; set; }
        public Rectangle rectangle { get; set; }
        public int speed { get; set; }


        public Bullet(Vector2 pos, Rectangle rectangle, int speed)
        {
            this.pos = pos;
            this.rectangle = rectangle;
            this.speed = speed;
        }

    }
}
