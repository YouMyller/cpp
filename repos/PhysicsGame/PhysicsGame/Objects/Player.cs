﻿using Microsoft.Xna.Framework;
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
    public class Player : Object
    {
        public Player(Texture2D newTexture, Vector2 newPos, List<Object> collisionObjects) 
            : base(newTexture, newPos, collisionObjects)
        {
            //scale = new Vector2(targetX / (float)texture.Width, targetX / (float)texture.Width);
            scale = new Vector2(texture.Width / 11, texture.Height / 9);

            accelerationX = .05f;

            collisionObjects.Add(this);
        }

        public override void Update(GameTime gameTime, List<Object> collisionObjects, Testbox testbox)  // List<Object> objects
        {

            position += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (velocity.X < 3)
                {
                    velocity.X += accelerationX * 3;
                }
                velocity.X += accelerationX;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (velocity.X >= 0)
                {
                    velocity.X -= accelerationX * 3;
                }
                velocity.X -= accelerationX;
            }
            else
            {
                if (velocity.X > 0)
                {
                    velocity.X -= accelerationX;
                }
                if (velocity.X < 0)
                {
                    velocity.X += accelerationX;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 20f;
                velocity.Y = -10f;
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

            foreach (var obj in collisionObjects)
            {
                if (obj == this)
                {
                    continue;
                }

                if (this.velocity.X > 0 && IsTouchingLeft(obj) || this.velocity.X < 0 && this.IsTouchingRight(obj))
                {
                    this.velocity.X = 0;
                }

                if (this.velocity.Y < 0 && this.IsTouchingBottom(obj))
                {
                    this.velocity.Y = 0;
                }

                if (this.IsTouchingTop(obj) && this.velocity.Y > 0)
                {
                    hasJumped = false;
                    this.velocity.Y = 0;
                }




                /*if (rect.Intersects(obj.rect) && ve)
                {
                    //this.position.X += .1f;
                    this.position.Y -= 20f;
                }*/

            }
        }
    }
}
