using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test2
{
    public class Test_PlayerAttackSceneMain : MonoBehaviour
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
                Debug.LogFormat("IsWithinRange: <color=lime>{0}</color>",this.IsWithinRange(distance,radius));
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

        }

    }
}
