using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Enemy
    {
        public Vector2 pos { get; set; }
        public Rectangle rec { get; set; }
        public bool isRight { get; set; }
        public int hp = 100;

        public Enemy(Vector2 pos,Rectangle rec, bool isRight)
        {
            this.pos = pos;
            this.rec = rec;
            this.isRight = isRight;
        }

    }
}
