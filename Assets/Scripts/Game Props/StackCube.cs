using System;
using DG.Tweening;
using UnityEngine;

namespace Game_Props
{
    public class StackCube : MonoBehaviour
    {
        public Color CurrentColor { get; set; }


        private void OnEnable()
        {
            transform.localScale = Vector3.one;
        }

        public void SetColor(Color color)
        {
            GetComponent<MeshRenderer>().material.color = color;
            CurrentColor = color;
        }

        public void ScaleDown(float duration)
        {
            transform.Scale(Vector3.zero, duration, () => gameObject.SetActive(false));
        }

        public Color GetColor()
        {
            return GetComponent<MeshRenderer>().material.color;
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}