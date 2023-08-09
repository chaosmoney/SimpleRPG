using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace GameMain
{
    public class MonsterController : MonoBehaviour
    {
        [SerializeField]
        private float radius = 1f;
        private float hp = 40;
        public float Radius
        {
            get { return this.radius; }

        }
        private Animator anim;

        public Action onHit;
        public Action onDie;
        private enum eState
        {
            Idle, Hit, Die
        }
        eState state;
        // Start is called before the first frame update
        void Start()
        {
            this.anim = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HitDamage(int atk)
        {
            this.hp -= atk;

            if (this.hp <= 0)
            {
                this.StartCoroutine(this.WaitForCompleteDieAnimation());
            }
            else
            {
                Debug.Log("피해를 입었습니다");
                this.onHit();
                this.anim.SetInteger("State", (int)eState.Hit);
            }
        }

        private IEnumerator WaitForCompleteDieAnimation()
        {
            this.anim.SetInteger("State", (int)eState.Die);
            yield return new WaitForSeconds(1.66f);
            this.onDie();
            Destroy(this.gameObject);

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.radius);
        }
    }
}