using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class Gun : MonoBehaviour
    {
        /// <summary>
        /// 缓存WaitForFixedUpdate对象
        /// </summary>
        private WaitForFixedUpdate cachedWaitForFixed = new WaitForFixedUpdate();
        /// <summary>
        /// 协程句柄
        /// </summary>
        private Coroutine shootCoroutineHandle;
        //通用对象池，用于管理子弹对象创建，降低GC
        private ObjectPool bulletPool;
        private bool isShooting;
        public float ShootGap = 0.1f;
        void Start()
        {
            //初始化对象池，开启协程并获得句柄
            bulletPool = GetComponent<ObjectPool>();
            shootCoroutineHandle = StartCoroutine(ShootCoroutine());
        }

        void Update()
        {
            //Update仅仅用于输入检测
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
            //销毁时释放协程
            StopCoroutine(shootCoroutineHandle);
        }

        IEnumerator ShootCoroutine()
        {
            //计时器变量
            float timer = 0f;
            while (true)
            {
                if (isShooting)
                {
                    timer += Time.fixedDeltaTime;
                    if (timer > ShootGap)
                    {
                        //补偿误差
                        timer -= ShootGap;
                        //从对象池获取子弹对象
                        bulletPool.GetPoolObject();
                    }
                    //返回缓存好的WaitForFixedUpdate对象而不是new，降低GC
                    yield return cachedWaitForFixed;
                }
                else
                {
                    //重置计时器，为保证首发立即生效，被赋值为ShootGap
                    timer = ShootGap;
                    yield return null;
                }
            }
        }

    }
}
