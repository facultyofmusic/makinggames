using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGame1
{
    public class Camera2D
    {
        Vector2 position;

        public Camera2D()
        {
            position = new Vector2(0, 0);
        }

        public void update(Vector2 wantedPosition)
        {

            // SLOW-MO MOVEMENTS!  Don't just GET there.  ye know.
            position.X += (wantedPosition.X - position.X) / 4f;

            position.Y += (wantedPosition.Y - position.Y) / 4f;
        }

        public Vector2 getPosition()
        {
            return position;
        }
    }
}
