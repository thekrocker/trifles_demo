using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraStackHandler : MonoBehaviour
{
    private CinemachineVirtualCamera _cm;
    private CinemachineFramingTransposer _transposer;
    private void Awake()
    {
        _cm = GetComponent<CinemachineVirtualCamera>();
        _transposer = _cm.GetCinemachineComponent<CinemachineFramingTransposer>();

    }

    private float _initialCamDistance;
    private void Start()
    {
        _initialCamDistance = _cm.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
        
        EventManager.OnStack += SetCamera;
    }
    
    private void OnDisable()
    {
        EventManager.OnStack -= SetCamera;
    }

    private float offset = 0.5f;
    private void SetCamera(int stackSize)
    {
        if (stackSize % 3 == 0)
        {
            _transposer.m_CameraDistance = stackSize + (_initialCamDistance + offset);
        }
    }
}
