using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace GameMain
{
    public class MonsterController : MonoBehaviour
    {
        [SerializeField]
        private float radius = 1f;
        private float hp = 40;
        public float Radius
        {
            get { return this.radius; }

        }
        private Animator anim;

        public Action onHit;
        [SerializeField]
        private GameEnums.eItemType itemType;
        public Action<GameEnums.eItemType> onDie;
        private enum eState
        {
            Idle, Hit, Die
        }
        eState state;
        // Start is called before the first frame update
        void Start()
        {
            this.anim = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HitDamage(int atk)
        {
            this.hp -= atk;

            if (this.hp <= 0)
            {
                this.Die();
            }
            else
            {
                Debug.Log("피해를 입었습니다");
                this.onHit();
                this.anim.SetInteger("State", (int)eState.Hit);
            }
        }

        public void Die()
        {
            bool state = false;
            AnimationClip[] animationClips = anim.runtimeAnimatorController.animationClips;
            for (int i = 0; i < animationClips.Length; i++)
            {
                if (animationClips[i].name == "Die")
                {
                    state = true;
                }
            }
            if (state)
            {
                StartCoroutine(CoDie());
            }
            else
            {
                this.onDie(this.itemType);
            }

        }
        public IEnumerator CoDie()
        {
            this.anim.SetInteger("State", 2);
            yield return new WaitForSeconds(2.5f);
            this.onDie(this.itemType);
        }
    }
}