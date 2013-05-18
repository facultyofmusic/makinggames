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
    public class GameMain : Game, CheckedInputListener
    {
        /**
         * GAME GLOBAL
         * 
         * DO NOT TEMPER WITH THESE.
         */
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        int counter = 0;
        bool paused = false;

        /**
         * Testing variables
         */
        Texture2D character;
        WSoundEffect crash, cat, song;
        SpriteFont font1;
        Vector2 position = new Vector2(0, 0);
        Texture2D testImage;

        /**
         * Default Constructor
         */
        public GameMain()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content = new ContentManager(Content.ServiceProvider, "Content");
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            try {
                //images
                character = this.Content.Load<Texture2D>("character");
                testImage = this.Content.Load<Texture2D>("testImage");

                //font
                font1 = this.Content.Load<SpriteFont>("font1");

                //sounds
                crash = new WSoundEffect(@"Content\se_crash.wav");
                cat = new WSoundEffect(@"Content\se_cat00.wav");
                song = new WSoundEffect(@"Content\se_background.wav");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex.Message + "\n" + ex.StackTrace);
            }
            //images
            character = this.Content.Load<Texture2D>("character");

            //font
            //font1 = this.Content.Load<SpriteFont>("font1");

            //sounds
            crash = new WSoundEffect(@"Content\se_crash.wav");
            cat = new WSoundEffect(@"Content\se_cat00.wav");
            song = new WSoundEffect(@"Content\se_background.wav");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            base.Update(gameTime);
            EventChecker.checkInput(this);  // check events

            //GAME LOGIC EXECUTIONS
            if (!paused)
            {
                counter++;          // increment game counter if not paused
                logicalUpdate(gameTime);
            }
        }



        /**
         * 
         * 
         *      GAME LOGIC
         * 
         */
        private void logicalUpdate(GameTime gameTime)
        {
            position.X++;

            // we should add a buffer of about 10 for the counter before triggering off game start actions
            if (counter == 10)
            {
                song.Play();
            }
        }

        
       

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //A little example is response to game pauses
            if (!paused) GraphicsDevice.Clear(Color.CornflowerBlue);
            else GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();
            {
                _spriteBatch.Draw(character, position, Color.Red);
                _spriteBatch.Draw(testImage, new Vector2(0, 0), Color.White);
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }



        /**
         * Input response section.
         * 
         * 
         * 
         * 
         */
        public void inputPressed(Vector2 position)
        {
            Debug.WriteLine("Pressed at: " + position.ToString());
            crash.Play();
        }

        public void inputDragged(Vector2 position)
        {
            Debug.WriteLine("Dragged to: " + position.ToString());
        }

        public void inputReleased(Vector2 position)
        {
            Debug.WriteLine("Released at: " + position.ToString());
            this.position = position;
        }

        public void inputFired(Vector2 position, int state)
        {
        }


        /**
         * WINDOW REPONSE SECTION
         * 
         * 
         * 
         * 
         */
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);
            paused = true;
            Debug.WriteLine("PAUSE HERE!");
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);
            paused = false;
            Debug.WriteLine("RESUME NOW!");
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);
            Debug.WriteLine("EXIT NOW!");
        }

        protected override void EndRun()
        {
            base.EndRun();
            Debug.WriteLine("EXIT NOW!");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Debug.WriteLine("EXIT NOW!");
        }
    }
}
