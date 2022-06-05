using System;
using UnityEngine;

namespace Player
{
    public class TrailHandler : MonoBehaviour
    {
        private TrailRenderer _tRenderer;

        private void Awake()
        {
            _tRenderer = GetComponent<TrailRenderer>();

            SetEmitting(false);
        }
        
        private void OnEnable() => EventManager.OnStackChange += SetTrailColor;

        private void OnDisable() => EventManager.OnStackChange -= SetTrailColor;

        private void SetEmitting(bool status) => _tRenderer.emitting = status;


        private void Update()
        {
            SetTrailColor();
        }

        private void SetTrailColor()
        {
            if (StackSystem.Instance.CurrentCubeStacks.Count <= 0)
            {
                SetEmitting(false);
                return;
            }

            SetEmitting(true);
            _tRenderer.startColor = StackSystem.Instance.CurrentCubeStacks[^1].GetColor();
            _tRenderer.endColor = StackSystem.Instance.CurrentCubeStacks[^1].GetColor();
        }
    }
}