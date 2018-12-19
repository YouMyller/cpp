using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsGame
{
    public class Quadtree
    {
        const int maxObjects = 10;
        const int maxLevels = 5;

        private int level;
        private List<Object> objects;     
        private Rectangle bounds;
        private Quadtree[] nodes;


        public Quadtree(int pLevel, Rectangle pBounds)
        {
            level = pLevel;
            objects = new List<Object>();
            bounds = pBounds;
            nodes = new Quadtree[4];
        }

        public void Clear()
        {
            objects.Clear();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            int subWidth = (int)(bounds.Width / 2);
            int subHeight = (int)(bounds.Height / 2);
            int x = (int)bounds.X;
            int y = (int)bounds.Y;

            nodes[0] = new Quadtree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new Quadtree(level + 1, new Rectangle(x, y, subWidth, subHeight));
            nodes[2] = new Quadtree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new Quadtree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        private int GetIndex(Object pRect)
        {
            int index = -1;
            double vertMidpoint = bounds.X + (bounds.Width / 2);
            double horMidpoint = bounds.Y + (bounds.Height / 2);

            bool topQuad = (pRect.rect.Y < horMidpoint && pRect.rect.Y + pRect.rect.Height < horMidpoint);
            bool bottomQuad = (pRect.rect.Y > horMidpoint);

            if (pRect.rect.X < vertMidpoint && pRect.rect.X + pRect.rect.Width < vertMidpoint)
            {
                if (topQuad)
                {
                    index = 1;
                }
                else if (bottomQuad)
                {
                    index = 2;
                }
            }
            else if (pRect.rect.X > vertMidpoint)
            {
                if (topQuad)
                {
                    index = 0;
                }
                else if (bottomQuad)
                {
                    index = 3;
                }
            }

            return index;
        }

        public void Insert(Object pRect) 
        {
            if(nodes[0] != null)
            {
                int index = GetIndex(pRect);

                if (index != -1)
                {
                    nodes[index].Insert(pRect);

                    return;
                }
            }

            objects.Add(pRect);

            if (objects.Count > maxObjects && level > maxLevels)
            {
                if (nodes[0] == null)
                {
                    Split();
                }

                int i = 0;

                while(i < objects.Count)
                {
                    int index = GetIndex(objects[i]);
                    if (index != -1)
                    {
                        nodes[index].Insert(objects[i]);        //Something might break here
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public List<Object> Retrieve(List<Object> returnObjects, Object pRect)
        {
            int index = GetIndex(pRect);

            if (index != 1 && nodes[0] != null)
            {
                nodes[index].Retrieve(returnObjects, pRect);
            }

            for (int i = 0; i < objects.Count; i++)
            {
                returnObjects.Add(objects[i]);
            }

            return returnObjects;
        }

    }
}
