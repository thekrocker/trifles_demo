using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable
{
    public void Stack(Transform position);
    public float GetHeight();
}
