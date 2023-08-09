using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    public class PlayerAttackSceneMain : MonoBehaviour
    {

        [SerializeField]
        private Button btnAttack;
        [SerializeField]
        private HeroController heroController;
        [SerializeField]
        private MonsterController monsterController;
        [SerializeField]
        private GameObject hitFxPrefab;
        [SerializeField]
        private GameObject grave;


        // Start is called before the first frame update
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

            this.monsterController.onHit = () => {
                Debug.Log("이펙트 생성");
                Vector3 offset = new Vector3(0, 0.5f, 0);
                Vector3 tpos = this.monsterController.transform.position + offset;
                Debug.LogFormat("생성위치 {0}", tpos);
                GameObject fxGo = Instantiate(this.hitFxPrefab);
                fxGo.transform.position = tpos;
                fxGo.GetComponent<ParticleSystem>().Play();
            };

            this.monsterController.onDie = () =>
            {
                GameObject grave = Instantiate(this.grave);
                grave.transform.position = monsterController.transform.position;

            };

            this.btnAttack.onClick.AddListener(() => {
                Vector3 a = heroController.gameObject.transform.position;
                Vector3 b = monsterController.gameObject.transform.position;
                Vector3 c = b - a;
                //시작위치, 방향
                float distance = c.magnitude;
                float radius = this.heroController.Radius + this.monsterController.Radius;
                Debug.LogFormat("radius: {0}", radius);
                Debug.LogFormat("IsWithinRange: <color=lime>{0}</color>", this.IsWithinRange(distance, radius));
                if (this.IsWithinRange(distance, radius))
                {
                    this.heroController.Attack(this.monsterController);
                }

            });
        }

        private bool IsWithinRange(float distance, float radius)
        {
            return radius > distance;
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
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
                        float sumRadius = this.heroController.Radius + monsterController.Radius;

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
