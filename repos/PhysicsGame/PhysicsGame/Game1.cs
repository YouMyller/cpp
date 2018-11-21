using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicsGame.Objects;
using System.Collections.Generic;

namespace PhysicsGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        const int screenWidth = 1024, screenHeight = 768;

        GraphicsDeviceManager graphics;
        PresentationParameters pp;
        SpriteBatch spriteBatch;

        const bool fullScreen = false;

        static public int screenW, screenH;
        static public Vector2 screen_center;

        Rectangle screenRect, desktopRect;

        Texture2D playerSprite;
        Texture2D gunSprite;
        Texture2D bulletSprite;

        //Character player;
        Player player;
        Gun gun;
        //Bullet bullet;

        RenderTarget2D mainTarget;

        float targetX = 128;
        float targetY;

        Vector2 scale;
        Vector2 position = new Vector2(0, 0);

        List<Object> objects;
        List<Bullet> bullets = new List<Bullet>();

        KeyboardState currentKey;
        KeyboardState previousKey;

        SpriteFont basicFont;

        public Game1()
        {
            int initScreenW = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int initScreenH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = initScreenW,
                PreferredBackBufferHeight = initScreenH,
                IsFullScreen = fullScreen,
                PreferredDepthStencilFormat = DepthFormat.Depth16
            };

            Content.RootDirectory = "Content";
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

            mainTarget = new RenderTarget2D(GraphicsDevice, screenWidth, screenHeight);
            pp = GraphicsDevice.PresentationParameters;
            SurfaceFormat format = pp.BackBufferFormat;

            screenW = mainTarget.Width;
            screenH = mainTarget.Height;
            desktopRect = new Rectangle(0, 0, pp.BackBufferWidth, pp.BackBufferHeight);
            screenRect = new Rectangle(0, 0, screenW, screenH);
            screen_center = new Vector2(screenW / 2.0f) - new Vector2(32f, 32f);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerSprite = Content.Load<Texture2D>("man");
            gunSprite = Content.Load<Texture2D>("gun");
            bulletSprite = Content.Load<Texture2D>("bullet");

            player = new Player(playerSprite, new Vector2(0, 900));
            gun = new Gun(gunSprite, new Vector2(50, 1000));

            basicFont = Content.Load<SpriteFont>("Fonts/basicfont");

            scale = new Vector2(targetX / (float)playerSprite.Width, targetX / (float)playerSprite.Width);
            targetY = playerSprite.Height * scale.Y;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            if (currentKey.IsKeyDown(Keys.Z) && previousKey.IsKeyUp(Keys.Z))
            {
                Shoot();
            }

            foreach (Bullet b in bullets)
            {
                b.Update(gameTime);
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].isRemoved)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }

            player.Update(gameTime);    //objects
            gun.Update(gameTime);

            base.Update(gameTime);
        }

        void Shoot()
        {
            Bullet bullet = new Bullet(bulletSprite, new Vector2(0, 0));

            bullets.Add(bullet);
            bullet.direction = new Vector2(gun.direction.X, gun.direction.Y);
            bullet.position = new Vector2(gun.position.X, gun.position.Y - 10);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            gun.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            foreach (var b in bullets)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.End();

            spriteBatch.Begin();

            var fontY = 10;
            var i = 0;

            spriteBatch.DrawString(basicFont, string.Format("Rotation {0}: {1}", ++i, ((Gun)gun).rotation), new Vector2(10, fontY += 20), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
