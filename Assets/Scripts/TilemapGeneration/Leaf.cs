using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public struct Vector2
{
    public int x;
    public int y;

    public Vector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"({x};{y})";
    }
}*/


namespace BSP
{
    class Leaf
    {
        const int MIN_LEAF_SIZE = 3;

        public Vector2 position;
        public int width;
        public int height;

        public Leaf leftChild;
        public Leaf rightChild;

        public Leaf(Vector2 pos, int width, int height)
        {
            this.position = pos;
            this.width = width;
            this.height = height;
        }


        public bool Split()
        {
            if (rightChild != null && leftChild != null)
                return false;  // Уже разразали лист

            // Определяем направление разрезания
            bool splitHor = Random.Range(0, 2) == 1;
            if (width > height && (float)width / height >= 1.25f)
                splitHor = false;
            else if (height > width && (float)height / width >= 1.25f)
                splitHor = true;


            int max = (splitHor ? height : width) - MIN_LEAF_SIZE;
            if (max <= MIN_LEAF_SIZE)
                return false;
            int split = Random.Range(MIN_LEAF_SIZE, max);

            if (splitHor)
            {
                leftChild = new Leaf(position, width, split);
                rightChild = new Leaf(new Vector2(position.x, position.y + split), width, height - split);
            }
            else
            {
                leftChild = new Leaf(position, split, height);
                rightChild = new Leaf(new Vector2(position.x + split, position.y), width - split, height);
            }

            return true;
        }

        public override string ToString()
        {
            return $"Position: {position}, width: {width}, height = {height}\n";
        }
    }
}
