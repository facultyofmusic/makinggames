using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGame1
{
    class UT
    {
        public static Vector2 pullMouseLocation()
        {
            return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }
    }
}
