using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicsGame.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsGame
{
    class Gun : Object
    {
        public Gun(Texture2D newTexture, Vector2 newPos, List<Object> collisionObjects) 
            : base(newTexture, newPos, collisionObjects)
        {
            //scale = new Vector2(targetX / (float)texture.Width + .1f, targetX / (float)texture.Width);
            //scale.Y = scale.Y / 2;
            //origin = new Vector2(texture.Width / 2, (texture.Height / 2) + 100);
            scale = new Vector2(.5f, .7f);
            //rect = new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void Update(GameTime gameTime, List<Object> collisionObjects, Testbox testbox)  //List<Object> objects
        {
            //Gun rotation
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                rotation -= MathHelper.ToRadians(rotationVelocity);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                rotation += MathHelper.ToRadians(rotationVelocity);
            }

            direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
        }
    }
}
