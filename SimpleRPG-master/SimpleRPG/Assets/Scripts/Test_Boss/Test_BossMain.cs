using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Test_Boss
{
    
    public class Test_BossMain : MonoBehaviour
    {
        [SerializeField]
        private Bull bull;
        [SerializeField]
        private HeroController hero;


        // Start is called before the first frame update
        void Start()
        {
            this.hero.onHit = () =>
            {
                this.hero.HitDamage(this.bull.Atk);
            };

            this.bull.onMoveComplete = () => {
                this.BullMoveAndAttack();
            };

            this.bull.onAttackCancel = () =>
            {

                this.BullMoveAndAttack();
            };
            this.bull.MoveForward(hero.transform);
            this.hero.onMoveComplete = (target) =>
            {
                Debug.LogFormat("<color=cyan>이동을 완료 했습니다. : {0}</color>", target);
                //타겟이 있다면 공격 애니메이션 실행
                if (target != null)
                {
                    this.hero.Attack(target);
                }
            };
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float maxDistance = 100f;
                Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 1f);

                RaycastHit hit;
                if(Physics.Raycast(ray,out hit, maxDistance))
                {
                    //클릭한 오브젝트가 몬스터라면 
                    if (hit.collider.tag == "Monster")
                    {
                        //거리를 구한다 
                        float distance = Vector3.Distance(this.hero.transform.position,
                            hit.collider.gameObject.transform.position);

                        global::Bull monsterController = hit.collider.gameObject.GetComponent<global::Bull>();

                        //각 반지름더한거와 비교 
                        float sumRadius = this.hero.Radius + this.bull.Range;

                        Debug.LogFormat("{0}, {1}", distance, sumRadius);

                        //사거리 안에 들어옴 
                        if (distance <= sumRadius)
                        {
                            //공격 
                            this.hero.Attack(bull);
                        }
                        else
                        {
                            //이동 
                            this.hero.Move(bull);
                            //this.heroController.Move(hit.point);
                        }

                    }
                    else if (hit.collider.tag == "Ground")
                    {
                        this.hero.Move(hit.point);
                    }
                }
            }
        }

        private void BullMoveAndAttack()
        {
            var dis = Vector3.Distance(this.bull.transform.position, this.hero.transform.position);
            if (dis <= this.bull.Range) 
            {
                this.bull.Attack(this.hero.transform);
            }
            else 
            {
                this.bull.MoveForward(this.hero.transform);
            }
        }
    }
}