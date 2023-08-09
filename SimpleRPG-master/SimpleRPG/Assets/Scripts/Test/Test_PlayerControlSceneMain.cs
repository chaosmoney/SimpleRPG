using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Test;
using Unity.VisualScripting;

namespace Test
{
    public class Test_PlayerControlSceneMain : MonoBehaviour
    {
        [SerializeField]
        private HeroController heroController;

        void Start()
        {
            this.heroController.onMoveComplete = (target) =>
            {
                Debug.LogFormat("<color=cyan>이동을 완료 했습니다. : {0}</color>", target);
                //타겟이 있다면 공격 애니메이션 실행
                if (target != null)
                {
                    this.heroController.Attack(target);
                }
            };
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("down");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float maxDistance = 100f;
                Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 3f);

                //Debug.LogFormat("-> {0}", this.transform.position == null);
                //if (this.transform.position == null)
                //{ 

                //}

                //this.transform.position = null;


                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    //클릭한 오브젝트가 몬스터라면 
                    if (hit.collider.tag == "Monster")
                    {
                        //거리를 구한다 
                        float distance = Vector3.Distance(this.heroController.gameObject.transform.position,
                            hit.collider.gameObject.transform.position);

                        MonsterController monsterController = hit.collider.gameObject.GetComponent<MonsterController>();

                        //각 반지름더한거와 비교 
                        float sumRadius = this.heroController.radius + monsterController.radius;

                        Debug.LogFormat("{0}, {1}", distance, sumRadius);

                        //사거리 안에 들어옴 
                        if (distance <= sumRadius)
                        {
                            //공격 
                        }
                        else
                        {
                            //이동 
                            this.heroController.Move(monsterController);
                            //this.heroController.Move(hit.point);
                        }

                    }
                    else if (hit.collider.tag == "Ground")
                    {
                        this.heroController.Move(hit.point);
                    }
                }
            }
        }

    }
}
