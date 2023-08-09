using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test {
    public class HeroController : MonoBehaviour
    {
        private Vector3 targetPosition;
        private Coroutine moveRoutine;
        private Animator anim;
        public System.Action<MonsterController> onMoveComplete;
        public float radius = 1f;
        private MonsterController target;

        void Start()
        {
            this.anim = this.GetComponent<Animator>();

            
        }

        //Method Overload 
        public void Move(MonsterController target)
        {
            this.target = target;
            this.targetPosition = this.target.gameObject.transform.position;
            this.anim.SetInteger("State", 1);
            if (this.moveRoutine != null)
            {
                //이미 코루틴이 실행중이다 -> 중지 
                this.StopCoroutine(this.moveRoutine);
            }
            this.moveRoutine = this.StartCoroutine(this.CoMove());
        }

        public void Move(Vector3 targetPosition)
        {
            //타겟을 지움 
            this.target = null;

            //이동할 목표지점을 저장 
            this.targetPosition = targetPosition;
            Debug.Log("Move");

            this.anim.SetInteger("State", 1);

            if (this.moveRoutine != null) {
                //이미 코루틴이 실행중이다 -> 중지 
                this.StopCoroutine(this.moveRoutine);
            }
            this.moveRoutine = this.StartCoroutine(this.CoMove());
        }

        private IEnumerator CoMove()
        {
            while (true)
            {
                //방향을 바라봄 
                this.transform.LookAt(this.targetPosition);
                //이미 바라봤으니깐 정면으로 이동 (relateTo : Self/지역좌표)
                this.transform.Translate(Vector3.forward * 1f * Time.deltaTime);
                //목표지점과 나와의 거리를 잼 
                float distance = Vector3.Distance(this.transform.position, this.targetPosition);
                //Debug.Log(distance);
                //0이 될수 없다 가까워질때 도착한것으로 판단 하자 

                //타겟이 있을경우 
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
                

                yield return null;  //다음 프레임 시작 
            }

            Debug.Log("<color=yellow>도착!</color>");
            this.anim.SetInteger("State", 0);
            //대리자를 호출
            this.onMoveComplete(this.target);
        }

        private void OnDrawGizmos()
        {
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.radius, 40);
            //Gizmos.DrawWireSphere(this.transform.position, 1);
        }

        public void Attack(MonsterController target)
        {
            this.anim.SetInteger("State", 2);
            //코루틴 함수 호출 
            this.StartCoroutine(this.CoAttack());
        }

        private IEnumerator CoAttack()
        {
            //yield return null;  //다음 프레임 시작 
            yield return new WaitForSeconds(0.1f);  //0.1초 이후 
            //float elapsedTime = 0;
            //while (true) {
            //    elapsedTime += Time.deltaTime;
            //    if (elapsedTime > 0.1f) {
            //        break;
            //    }
            //    yield return null;
            //}

            Debug.Log("<color=red>Impact!!!!!</color>");

            //0.83 - 0.1
            yield return new WaitForSeconds(0.73f);

            Debug.Log("Attack애니메이션 종료");

            this.anim.SetInteger("State", 0);   //기본 동작으로 전환
        }
    }
}
