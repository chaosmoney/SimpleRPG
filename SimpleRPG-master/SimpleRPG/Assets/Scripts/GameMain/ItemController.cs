using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameMain
{
    public class ItemController : MonoBehaviour
    {
        public GameEnums.eItemType itemType;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Hero")
            {
                Destroy(this.gameObject);
            }
        }
    }
}