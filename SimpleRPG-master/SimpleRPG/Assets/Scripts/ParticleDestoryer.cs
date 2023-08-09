using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestoryer : MonoBehaviour
{
    private ParticleSystem ps;
    private float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        this.ps = this.GetComponent<ParticleSystem>();
        this.StartCoroutine(this.CoWaitForPlayAfterDestory());
    }

    private IEnumerator CoWaitForPlayAfterDestory()
    {
        yield return new WaitForSeconds(this.ps.main.duration);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
