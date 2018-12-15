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
        public int changed = 0;
        Vector2 currentDir;
        public string kakka = "olo";

        public Bullet(Texture2D newTexture, Vector2 newPos, List<Object> collisionObjects, Vector2 scaleBase) 
            : base(newTexture, newPos, collisionObjects, scaleBase)
        {
            scale = new Vector2(texture.Width / 20, texture.Height / 20);   
            lifeSpan = 6;
            velocity = new Vector2(14, 14);
            accelerationY = .1f;
            gravity = accelerationY;
            collisionObjects.Add(this);
            scaleBase = new Vector2(140, 110);
            scaleRect = scale;
        }

        public override void Update(GameTime gameTime, List<Object> collisionObjects)
        {

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > lifeSpan)
            {
                isRemoved = true;
            }

            position += direction * velocity;

            position.Y += gravity;

            direction.Y += .01f;

            foreach (var obj in collisionObjects)
            {
                if (obj == this || obj is Bullet || obj is Player)
                {
                    continue;
                }

                if (!this.rect.Intersects(obj.rect))
                {
                    changed = 0;
                }

                if (this.velocity.X > 0 && IsTouchingLeft(obj))
                {
                    this.direction.X = -this.direction.X;
                    this.velocity.X = velocity.X / 2;
                }
                else if (this.rect.Intersects(obj.rect) && this.velocity.Y > 0 && this.velocity.Y < 0) //this.IsTouchingTop(obj)
                {
                    this.direction.Y = -this.direction.Y;
                    this.velocity.Y /= 1.2f;
                    this.velocity.X = velocity.X / 2;
                }
                else    //right && bottom
                {
                    //Changed resettautuu nollaksi
                    if (this.rect.Intersects(obj.rect) && !IsTouchingLeft(obj) && !IsTouchingTop(obj))
                    {
                        if (changed == 0)
                        {
                            if (velocity.Y > velocity.X)
                            {
                                this.direction.X = -this.direction.X;
                                this.velocity.X = velocity.X / 2;
                                currentDir = direction;
                            }
                            else if (velocity.Y < velocity.X)
                            {
                                this.direction.X = -this.direction.X;
                                this.velocity.X = velocity.X / 2;
                                currentDir = direction;
                            }
                        }

                        if (this.rect.Intersects(obj.rect) && changed == 0)
                        {
                            this.direction.Y = -this.direction.Y;
                            this.velocity.Y /= 1.2f;
                            changed = 1;
                        }

                        else if (this.rect.Intersects(obj.rect) && changed == 1 && currentDir.X < direction.X)
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
