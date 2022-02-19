using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSP
{
    class Program
    {
        public static List<Leaf> GetLeaves(Vector2 startPos, int width, int height)
        {
            List<Leaf> leaves = new List<Leaf>();
            List<Leaf> toAdd = new List<Leaf>();
            List<Leaf> toRemove = new List<Leaf>();

            Leaf leaf = new Leaf(startPos, width, height);
            leaves.Add(leaf);

            bool didIt = true;
            while (didIt)
            {
                didIt = false;
                toAdd.Clear();
                toRemove.Clear();
                foreach (var item in leaves)
                {
                    if (item.leftChild == null  && item.rightChild == null)
                    {
                        if (item.Split())
                        {
                            toAdd.Add(item.rightChild);
                            toAdd.Add(item.leftChild);
                            toRemove.Add(item);
                            didIt = true;
                        }
                    }
                }

                foreach (var item in toAdd)
                    leaves.Add(item);

                foreach (var item in toRemove)
                    leaves.Remove(item);
            }


            return leaves;
        }
    }
}
