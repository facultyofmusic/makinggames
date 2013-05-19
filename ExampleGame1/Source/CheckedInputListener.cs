using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGame1
{
    public interface CheckedInputListener
    {
        void inputPressed(Vector2 position);

        void inputDragged(Vector2 position);

        void inputReleased(Vector2 position);

        void inputFired(Vector2 position, int state);
    }
}
