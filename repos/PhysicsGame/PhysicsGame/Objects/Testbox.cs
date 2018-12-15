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
        public Testbox(Texture2D newTexture, Vector2 newPos, List<Object> collisionObjects, Vector2 scaleBase, List<Testbox> boxes) 
            : base(newTexture, newPos, collisionObjects, scaleBase)
        {
            scaleRect = scaleBase;

            scale = new Vector2(texture.Width / 9, texture.Height / 9);

            boxes.Add(this);
            collisionObjects.Add(this);
        }
    }
}
