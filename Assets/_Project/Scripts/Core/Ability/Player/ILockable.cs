using UnityEngine;

namespace MyGame.Core
{
    public interface ILockable
    {
        public Transform Transform { get; }
        public bool CanLock { get; }
    }
}