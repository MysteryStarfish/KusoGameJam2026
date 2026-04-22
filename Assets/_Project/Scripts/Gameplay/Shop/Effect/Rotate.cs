using System;
using DG.Tweening;
using UnityEngine;

namespace MyGame.Gameplay
{
    public class Rotate : MonoBehaviour
    {
        void Start()
        {
            transform.DORotate(new Vector3(0, 0, 360f), 3.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
    }
}
