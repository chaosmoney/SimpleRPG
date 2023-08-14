using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Test3
{
    public class MonsterController : MonoBehaviour
    {
        private GameEnums.eItemType itemType;
        public Action<GameEnums.eItemType> onDie;
        [SerializeField]
        private Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Die()
        {
            bool state = false;
            AnimationClip[] animationClips = anim.runtimeAnimatorController.animationClips;
            for(int i = 0; i < animationClips.Length; i++)
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