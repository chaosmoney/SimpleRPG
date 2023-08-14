using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Test3
{
    public class HeroController : MonoBehaviour
    {
        public Animator anim;
        private Coroutine moveRoutine;
        private MonsterController target;
        private Vector3 targetPosition;
        private Action getItem;
        private GameObject Items;
        [SerializeField]
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Move(Vector3 targetPosition)
        {
            //Ÿ���� ���� 
            this.target = null;

            //�̵��� ��ǥ������ ���� 
            this.targetPosition = targetPosition;
            Debug.Log("Move");

            this.anim.SetInteger("State", 1);

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
        }
        public void GetItem(GameObject item)
        {
            Transform sword = this.gameObject.transform.Find("LeftHand");
            if (sword != null)
            {
                Destroy(sword.gameObject);
            }
            GameObject weapon = Instantiate(item, this.transform);
            this.Items = item;
        }
    }
}