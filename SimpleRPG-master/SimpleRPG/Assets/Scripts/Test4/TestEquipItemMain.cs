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
        private Button equipSword0; //������ �θ� ����
        [SerializeField]
        private Button equipSword1; //������ �θ� ����
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
                Debug.Log("������ Į�� �ִٸ� ������ ����");
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
                    Debug.Log("�̹� ���� ���Դϴ�.");
                }
            });

            this.equipSword1.onClick.AddListener(() => {
                bool hasWeapon = this.heroController.HasWeapon();
                if(!hasWeapon) {
                    GameObject go = Instantiate(this.swordPrefab);
                    go.transform.SetParent(this.heroController.WeaponTrans);
                    //��ġ�� �ʱ�ȭ
                    go.transform.localPosition = Vector3.zero;
                    //ȸ���� �ʱ�ȭ
                    go.transform.localRotation = Quaternion.identity;
                }
                else
                {
                    Debug.Log("�̹� ���� ���Դϴ�.");
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
                    Debug.Log("�̹� ���а� �ֽ��ϴ�.");
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