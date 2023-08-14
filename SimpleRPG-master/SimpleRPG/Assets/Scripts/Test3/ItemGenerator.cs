using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Test3;
using static Test3.GameEnums;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabList;
    // Start is called before the first frame update
    public Action onDie;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ItemController Generate(GameEnums.eItemType itemType, Vector3 initPosition)
    {
        int index = (int)itemType;
        Debug.LogFormat("index: {0}", index);
        //프리팹 배열에서 인덱스로 프리팹 가져옴
        GameObject prefab = this.prefabList[index];
        Debug.LogFormat("prefab: {0}", index);
        //프리팹 인스턴스를 생성 
        GameObject go = Instantiate(prefab);    //위치를 결정 하지 않은 상태이기때문 (프리팹의 설정된 위치에 생성됨)
                                                //위치를 설정
        go.transform.position = initPosition;
        return go.GetComponent<ItemController>();
    }
}
