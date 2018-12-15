using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testgame
{
    public class Game1 : Game
    {
        const int screenWidth = 1024, screenHeight = 768;
        const bool fullScreen = false;

        GraphicsDeviceManager graphics;
        PresentationParameters pp;
        SpriteBatch spriteBatch;

        static public int screenW, screenH;
        static public Vector2 screen_center;

        Rectangle screenRect, desktopRect;

        Character player;

        static public Vector2 bg_pos;

        RenderTarget2D mainTarget;

        const int max_sheet_parts = 300;
        Sheet[] sheet;
        SheetManager sheetManager;

        Texture2D playerGuy;
        Texture2D far_bg, mid_bg;
        Texture2D tiles_images;

        Texture2D playerSprite;

        Input inp;

        float targetX = 128;
        float targetY;

        Vector2 scale;
        Vector2 position = new Vector2(0, 0);
        Vector2 velocity = new Vector2(100, 100);

        public Game1()
        {
            int initScreenW = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 10; //Setting -10 is temporary for debugging purposes.
            int initScreenH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 10; //Remember to remove them later!!

            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = initScreenW,
                PreferredBackBufferHeight = initScreenH,
                IsFullScreen = fullScreen, PreferredDepthStencilFormat = DepthFormat.Depth16
            };

            Window.IsBorderless = true;

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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainTarget = new RenderTarget2D(GraphicsDevice, screenWidth, screenHeight);
            pp = GraphicsDevice.PresentationParameters;
            SurfaceFormat format = pp.BackBufferFormat;

            screenW = mainTarget.Width;
            screenH = mainTarget.Height;
            desktopRect = new Rectangle(0, 0, pp.BackBufferWidth, pp.BackBufferHeight);
            screenRect = new Rectangle(0, 0, screenW, screenH);
            screen_center = new Vector2(screenW / 2.0f) - new Vector2(32f, 32f);

            inp = new Input();

            base.Initialize();

            sheet = new Sheet[max_sheet_parts];
            sheetManager = new SheetManager();
        }


        protected override void LoadContent()
        {
            far_bg = Content.Load<Texture2D>("sky_bg");      
            mid_bg = Content.Load<Texture2D>("clouds");
            //tiles_images = Content.Load<Texture2D>(""); //Missing sprite

            //playerSprite = Content.Load<Texture2D>("man");
            //player = new Character(playerSprite, new Vector2(50, 50));     //new Vector2(50, 50)
            playerGuy = Content.Load<Texture2D>("man");
            scale = new Vector2(targetX / (float)playerGuy.Width, targetX / (float)playerGuy.Width);
            targetY = playerGuy.Height * scale.Y;
        }


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
            inp.Update();

            if(inp.Keypress(Keys.Escape))
            {
                Exit();
            }

            //player.Update(gameTime);

            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }*/

            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds; 

            /*if (position.X < 0 && velocity.X < 0)
            {
                //velocity.X *= -1;
            }
            else if (position.Y <= 0 && velocity.Y <= 0)
            {
                //velocity.Y *= -1;
            }
            else if (position.X >= graphics.GraphicsDevice.Viewport.Width - targetX && velocity.X > 0)
            {
                //velocity.X *= -1;
            }
            else if (position.X >= graphics.GraphicsDevice.Viewport.Height - targetY && velocity.Y > 0)
            {
                //velocity.Y *= -1;
            }*/

            if (inp.Keypress(Keys.Right))
            {
                position.X += 100;
                bg_pos.X -= 1;
            }
            if (inp.Keypress(Keys.Left))
            {
                position.X -= 100;
                bg_pos.X += 1;
            }
            if (inp.Keypress(Keys.Up))
            {
                position.Y -= 100;
                bg_pos.Y += 1;
            }
            if (inp.Keypress(Keys.Down))
            {
                position.Y += 100;
                bg_pos.Y -= 1;
            }

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetRenderTarget(mainTarget);

            //Draw Far Background
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearWrap);
            spriteBatch.Draw(far_bg, screenRect, new Rectangle((int)(-bg_pos.X * 0.5f), 0, far_bg.Width, far_bg.Height), Color.White);
            spriteBatch.End();

            //Draw Mid Background
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp);
            spriteBatch.Draw(mid_bg, screenRect, new Rectangle((int)(-bg_pos.X), (int)(-bg_pos.Y), far_bg.Width, far_bg.Height), Color.White);
            spriteBatch.End();

            //Draw main target
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearClamp, DepthStencilState.None,
                RasterizerState.CullNone);
            spriteBatch.Draw(mainTarget, desktopRect, Color.White);
            spriteBatch.End();

            //Draw player character
            spriteBatch.Begin();
            //spriteBatch.Draw(playerSprite, position: position, scale: scale);
            spriteBatch.Draw(playerGuy, position: position, scale: scale);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
