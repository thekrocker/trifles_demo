using System.Collections;
using System.Collections.Generic;
using Game_Props;
using UnityEngine;

public static class SafaUtility 
{
    public static void ShuffleList<T>(this IList<T> list) where T : StackCube
    {
        for (var i = list.Count - 1; i > 1; i--)
        {
            var j = Random.Range(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }
}
