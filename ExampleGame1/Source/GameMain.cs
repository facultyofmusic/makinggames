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
         * Game view variables
         * 
         * 
         */
        Camera2D camera;
        Vector2 lastInputLocation = new Vector2(0, 0);
        Vector2 origin;

        /**
         * Player related.
         * 
         */
        Player player;


        /**
         * Testing variables
         */
        Texture2D character;
        WSoundEffect crash, cat, song;
        Texture2D testImage;
        Texture2D centerTest;
        Texture2D cursor;

        /**
         * Default Constructor
         */
        public GameMain()
        {
            try{
                _graphics = new GraphicsDeviceManager(this);
                _graphics.PreferMultiSampling = true;
                Content = new ContentManager(Content.ServiceProvider, "Content");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex.Message + "\n" + ex.StackTrace);
            }
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
            camera = new Camera2D();
            player = new Player();
            origin = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2);

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
                centerTest = this.Content.Load<Texture2D>("centerTest");
                cursor = this.Content.Load<Texture2D>("cursor");

                //sounds
                crash = new WSoundEffect(@"Content\se_crash.wav");
                cat = new WSoundEffect(@"Content\se_cat00.wav");
                song = new WSoundEffect(@"Content\se_background.wav");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EXCEPTION: " + ex.Message + "\n" + ex.StackTrace);
            }

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
            lastInputLocation.X+= 0.01f;

            // we should add a buffer of about 10 for the counter before triggering off game start actions
            if (counter == 10)
            {
                song.Play();
            }


            /**
             * Player update and logic
             * 
             */
            player.update(gameTime);

            /**
             * Update camera stuff
             */
            camera.update(player.position);
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

            //_spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, camera.Transform);
            //_spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.SaveState, camera.Transform);
            _spriteBatch.Begin();
            {
                //ORIGIN TEST.  ALWAYS DRAW AT WHERE THE ORIGIN IS ON SCREEN
                _spriteBatch.Draw(centerTest, origin, null, Color.White, 0, new Vector2(centerTest.Width / 2, centerTest.Height / 2), new Vector2(1f, 1), SpriteEffects.None, 0);
                //CURSOR FOR MOUSE INPUT REFERENCE
                _spriteBatch.Draw(cursor, UT.pullMouseLocation(), null, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);
                Debug.WriteLine("HEIGHT: " + centerTest.Width / 2);

                _spriteBatch.Draw(character, lastInputLocation, Color.Red);
                _spriteBatch.Draw(character, getRenderCoordinate(camera.getPosition()), Color.Green);
                _spriteBatch.Draw(testImage, getRenderCoordinate(new Vector2(500, 500)), null, Color.White, counter/100f, new Vector2(200, 200), new Vector2(1, 1), SpriteEffects.None, 0);
                _spriteBatch.Draw(testImage, getRenderCoordinate(new Vector2(500, 500)), null, Color.White, 0, new Vector2(200, 200), new Vector2(1, 1), SpriteEffects.None, 0);
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }


        Vector2 getRenderCoordinate(Vector2 relativePosition)
        {
            return new Vector2(relativePosition.X - camera.getPosition().X, relativePosition.Y - camera.getPosition().Y);
        }

        Vector2 getRelativeToOrigin(Vector2 relativePosition)
        {
            return -(new Vector2(origin.X - relativePosition.X, origin.Y - relativePosition.Y));
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
            //crash.Play();
        }

        public void inputDragged(Vector2 position)
        {
            Debug.WriteLine("Dragged to: " + position.ToString());
            this.lastInputLocation = position;
            player.moveTowards(getRelativeToOrigin(lastInputLocation));
            Debug.WriteLine("Clicked " + getRelativeToOrigin(lastInputLocation) + " relative to origin");
        }

        public void inputReleased(Vector2 position)
        {
            Debug.WriteLine("Released at: " + position.ToString());
            this.lastInputLocation = position;
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
