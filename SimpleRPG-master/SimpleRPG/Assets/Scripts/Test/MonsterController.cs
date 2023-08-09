using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
    {
    public class MonsterController : MonoBehaviour
    {
        public float radius = 1f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDrawGizmos()
        {
            GizmosExtensions.DrawWireArc(this.transform.position, this.transform.forward, 360, this.radius, 40);
            //Gizmos.DrawWireSphere(this.transform.position, 1);
        }
    }
}
