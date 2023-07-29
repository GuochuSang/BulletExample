using UnityEngine;

namespace Scripts
{
    public class Bullet : PoolObject
    {
        public Vector2 Speed;

        public float LifeTime;

        private float lifeTimer = 0;
        public override bool IsActive { get; set; }

        private bool _isMove;
        private bool isMove
        {
            get => _isMove;
            set
            {
                if (rig2D != null)
                {
                    if (_isMove != value)
                    {
                        rig2D.velocity = value ? Speed : Vector2.zero;
                    }
                }
                _isMove = value;
            }
        }

        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rig2D;

        private void Update()
        {
            if (IsActive)
            {
                lifeTimer += Time.deltaTime;
                if (lifeTimer > LifeTime)
                {
                    DeactivateAndCollect();
                }
            }
        }
        /// <summary>
        /// 克隆子弹
        /// </summary>
        /// <param name="pool">对象池引用</param>
        /// <param name="specifyTransform">初始transform</param>
        /// <returns></returns>
        public override PoolObject Clone(ObjectPool pool,Transform specifyTransform)
        {
            var bullet =  GameObject.Instantiate(gameObject, specifyTransform.position, specifyTransform.rotation,
                specifyTransform.parent).GetComponent<Bullet>();
            bullet.Pool = pool;
            bullet.spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            bullet.rig2D = bullet.GetComponent<Rigidbody2D>();
            bullet.Activate(specifyTransform);
            return bullet;
        }
        /// <summary>
        /// 回收子弹
        /// </summary>
        public override void DeactivateAndCollect()
        {
            IsActive = false;
            enabled = false;
            spriteRenderer.enabled = false;
            isMove = false;
            Pool.CollectPoolObject(this);
        }
        /// <summary>
        /// 激活子弹
        /// </summary>
        /// <param name="specifyTransform"></param>
        public override void Activate(Transform specifyTransform)
        {
            lifeTimer = 0;
            IsActive = true;
            transform.position = specifyTransform.position;
            transform.rotation = specifyTransform.rotation;
            transform.parent = specifyTransform.parent;
            enabled = true;
            spriteRenderer.enabled = true;
            isMove = true;
        }

    }
}
