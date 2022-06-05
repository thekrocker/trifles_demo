using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game_Props;
using UnityEngine;

public static class SafaUtility 
{
    
    public static float DistanceBetweenLanes = 3f;
    
    
    public static void Scale(this Transform target, Vector3 targetValue, float duration, Action OnCompleteAction = null)
    {
        target.DOScale(targetValue, duration).OnComplete(() =>
        {
            OnCompleteAction?.Invoke();
        });
    }
}
