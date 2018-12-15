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
    public class MovingPlatform : Object
    {
        public bool important;
        public bool forward;
        public int score = 0;

        public MovingPlatform(Texture2D newTexture, Vector2 newPos, List<Object> collisionObjects, Vector2 scaleBase, List<MovingPlatform> movers)
            : base(newTexture, newPos, collisionObjects, scaleBase)
        {
            scaleRect = scaleBase;

            scale = new Vector2(texture.Width / 9, texture.Height / 9);

            movers.Add(this);
            collisionObjects.Add(this);
        }

        public override void Update(GameTime gameTime, List<Object> collisionObjects)
        {
            position += velocity;

            if (forward == true)
            {
                velocity.X = 1;
            }
            else
            {
                velocity.X = -1;
            }

            Collision(collisionObjects);
        }

        void Collision(List<Object> collisionObjects)
        {
            foreach (var obj in collisionObjects)
            {
                if (obj == this || obj is Bullet && important == false)
                {
                    continue;
                }

                if (this.velocity.X > 0 && IsTouchingLeft(obj) || this.velocity.X < 0 && this.IsTouchingRight(obj))
                {
                    if (forward == true)
                    {
                        forward = false;
                    }
                    else
                    {
                        forward = true;
                    }
                }

                if (obj is Bullet && important == true)
                {
                    if (IsTouchingTop(obj) || IsTouchingBottom(obj) || IsTouchingLeft(obj) || IsTouchingRight(obj))
                    {
                        score += 1;
                    }
                }
            }
        }
    }
}
