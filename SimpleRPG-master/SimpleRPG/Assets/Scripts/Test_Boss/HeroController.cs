using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_Boss
{
    public class HeroController : MonoBehaviour
    {
        public enum eState
        {
            Idle, Attack, Attack2
        }
        private Vector3 targetPosition;
        private Bull target;
        public System.Action<Bull> onMoveComplete;
        public System.Action onHit;
        private Coroutine moveRoutine;
        [SerializeField]
        private eState state;
        private float radius = 1f;
        public float impactTime = 0.4f;
        public bool attacked;
        private Coroutine attackRoutine;
        private int hp = 40;
        
        


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

        public void Move(Bull target)
        {
            Debug.Log("����Ÿ�� ����");
            this.target = target;
            this.targetPosition = this.target.gameObject.transform.position;
            if (this.moveRoutine != null)
            {
                //�̹� �ڷ�ƾ�� �������̴� -> ���� 
                this.StopCoroutine(this.moveRoutine);
            }
            this.moveRoutine = this.StartCoroutine(this.CoMove());
        }

        public void Move(Vector3 targetPosition)
        {
            //Ÿ���� ���� 
            this.target = null;

            //�̵��� ��ǥ������ ���� 
            this.targetPosition = targetPosition;
            Debug.Log("Move");


            if (this.moveRoutine != null)
            {
                //�̹� �ڷ�ƾ�� �������̴� -> ���� 
                this.StopCoroutine(this.moveRoutine);
            }
            this.moveRoutine = this.StartCoroutine(this.CoMove());
        }

        private IEnumerator CoMove()
        {
            while (true)
            {
                //������ �ٶ� 
                this.transform.LookAt(this.targetPosition);
                //�̹� �ٶ�����ϱ� �������� �̵� (relateTo : Self/������ǥ)
                this.transform.Translate(Vector3.forward * 2f * Time.deltaTime);
                //��ǥ������ ������ �Ÿ��� �� 
                float distance = Vector3.Distance(this.transform.position, this.targetPosition);
                //Debug.Log(distance);
                //0�� �ɼ� ���� ��������� �����Ѱ����� �Ǵ� ���� 

                //Ÿ���� ������� 
                if (this.target != null)
                {
                    if (distance <= (1f + 1f))
                    {
                        break;
                    }
                }
                else
                {
                    if (distance <= 0.1f)
                    {
                        break;
                    }
                }


                yield return null;  //���� ������ ���� 
            }

            Debug.Log("<color=yellow>����!</color>");
            this.anim.SetInteger("State", 0);
            //�븮�ڸ� ȣ��
            Debug.Log(this.target);
            this.onMoveComplete(this.target);
        }

        public void Attack(Bull target)
        {
            this.target = target;
            this.transform.LookAt(target.transform.position);
            this.PlayAnimation(eState.Attack);
        }

        private void PlayAnimation(eState state)
        {
            if (this.state != state)
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
                if (this.state == eState.Attack)
                {
                    this.state = state;
                    this.anim.SetInteger("State", (int)eState.Attack2);

                }
            }
        }

        private IEnumerator WaitForCompleteAttackAnimation()
        {
            yield return null;

            Debug.Log("���� �ִϸ��̼��� ���������� ��ٸ�");
            AnimatorStateInfo animStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
            bool isAttackState = animStateInfo.IsName("Attack01");
            Debug.LogFormat("isAttackState: {0}", isAttackState);
            if (isAttackState)
            {
                Debug.LogFormat("animStateInfo.length: {0}", animStateInfo.length);
            }
            else
            {
                Debug.LogFormat("Attack State�� �ƴմϴ�");
            }

            yield return new WaitForSeconds(this.impactTime);

            // target.HitDamage(this.atk);

            yield return new WaitForSeconds(animStateInfo.length - this.impactTime);
            this.PlayAnimation(eState.Idle);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.radius);
        }
        public void HitDamage(int atk)
        {
            Debug.LogFormat("���� hp{0}", this.hp);
            this.hp -= atk;
            if (this.hp < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}