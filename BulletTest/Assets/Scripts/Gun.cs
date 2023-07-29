using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class Gun : MonoBehaviour
    {
        /// <summary>
        /// ����WaitForFixedUpdate����
        /// </summary>
        private WaitForFixedUpdate cachedWaitForFixed = new WaitForFixedUpdate();
        /// <summary>
        /// Э�̾��
        /// </summary>
        private Coroutine shootCoroutineHandle;
        //ͨ�ö���أ����ڹ����ӵ����󴴽�������GC
        private ObjectPool bulletPool;
        private bool isShooting;
        public float ShootGap = 0.1f;
        void Start()
        {
            //��ʼ������أ�����Э�̲���þ��
            bulletPool = GetComponent<ObjectPool>();
            shootCoroutineHandle = StartCoroutine(ShootCoroutine());
        }

        void Update()
        {
            //Update��������������
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
            //����ʱ�ͷ�Э��
            StopCoroutine(shootCoroutineHandle);
        }

        IEnumerator ShootCoroutine()
        {
            //��ʱ������
            float timer = 0f;
            while (true)
            {
                if (isShooting)
                {
                    timer += Time.fixedDeltaTime;
                    if (timer > ShootGap)
                    {
                        //�������
                        timer -= ShootGap;
                        //�Ӷ���ػ�ȡ�ӵ�����
                        bulletPool.GetPoolObject();
                    }
                    //���ػ���õ�WaitForFixedUpdate���������new������GC
                    yield return cachedWaitForFixed;
                }
                else
                {
                    //���ü�ʱ����Ϊ��֤�׷�������Ч������ֵΪShootGap
                    timer = ShootGap;
                    yield return null;
                }
            }
        }

    }
}
