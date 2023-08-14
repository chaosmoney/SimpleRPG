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
        //������ �迭���� �ε����� ������ ������
        GameObject prefab = this.prefabList[index];
        Debug.LogFormat("prefab: {0}", index);
        //������ �ν��Ͻ��� ���� 
        GameObject go = Instantiate(prefab);    //��ġ�� ���� ���� ���� �����̱⶧�� (�������� ������ ��ġ�� ������)
                                                //��ġ�� ����
        go.transform.position = initPosition;
        return go.GetComponent<ItemController>();
    }
}
