using System.Collections;
using System.Collections.Generic;
using Test3;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    public class PlayerAttackSceneMain : MonoBehaviour
    {
        [SerializeField]
        private Button btnRemoveSword;
        [SerializeField]
        private Button equipSword;
        [SerializeField]
        private Button btnRemoveShield;
        [SerializeField]
        private Button equipShield;
        [SerializeField]
        private GameObject swordPrefab;
        [SerializeField]
        private GameObject shieldPrefab;
        [SerializeField]
        private GameObject hero;
        private HeroController heroController;
        [SerializeField]
        private MonsterGenerator monsterGenerator;
        private List<MonsterController> monsterList;
        [SerializeField]
        private GameObject hitFxPrefab;
        private List<ItemController> itemList;
        [SerializeField]
        private ItemGenerator itemGenerator;
        [SerializeField]
        private PortalController portalController;
        private bool isClear = false;


        // Start is called before the first frame update
        void Start()
        {
            this.heroController = CreateHero(new Vector3(-3, 0, -3));

            this.monsterList = new List<MonsterController>();
            MonsterController turtle = this.monsterGenerator.Generate(GameEnums.eMonsterType.Turtle, new Vector3(0, 0, 3));
            turtle.onDie = (itemType) =>
            {
                this.itemGenerator.Generate(itemType, turtle.transform.position);
                this.monsterList.Remove(turtle);
                Destroy(turtle.gameObject);
            };
            turtle.onHit = () => {
                Debug.Log("����Ʈ ����");
                Vector3 offset = new Vector3(0, 0.5f, 0);
                Vector3 tpos = turtle.transform.position + offset;
                Debug.LogFormat("������ġ {0}", tpos);
                GameObject fxGo = Instantiate(this.hitFxPrefab);
                fxGo.transform.position = tpos;
                fxGo.GetComponent<ParticleSystem>().Play();
            };
            MonsterController slime = this.monsterGenerator.Generate(GameEnums.eMonsterType.Slime, new Vector3(3, 0, 0));
            slime.onDie = (itemType) =>
            {
                this.itemGenerator.Generate(itemType, slime.transform.position);
                this.monsterList.Remove(slime);
                Destroy(slime.gameObject);
            };
            slime.onHit = () => {
                Debug.Log("����Ʈ ����");
                Vector3 offset = new Vector3(0, 0.5f, 0);
                Vector3 tpos = slime.transform.position + offset;
                Debug.LogFormat("������ġ {0}", tpos);
                GameObject fxGo = Instantiate(this.hitFxPrefab);
                fxGo.transform.position = tpos;
                fxGo.GetComponent<ParticleSystem>().Play();
            };

            this.monsterList.Add(turtle);
            this.monsterList.Add(slime);

            this.heroController.onMoveComplete = (target) =>
            {
                Debug.LogFormat("<color=cyan>�̵��� �Ϸ� �߽��ϴ�. : {0}</color>", target);
                //Ÿ���� �ִٸ� ���� �ִϸ��̼� ����
                if (target != null)
                {
                    this.heroController.Attack(target);
                }
            };

            this.btnRemoveSword.onClick.AddListener(() => {
                Debug.Log("������ Į�� �ִٸ� ������ ����");
                this.heroController.UnEquipWeapon();
            });

            this.btnRemoveShield.onClick.AddListener(() => {
                Debug.Log("������ Į�� �ִٸ� ������ ����");
                this.heroController.UnEquipShield();
            });

            this.equipSword.onClick.AddListener(() => {
                bool hasWeapon = this.heroController.HasWeapon();
                if (!hasWeapon)
                {
                    GameObject go = Instantiate(this.swordPrefab, this.heroController.WeaponTrans);
                }
                else
                {
                    Debug.Log("�̹� ���� ���Դϴ�.");
                }
            });

            this.equipShield.onClick.AddListener(() => {
                bool hasWeapon = this.heroController.HasWeapon();
                if (!hasWeapon)
                {
                    GameObject go = Instantiate(this.shieldPrefab, this.heroController.WeaponTrans);
                }
                else
                {
                    Debug.Log("�̹� ���� ���Դϴ�.");
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
                            this.heroController.Attack(monsterController);
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
            if (this.monsterList.Count == 0)
            {
                if (isClear == false)
                {
                    this.CreatePortal();
                    isClear = true;
                }
            }
        }
        public HeroController CreateHero(Vector3 initPosition)
        {
            GameObject go = Instantiate(hero);
            go.transform.position = initPosition;
            return go.GetComponent<HeroController>();
        }

        public void CreatePortal()
        {
            Instantiate(portalController);
        }
    }
}
