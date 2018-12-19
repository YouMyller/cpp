using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicsGame.Objects;
using System.Collections.Generic;

namespace PhysicsGame
{
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
        Texture2D boxSprite;
        Texture2D goalSprite;

        Player player;
        Gun gun;

        Testbox testBox;       
        Testbox testBox2;
        Testbox testBox3;

        Testbox wall;       
        Testbox wall2;
        Testbox floor;

        MovingPlatform movePlat;
        MovingPlatform movePlat2;
        MovingPlatform goal;

        RenderTarget2D mainTarget;

        float targetX = 128;
        float targetY;

        Vector2 screenScale;

        List<Object> objects;
        List<Bullet> bullets = new List<Bullet>();
        List<Testbox> boxes = new List<Testbox>();
        List<MovingPlatform> movers = new List<MovingPlatform>();

        KeyboardState currentKey;
        KeyboardState previousKey;

        SpriteFont basicFont;

        Quadtree quad;
        List<Object> returnObjects;

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

        protected override void Initialize()
        {
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
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerSprite = Content.Load<Texture2D>("newman");
            gunSprite = Content.Load<Texture2D>("gun");
            bulletSprite = Content.Load<Texture2D>("newbullet");
            boxSprite = Content.Load<Texture2D>("newbox");
            goalSprite = Content.Load<Texture2D>("heart2");

            objects = new List<Object>();

            player = new Player(playerSprite, new Vector2(0, 900), objects, new Vector2(0, 0));

            gun = new Gun(boxSprite, new Vector2(player.position.X + player.scale.X / 2, player.position.Y - player.scale.Y), objects, new Vector2(0, 0));

            testBox = new Testbox(boxSprite, new Vector2(1000, 800), objects, new Vector2(280, 110), boxes);
            testBox.scale.X *= 2;
            testBox2 = new Testbox(boxSprite, new Vector2(500, 600), objects, new Vector2(280, 110), boxes);
            testBox2.scale.X *= 2;
            testBox3 = new Testbox(boxSprite, new Vector2(0, 700), objects, new Vector2(280, 110), boxes);
            testBox3.scale.X *= 2;
            wall = new Testbox(boxSprite, new Vector2(1200, 200), objects, new Vector2(140, 880), boxes);
            wall.scale.Y *= 8;
            wall2 = new Testbox(boxSprite, new Vector2(1900, 200), objects, new Vector2(140, 880), boxes);
            wall2.scale.Y *= 8;
            floor = new Testbox(boxSprite, new Vector2(0, 1010), objects, new Vector2(1120, 110), boxes);
            floor.scale.X *= 16;

            movePlat = new MovingPlatform(boxSprite, new Vector2(1500, 200), objects, new Vector2(140, 110), movers);
            movePlat2 = new MovingPlatform(boxSprite, new Vector2(1400, 500), objects, new Vector2(140, 110), movers);

            goal = new MovingPlatform(goalSprite, new Vector2(1600, 800), objects, new Vector2(140, 110), movers);
            goal.important = true;

            quad = new Quadtree(0, new Rectangle(0, 0, screenW, screenH));

            basicFont = Content.Load<SpriteFont>("Fonts/basicfont");

            screenScale = new Vector2(targetX / (float)playerSprite.Width, targetX / (float)playerSprite.Width);
            targetY = playerSprite.Height * screenScale.Y;
        }

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
                b.Update(gameTime, objects);
            }

            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].isRemoved)
                {
                    objects.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].isRemoved)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }

            player.Update(gameTime, objects);
            gun.Update(gameTime, objects);
            testBox.Update(gameTime, objects);
            movePlat.Update(gameTime, objects);
            movePlat2.Update(gameTime, objects);
            goal.Update(gameTime, objects);

            gun.position = new Vector2 (player.position.X + 75, player.position.Y - player.scale.Y);

            //Quadtree stuff
            returnObjects = new List<Object>();

            quad.Clear();
            for (int i = 0; i < objects.Count; i++)
            {
                quad.Insert(objects[i]);
            }

            for (int i = 0; i < objects.Count; i++)
            {
                returnObjects.Clear();
                quad.Retrieve(returnObjects, objects[i]);
            }

            for (int x = 0; x < returnObjects.Count; x++)
            {
                returnObjects[x].Collision(returnObjects);
            }

            base.Update(gameTime);
        }

        void Shoot()
        {
            Bullet bullet = new Bullet(bulletSprite, gun.position, objects, new Vector2(0,0));

            bullets.Add(bullet);
            bullet.direction = new Vector2(gun.direction.X, gun.direction.Y);
        }

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
            foreach (var box in boxes)
            {
                box.Draw(spriteBatch);
            }
            spriteBatch.End();

            spriteBatch.Begin();
            foreach (var mover in movers)
            {
                mover.Draw(spriteBatch);
            }
            spriteBatch.End();

            spriteBatch.Begin();
            var fontY = 10;
            spriteBatch.DrawString(basicFont, string.Format("Points: {0}", ((MovingPlatform)goal).score), new Vector2(10, fontY += 20), Color.White);
            spriteBatch.DrawString(basicFont, string.Format("Move: Arrow Keys", ((MovingPlatform)goal).score), new Vector2(10, fontY += 40), Color.White);
            spriteBatch.DrawString(basicFont, string.Format("Aim: A/D", ((MovingPlatform)goal).score), new Vector2(10, fontY += 20), Color.White);
            spriteBatch.DrawString(basicFont, string.Format("Jump: Space", ((MovingPlatform)goal).score), new Vector2(10, fontY += 20), Color.White);
            spriteBatch.DrawString(basicFont, string.Format("Shoot: Z", ((MovingPlatform)goal).score), new Vector2(10, fontY += 20), Color.White);
            spriteBatch.DrawString(basicFont, string.Format("Aim for the heart!", ((MovingPlatform)goal).score), new Vector2(10, fontY += 20), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
