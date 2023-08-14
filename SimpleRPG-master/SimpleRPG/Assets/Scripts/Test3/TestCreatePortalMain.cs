using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Test3
{
    public class TestCreatePortalMain : MonoBehaviour
    {
        [SerializeField]
        private GameObject hero;
        private HeroController heroController;
        [SerializeField]
        private MonsterGenerator monsterGenerator;
        private List<MonsterController> monsterList;
        [SerializeField]
        private PortalController portalController;
        [SerializeField]
        private List<ItemController> itemList;
        [SerializeField]
        private ItemGenerator itemGenerator;
        // Start is called before the first frame update
        void Start()
        {
            heroController = CreateHero(new Vector3(-3, 0, -3));
            this.monsterList = new List<MonsterController>();
            MonsterController turtle = this.monsterGenerator.Generate(GameEnums.eMonsterType.Turtle, new Vector3(0,0,3));
            Debug.Log(turtle);
            turtle.onDie = (itemType) =>
            {
                this.itemGenerator.Generate(itemType, turtle.transform.position);
                Destroy(turtle.gameObject);
            };
            MonsterController slime = this.monsterGenerator.Generate(GameEnums.eMonsterType.Slime, new Vector3(3, 0, 0));
            slime.onDie = (itemType) =>
            {
                this.itemGenerator.Generate(itemType, slime.transform.position);
                Destroy(slime.gameObject);
            };


            this.monsterList.Add(turtle);
            this.monsterList.Add(slime);

            this.itemList = new List<ItemController>();

            //this.monsterGenerator.onDie = () => {

            //    //아이템을 생성한다 
            //    //Instantiate(ItemList[]);
            //    ItemController item = this.itemGenerator.Generate(dropItemType);
            //    this.itemList.Add(item);

            //};

            


        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float maxDistance = 100f;
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 3f);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    MonsterController controller = hit.collider.gameObject.GetComponent<MonsterController>();
                    if(controller != null)
                    {
                        this.monsterList.Remove(controller);

                        controller.Die();
                        Debug.LogFormat("남은 수: {0}",this.monsterList.Count);
                        if (this.monsterList.Count == 0)
                        {
                            this.CreatePortal();
                        }
                    }
                    else if (hit.collider.tag == "Ground")
                    {
                        Debug.Log(hit.point);
                        this.heroController.Move(hit.point);
                    }
                }
            }

        }

        public void CreatePortal()
        {
            Instantiate(portalController);
        }

        public HeroController CreateHero(Vector3 initPosition)
        {
            GameObject go = Instantiate(hero);
            go.transform.position = initPosition;
            return go.GetComponent<HeroController>();
        }

        private void CreateItem(GameEnums.eItemType itemType, Vector3 position)
        {
            this.itemGenerator.Generate(itemType, position);
        }

    }
}
