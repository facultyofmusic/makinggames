using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ExampleGame1
{
    class Player
    {
        public Vector2 position, velocity;

        public Player()
        {
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
        }

        public void moveTowards(Vector2 where)
        {


            velocity.X += (where.X - velocity.X) / 100f;
            velocity.Y += (where.Y - velocity.Y) / 100f;
        }

        public void update(GameTime gameTime)
        {
            position += velocity;
            velocity -= velocity / 2f;
        }
    }
}
