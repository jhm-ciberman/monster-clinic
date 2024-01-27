using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

[Serializable]
public class LevelPart
{
    public LevelBlock[] blocks;
}

public class LevelGenerator : MonoBehaviour
{
    public LevelPart[] parts;

    public void Start()
    {
        var random = new Random();

        var blocks = this.parts.Select(part => Pick(random, part.blocks)).ToArray();

        Vector2 start = this.transform.position;
        foreach (var block in blocks)
        {
            var newBlock = Instantiate(block, start, Quaternion.identity, this.transform);
            var width = newBlock.background.bounds.size.x;

            start.x += width;
        }
    }

    private static T Pick<T>(Random random, T[] array)
    {
        return array[random.Next(array.Length)];
    }
}
