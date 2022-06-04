using System;
using DG.Tweening;
using UnityEngine;

namespace Game_Props
{
    public class StackCube : MonoBehaviour
    {
        public int index;
        public Color CurrentColor { get; set;}
        public void SetColor(Color color)
        {
            GetComponent<MeshRenderer>().material.color = color;
            CurrentColor = color;
        }

        private void Start()
        {
            index = StackSystem.Instance.CurrentCubeStacks.IndexOf(this);
        }

        public void ScaleDown()
        {
            transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}