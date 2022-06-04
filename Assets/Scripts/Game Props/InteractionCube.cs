using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCube : MonoBehaviour, IStackable
{
    [SerializeField] private ColorPool colorPool;
    [SerializeField] private MeshRenderer cubeRenderer;
    
    private BoxCollider _col;
    public Color CurrentColor { get; set; }

    private void Awake()
    {
        SetReferences();
        SetColor();
    }

    private void SetReferences()
    {
        _col = GetComponent<BoxCollider>();
    }

    private void SetColor()
    {
        cubeRenderer.material.color = colorPool.GetRandomColorFromPool();
        CurrentColor = cubeRenderer.material.color;
    }

    public void Stack(Transform position)
    {
        StackSystem.Instance.AddStack(this, position, CurrentColor);
        gameObject.SetActive(false);
    }

    public float GetHeight() => _col.bounds.size.y;
}
