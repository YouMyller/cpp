using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsGame.Objects
{
    public class Bullet : Object
    {
        float timer;
        public float lifeSpan;
        float gravity;

        public Bullet(Texture2D newTexture, Vector2 newPos, List<Object> collisionObjects) 
            : base(newTexture, newPos, collisionObjects)
        {
            scale = new Vector2(targetX / (float)texture.Width, targetX / (float)texture.Width);
            //velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 20f + new Vector2(7f, 7f);
            lifeSpan = 6;
            velocity = new Vector2(14, 14);
            accelerationY = .1f;
            gravity = accelerationY;
            //rect = new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y);
            //collisionObjects.Add(this);
        }

        public override void Update(GameTime gameTime, List<Object> collisionObjects, Testbox testbox)
        {

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > lifeSpan)
            {
                isRemoved = true;
            }

            position += direction * velocity;

            if (direction.Y >= 0)
            {
                velocity.Y += gravity;
            }
            else
            {
                velocity.Y -= gravity;
            }

            direction.Y += .01f;

            /*foreach (var obj in collisionObjects)
            {
                if (obj == this)
                {
                    continue;
                }

                if (this.velocity.X > 0 && IsTouchingLeft(obj) || this.velocity.X < 0 && this.IsTouchingRight(obj))
                {
                    this.velocity.X = 0;
                }

                if (this.velocity.Y > 0 && IsTouchingTop(obj) || this.velocity.Y < 0 && this.IsTouchingBottom(obj))
                {
                    this.velocity.Y = 0;
                }
            }*/
        }
    }
}
