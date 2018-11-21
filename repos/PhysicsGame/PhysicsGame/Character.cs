using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsGame
{
    class Character
    {
        //Create a base class (Object), and have the three objects inherit from it (player, gun, bullet)
        //Object class should hold the collisions

        //Character variables
        public Character player;

        Texture2D texture;

        Vector2 position;
        Vector2 velocity;

        Vector2 scale;

        bool hasJumped;

        float targetX = 128;


        //Gun variables
        Texture2D gunTexture;

        Vector2 gunScale;
        Vector2 origin;

        float rotation;
        public float rotationVelocity = 3f;
        public float linearVelocity = 4f;

        Rectangle rect;

        //Bullet variables
        public Texture2D bulletTexture;

        Vector2 bulletOrigin;

        public float bulletVelocity;

        public Character(Texture2D newTexture, Texture2D newGunTexture, Texture2D newBulletTexture, Vector2 newPos)  //Vector2 newPos
        {
            texture = newTexture;
            gunTexture = newGunTexture;
            position = newPos;
            hasJumped = true;
            scale = new Vector2(targetX / (float)texture.Width, targetX / (float)texture.Width);
            gunScale = new Vector2(targetX / (float)gunTexture.Width, targetX / (float)gunTexture.Width);
        }

        public void Update(GameTime gameTime)
        {
            //Character movement
            position += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = 3f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -3f;
            }
            else
            {
                velocity.X = 0f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -5f;
                hasJumped = true;
            }

            if (hasJumped == true)
            {
                velocity.Y += 0.15f;
            }

            if (position.Y + scale.Y >= 890)
            {
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }

            //Gun rotation
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                rotation -= MathHelper.ToRadians(rotationVelocity);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                rotation += MathHelper.ToRadians(rotationVelocity);
            }
            var dir = new Vector2((float)Math.Cos(rotation), -(float)Math.Sin(rotationVelocity));

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position += dir * linearVelocity;
            }
        }

        public void Draw(SpriteBatch spB)
        {
            spB.Draw(texture, position, scale: scale);
        }

        public void DrawGun(SpriteBatch spB)
        {
            spB.Draw(gunTexture, position, null, Color.White, rotation, origin, scale * 1.2f, SpriteEffects.None, 0f);
        }

        public void DrawBullet(SpriteBatch spB)
        {
            spB.Draw(bulletTexture, position, null, Color.White, rotation, bulletOrigin, scale, SpriteEffects.None, 0f);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
