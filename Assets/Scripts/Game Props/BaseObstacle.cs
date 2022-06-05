using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour, IObstacle
{
    public Action OnTrigger;

    public virtual void Interact()
    {
    }
}