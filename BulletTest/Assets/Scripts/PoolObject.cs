using UnityEngine;

namespace Scripts
{
    public abstract class PoolObject : MonoBehaviour
    {
        protected ObjectPool Pool;
        public abstract PoolObject Clone(ObjectPool pool, Transform specifyTransform);
        public abstract void DeactivateAndCollect();
        public abstract void Activate(Transform specifyTransform);

        public abstract bool IsActive { get; set; }
    }
}
