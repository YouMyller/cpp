using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsGame
{
    public class Testbox : Object
    {
        public Testbox(Texture2D newTexture, Vector2 newPos, List<Object> collisionObjects, Testbox box) 
            : base(newTexture, newPos, collisionObjects)
        {
            //scale = new Vector2(targetX / (float)texture.Width, targetX / (float)texture.Width);
            scale = new Vector2(texture.Width / 9, texture.Height / 9);
            //rect = new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y);
            //rect = new Rectangle((int)(newPos.X), (int)(newPos.Y), texture.Height, texture.Width); //texture.Height, texture.Width

            collisionObjects.Add(this);
            box = this;
        }

        public override void Update (GameTime gameTime, List<Object> collisionObjects, Testbox testbox)
        {

        }

    }
}
