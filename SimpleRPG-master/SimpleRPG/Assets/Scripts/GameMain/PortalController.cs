using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMain
{
    public class PortalController : MonoBehaviour
    {
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
            if (other.gameObject.tag == "Hero")
            {
                Debug.Log(123);
                SceneManager.LoadScene("BossScene");
            }
        }
        

    }
}