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
                Debug.LogFormat("<color=cyan>�̵��� �Ϸ� �߽��ϴ�. : {0}</color>", target);
                //Ÿ���� �ִٸ� ���� �ִϸ��̼� ����
                if (target != null)
                {
                    this.heroController.Attack(target);
                }
            };

            this.monsterController.onHit = () => {
                Debug.Log("����Ʈ ����");
                Vector3 offset = new Vector3(0, 0.5f, 0);
                Vector3 tpos = this.monsterController.transform.position + offset;
                Debug.LogFormat("������ġ {0}", tpos);
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
                //������ġ, ����
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
                    //Ŭ���� ������Ʈ�� ���Ͷ�� 
                    if (hit.collider.tag == "Monster")
                    {
                        //�Ÿ��� ���Ѵ� 
                        float distance = Vector3.Distance(this.heroController.gameObject.transform.position,
                            hit.collider.gameObject.transform.position);

                        MonsterController monsterController = hit.collider.gameObject.GetComponent<MonsterController>();

                        //�� ���������Ѱſ� �� 
                        float sumRadius = this.heroController.Radius + monsterController.Radius;

                        Debug.LogFormat("{0}, {1}", distance, sumRadius);

                        //��Ÿ� �ȿ� ���� 
                        if (distance <= sumRadius)
                        {
                            //���� 
                        }
                        else
                        {
                            //�̵� 
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
