using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class Gun : MonoBehaviour
    {
        private WaitForFixedUpdate cachedWaitForFixed = new WaitForFixedUpdate();
        private Coroutine shootCoroutine;
        private ObjectPool bulletPool;
        private bool isShooting;
        public float ShootGap = 0.1f;
        void Start()
        {
            bulletPool = GetComponent<ObjectPool>();
            shootCoroutine = StartCoroutine(ShootCoroutine());
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isShooting = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isShooting = false;
            }
        }

        void OnDestroy()
        {
            StopCoroutine(shootCoroutine);
        }

        IEnumerator ShootCoroutine()
        {
            float timer = 0f;
            while (true)
            {
                if (isShooting)
                {
                    timer += Time.fixedDeltaTime;
                    if (timer > ShootGap)
                    {
                        timer -= ShootGap;
                        bulletPool.GetPoolObject();
                    }
                    yield return cachedWaitForFixed;
                }
                else
                {
                    timer = ShootGap;
                    yield return null;
                }
            }
        }

    }
}
