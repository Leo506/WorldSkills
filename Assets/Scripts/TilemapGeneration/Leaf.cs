using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf
{
    const int MIN_LEAF_SIZE = 2;

    public int x, y, width, height;

    public Leaf leftChild;
    public Leaf rightChild;

    public Leaf(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;

        Debug.Log($"New leaf with pos: {this.x} {this.y} and width {this.width} and height {this.height}");
    }


    public bool Split()
    {
        if (rightChild != null || leftChild != null)
            return false;

        bool splitHorizontal = Random.Range(0, 2) == 1;
        if (width > height && width / height >= 1.25f)
            splitHorizontal = true;
        else if (height > width && height / width >= 1.25f)
            splitHorizontal = false;

        int max = (splitHorizontal ? height : width) - MIN_LEAF_SIZE;
        if (max <= MIN_LEAF_SIZE)
            return false;

        int splitPoint = Random.Range(MIN_LEAF_SIZE, max);

        if (splitHorizontal)
        {
            leftChild = new Leaf(x, y, width, splitPoint);
            rightChild = new Leaf(x, y + splitPoint, width, height - splitPoint);
        }
        else
        {
            leftChild = new Leaf(x, y, splitPoint, height);
            rightChild = new Leaf(x + splitPoint, y, width - splitPoint, height);
        }

        return true;
    }

}
