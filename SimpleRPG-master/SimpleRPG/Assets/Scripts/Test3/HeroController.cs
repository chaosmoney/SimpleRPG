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
            //타겟을 지움 
            this.target = null;

            //이동할 목표지점을 저장 
            this.targetPosition = targetPosition;
            Debug.Log("Move");

            this.anim.SetInteger("State", 1);

            if (this.moveRoutine != null)
            {
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