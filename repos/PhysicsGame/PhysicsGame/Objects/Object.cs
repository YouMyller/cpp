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
    public class Object : ICloneable
    {
        public Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 scale;      
        public Vector2 origin;
        public Vector2 direction;
        public float accelerationX;
        public float accelerationY;

        public bool hasJumped;
        public bool isRemoved = false;

        public float targetX = 128;
        public float rotation;
        public float rotationVelocity = 3f;
        public float linearVelocity = 4f;

        protected bool collideable;

        protected readonly Rectangle gameBoundaries;

        public string name;

        public Vector2 scaleRect;

        public Object(Texture2D newTexture, Vector2 newPos, List<Object> objects, Vector2 scaleBase)
        {
            texture = newTexture;
            position = newPos;
            hasJumped = true;
            scaleBase = new Vector2(140, 110);
            scaleRect = scaleBase;
        }

        public virtual void Update(GameTime gameTime, List<Object> collisionObjects, Testbox testbox)
        {
            position += velocity;
        }

        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)scaleRect.X, (int)scaleRect.Y);
            }
        }

        public virtual void Draw(SpriteBatch spB)
        {
            spB.Draw(texture, position, null, Color.White, rotation, origin, scale * 1.2f, SpriteEffects.None, 0f);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        //Collision
        protected bool IsTouchingLeft(Object obj)
        {
            return this.rect.Right + this.velocity.X > obj.rect.Left &&
                this.rect.Left < obj.rect.Left &&
                this.rect.Bottom > obj.rect.Top &&
                this.rect.Top < obj.rect.Bottom;
        }

        protected bool IsTouchingRight(Object obj)
        {
            return this.rect.Left + this.velocity.X < obj.rect.Right &&
                this.rect.Right > obj.rect.Right &&
                this.rect.Bottom > obj.rect.Top &&
                this.rect.Top < obj.rect.Bottom;
        }

        protected bool IsTouchingTop(Object obj)
        {
            return this.rect.Bottom + this.velocity.Y > obj.rect.Top &&
                this.rect.Top < obj.rect.Top &&
                this.rect.Right > obj.rect.Left &&
                this.rect.Left < obj.rect.Right;
        }

        protected bool IsTouchingBottom(Object obj)
        {
            return this.rect.Top + this.velocity.Y < obj.rect.Bottom &&
                this.rect.Bottom > obj.rect.Bottom &&
                this.rect.Right > obj.rect.Left &&
                this.rect.Left < obj.rect.Right;
        }
    }
}
