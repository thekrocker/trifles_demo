using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractionCube : MonoBehaviour, IStackable
{
    private readonly float[] _lanes = { -SafaUtility.DistanceBetweenLanes, 0f, SafaUtility.DistanceBetweenLanes};
    
    [SerializeField] private ColorPool colorPool;
    [SerializeField] private MeshRenderer cubeRenderer;
    
    private BoxCollider _col;
    public Color CurrentColor { get; set; }

    private void Awake()
    {
        SetReferences();
        SetColor();
        SetXAxis();
    }

    private void SetXAxis()
    {
        transform.position = new Vector3(_lanes[Random.Range(0, _lanes.Length)], transform.position.y, transform.position.z);
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
