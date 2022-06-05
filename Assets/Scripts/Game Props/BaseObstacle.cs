using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour, IObstacle
{
    public Action OnTrigger;

    public virtual void Interact()
    {
    }

    private void OnDisable() => transform.DOKill();
}