using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test4
{
    public class HeroController : MonoBehaviour
    {
        [SerializeField]
        private Transform weaponTrans;
        [SerializeField]
        private Transform shieldTrans;


        public Transform WeaponTrans
        {
            get
            {
                return this.weaponTrans;
            }
        }

        public Transform ShieldTrans
        {
            get
            {
                return this.shieldTrans;
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void UnEquipWeapon()
        {
            Debug.LogFormat("자식의 수: {0}", this.weaponTrans.childCount);
            if(this.weaponTrans.childCount == 0 ) {
                Debug.Log("착용 중인 무기가 없습니다");
            }
            else
            {
                Debug.Log("착용 중인 무기가 있습니다");
                Transform child = this.weaponTrans.GetChild(0);
                Destroy(child.gameObject);
            }
        }

        public void UnEquipShield()
        {
            if(this.shieldTrans.childCount == 0 )
            {
                Debug.Log("착용 중인 방패가 없습니다.");
            }
            else
            {
                Debug.Log("착용 중인 방패가 있습니다.");
                Transform child = this.shieldTrans.GetChild(0);
                Destroy(child.gameObject);
            }
        }

        public bool HasWeapon()
        {
            return this.weaponTrans.childCount > 0;
        }

        public bool HasShield()
        {
            return this.shieldTrans.childCount > 0;
        }
    }
}