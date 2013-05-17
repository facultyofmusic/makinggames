using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;
using SharpDX.XAudio2;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input.Touch;

namespace ExampleGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class EventChecker
    {
        public static readonly short PRESSED = 1, DRAGGED = 2, RELEASED = 3;
        private static bool mousePressed = false;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public static void checkInput(CheckedInputListener listener)
        {
            TouchCollection touches = TouchPanel.GetState();

            foreach (TouchLocation touch in touches)
            {
                if (touch.State == TouchLocationState.Pressed)
                {
                    listener.inputFired(touch.Position, PRESSED);
                    listener.inputPressed(touch.Position);
                }
                else if (touch.State == TouchLocationState.Moved)
                {
                    listener.inputFired(touch.Position, DRAGGED);
                    listener.inputDragged(touch.Position);
                }
                else if (touch.State == TouchLocationState.Released)
                {
                    listener.inputFired(touch.Position, RELEASED);
                    listener.inputReleased(touch.Position);
                }
            }

            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                mousePressed = true;
                listener.inputFired(new Vector2(mouseState.X, mouseState.Y), PRESSED);
                listener.inputPressed(new Vector2(mouseState.X, mouseState.Y));
            }
            else if (mouseState.LeftButton == ButtonState.Released && mousePressed == true)
            {
                mousePressed = false;
                listener.inputFired(new Vector2(mouseState.X, mouseState.Y), RELEASED);
                listener.inputReleased(new Vector2(mouseState.X, mouseState.Y));
            }
            else if (mousePressed)
            {
                listener.inputFired(new Vector2(mouseState.X, mouseState.Y), DRAGGED);
                listener.inputDragged(new Vector2(mouseState.X, mouseState.Y));
            }
        }
    }
}
