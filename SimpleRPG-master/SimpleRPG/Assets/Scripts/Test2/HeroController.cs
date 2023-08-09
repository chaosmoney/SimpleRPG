using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test2
{
    public class HeroController : MonoBehaviour
    {
        public enum eState
        {
            Idle, Attack,Attack2
        }
        [SerializeField]
        private eState state;
        private float radius = 1f;
        public float impactTime = 0.4f;
        [SerializeField]
        private MonsterController monsterController;
        private Coroutine attackRoutine;
        private int atk = 10;
        public float Radius
        {
            get { return this.radius; }

        }

        private Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            this.anim = this.GetComponent<Animator>();
        }

        // Update is called once per frame 
        void Update()
        {
        }

        public void Attack(MonsterController target)
        {
            this.transform.LookAt(target.transform.position);
            this.PlayAnimation(eState.Attack);
        }

        private void PlayAnimation(eState state)
        {
            if(this.state != state)
            {
                Debug.LogFormat("{0} => {1}", this.state, state);
                this.state = state;
                this.anim.SetInteger("State", (int)state);

                switch (state)
                {
                    case eState.Attack:
                        if (this.attackRoutine != null)
                        {
                            this.StopCoroutine(this.attackRoutine);
                        }
                        this.attackRoutine = this.StartCoroutine(WaitForCompleteAttackAnimation());
                        // this.WaitForCompleteAnimation(0.833f);
                        break;
                }
            }
            else
            {
                if(this.state  == eState.Attack)
                {
                    this.state = state;
                    this.anim.SetInteger("State", (int)eState.Attack2) ;
                }
            }
        }

        private IEnumerator WaitForCompleteAttackAnimation()
        {
            yield return null;

            Debug.Log("공격 애니메이션이 끝날때까지 기다림");
            AnimatorStateInfo animStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
            bool isAttackState = animStateInfo.IsName("Attack01");
            Debug.LogFormat("isAttackState: {0}", isAttackState);
            if (isAttackState)
            {
                Debug.LogFormat("animStateInfo.length: {0}", animStateInfo.length);
            }
            else
            {
                Debug.LogFormat("Attack State가 아닙니다");
            }

            yield return new WaitForSeconds(this.impactTime);

            monsterController.HitDamage(this.atk);

            yield return new WaitForSeconds(animStateInfo.length - this.impactTime);
            this.PlayAnimation(eState.Idle);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.radius);
        }
    }
}