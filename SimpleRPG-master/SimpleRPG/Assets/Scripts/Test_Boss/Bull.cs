using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test_Boss
{
    public class Bull : MonoBehaviour
    {
        public System.Action onMoveComplete;
        public System.Action onAttackCancel;
        public System.Action onHit;
        private Transform targetTrans;

        [SerializeField]
        private float sight = 5f;
        [SerializeField]
        private float range = 1f;
        private Vector3 startPosition;
        private Animator anim;
        private int atk = 10;
        public int Atk => this.atk;

        public float Range => this.range;
        public float Sight => this.sight;

        void Start()
        {
            startPosition = this.transform.position;
            this.anim = GetComponent<Animator>();
        }
        public void MoveForward(Transform targetTras)
        {
            this.targetTrans = targetTras;
            this.StartCoroutine(this.CoMoveForward()); 
        }

        private IEnumerator CoMoveForward()
        {
            //매프레임마다 앞으로 이동 
            while (true)
            {
                if (targetTrans == null)
                    break;
                
                var dis = Vector3.Distance(this.transform.position, this.targetTrans.position);

                //시야안에 있는지확인 
                if (!isTargetInSight())
                {
                    if (Vector3.Distance(this.transform.position, this.startPosition) > 0.2f)
                    {
                        this.transform.LookAt(startPosition);
                        this.transform.Translate(Vector3.forward * 3f * Time.deltaTime);
                    }
                    
                    yield return null;  //한 프레임 건너뜀 
                    continue;
                }
                
                
                this.transform.LookAt(this.targetTrans);
                this.transform.Translate(Vector3.forward * 1f * Time.deltaTime);
                

                if (dis <= this.range)
                {
                    break;
                }
                yield return null;
            }
            if (this.targetTrans == null)
            {
                Debug.Log("타겟을 잃었습니다.");
            }
            else
            {
                Debug.Log("이동을 완료함");
                this.onMoveComplete();
            }

        }

        private bool isTargetInSight()
        {
            if(Vector3.Distance(this.transform.position, targetTrans.position)< this.sight)
            {
                this.anim.SetInteger("State", 1);
                return true;
            }
            return false;
        }

        public void Attack(Transform targetTrans)
        {
            this.targetTrans = targetTrans;
            this.StartCoroutine(this.CoAttack());
        }

        private IEnumerator CoAttack()
        {
            yield return null;

            var dis = Vector3.Distance(this.transform.position, this.targetTrans.position);

            this.anim.SetInteger("State", 2);

            this.onHit();

            if(onAttackCancel != null)
            {
                this.onAttackCancel();
            }
            
        }


        private void OnDrawGizmos()
        {
            //시야 
            Gizmos.color = Color.yellow;
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.sight, 20);
            //공격사거리
            Gizmos.color = Color.red;
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.range, 20);
        }
    }

}