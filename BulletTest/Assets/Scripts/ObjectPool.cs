using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ObjectPool: MonoBehaviour
    {
        public PoolObject PoolObjectModel;
        private readonly Stack<PoolObject> poolStack = new Stack<PoolObject>();

        public PoolObject GetPoolObject()
        {
            if (poolStack.Count > 0)
            {
                var poolObject = poolStack.Pop();
                poolObject.Activate(transform);
                return poolObject;
            }

            var newPoolObject = PoolObjectModel.Clone(this, transform);
            return newPoolObject;
        }

        public void CollectPoolObject(PoolObject poolObject)
        {
            poolStack.Push(poolObject);
        }
    }
}