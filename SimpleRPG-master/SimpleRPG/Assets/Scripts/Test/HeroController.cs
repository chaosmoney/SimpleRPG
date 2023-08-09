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

            this.anim.SetInteger("State", 1);

            if (this.moveRoutine != null) {
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
                this.transform.Translate(Vector3.forward * 1f * Time.deltaTime);
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
            //�ڷ�ƾ �Լ� ȣ�� 
            this.StartCoroutine(this.CoAttack());
        }

        private IEnumerator CoAttack()
        {
            //yield return null;  //���� ������ ���� 
            yield return new WaitForSeconds(0.1f);  //0.1�� ���� 
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

            Debug.Log("Attack�ִϸ��̼� ����");

            this.anim.SetInteger("State", 0);   //�⺻ �������� ��ȯ
        }
    }
}
