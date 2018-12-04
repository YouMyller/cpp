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
        int changed = 0;
        Vector2 currentDir;

        public Bullet(Texture2D newTexture, Vector2 newPos, List<Object> collisionObjects, Vector2 scaleBase) 
            : base(newTexture, newPos, collisionObjects, scaleBase)
        {
            scale = new Vector2(texture.Width / 20, texture.Height / 20);   
            lifeSpan = 6;
            velocity = new Vector2(14, 14);
            accelerationY = .1f;
            gravity = accelerationY;
            collisionObjects.Add(this);
            scaleRect = scale;
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

            foreach (var obj in collisionObjects)
            {
                if (obj == this || obj is Bullet || obj is Player)
                {
                    continue;
                }

                if (this.velocity.X > 0 && IsTouchingLeft(obj))
                {
                    this.direction.X = -this.direction.X;
                    this.velocity.X = velocity.X / 2;
                }
                else if (this.IsTouchingTop(obj) && this.velocity.Y > 0 && this.velocity.Y < 0)
                {
                    this.direction.Y = -this.direction.Y;
                    this.velocity.X = velocity.X / 2;
                }
                else
                {
                    if (this.rect.Intersects(obj.rect))
                    {
                        if (changed == 0)
                        {
                            this.direction.X = -this.direction.X;
                            this.velocity.X = velocity.X / 2;
                            currentDir = direction;
                        }

                        if (this.rect.Intersects(obj.rect) && changed == 0)
                        {
                            this.direction.Y = -this.direction.Y;
                            changed = 1;
                        }

                        else if (this.rect.Intersects(obj.rect) && changed == 1 && currentDir.X < direction.X)  // && position.Y > obj.rect.Top
                        {
                            this.direction.X = -this.direction.X;
                            changed = 2;
                        }
                    }
                }
            }
        }
    }
}
