using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private List<BoxCollider> colliderList = new List<BoxCollider>();
    private void Awake()
    {
        SetColliderList();
    }

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<BaseObstacle>().OnTrigger += Deactivate;
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<BaseObstacle>().OnTrigger -= Deactivate;
        }
    }
    

    private void SetColliderList()
    {
        foreach (Transform child in transform) colliderList.Add(child.GetComponent<BoxCollider>());
    }

    public void Deactivate()
    {
        foreach (var col in colliderList) col.enabled = false;
    }
}
