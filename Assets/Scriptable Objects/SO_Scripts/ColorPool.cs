using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Colors/Color Pool")]
public class ColorPool : ScriptableObject
{
    public Color[] colors;

    public Color GetRandomColorFromPool()
    {
        return colors[Random.Range(0, colors.Length)];
    }
}
