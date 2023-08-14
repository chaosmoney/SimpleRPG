using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Test4
{
    public class TestEquipItemMain : MonoBehaviour
    {
        [SerializeField]
        private Button btnRemoveSword;
        [SerializeField]
        private Button equipSword0; //생성시 부모를 지정
        [SerializeField]
        private Button equipSword1; //생성후 부모를 지정
        [SerializeField]
        private Button btnRemoveShield;
        [SerializeField]
        private Button equipShield0;
        [SerializeField]
        private Button equipShield1;
        [SerializeField]
        private HeroController heroController;
        [SerializeField]
        private GameObject swordPrefab;
        [SerializeField]
        private GameObject shieldPrefab;



        // Start is called before the first frame update
        void Start()
        {
            this.btnRemoveSword.onClick.AddListener(() => {
                Debug.Log("영웅의 칼이 있다면 씬에서 제거");
                this.heroController.UnEquipWeapon();
            });

            this.equipSword0.onClick.AddListener(() => {
                bool hasWeapon = this.heroController.HasWeapon();
                if(!hasWeapon)
                {
                    GameObject go = Instantiate(this.swordPrefab, this.heroController.WeaponTrans);
                }
                else
                {
                    Debug.Log("이미 착용 중입니다.");
                }
            });

            this.equipSword1.onClick.AddListener(() => {
                bool hasWeapon = this.heroController.HasWeapon();
                if(!hasWeapon) {
                    GameObject go = Instantiate(this.swordPrefab);
                    go.transform.SetParent(this.heroController.WeaponTrans);
                    //위치를 초기화
                    go.transform.localPosition = Vector3.zero;
                    //회전을 초기화
                    go.transform.localRotation = Quaternion.identity;
                }
                else
                {
                    Debug.Log("이미 착용 중입니다.");
                }
            });

            this.btnRemoveShield.onClick.AddListener(() => {
                this.heroController.UnEquipShield();
            });

            this.equipShield0.onClick.AddListener(() => {
                bool hasShield = this.heroController.HasShield();
                if (!hasShield)
                {
                    GameObject go = Instantiate(shieldPrefab, this.heroController.ShieldTrans);
                }
                else
                {
                    Debug.Log("이미 방패가 있습니다.");
                }
            });

            this.equipShield1.onClick.AddListener(() => {
                bool hasShield = this.heroController.HasShield();
                if(!hasShield)
                {
                    GameObject go = Instantiate(shieldPrefab);
                    go.transform.SetParent(this.heroController.ShieldTrans);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localRotation = Quaternion.identity;
                }
            });
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        
    }
}