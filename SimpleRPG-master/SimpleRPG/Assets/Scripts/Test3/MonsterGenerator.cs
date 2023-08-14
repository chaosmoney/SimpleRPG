using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Test3
{
    public class MonsterGenerator : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> prefabList;    //���� �迭
        // Start is called before the first frame update
        public Action onDie;
        void Start()
        {
            for (int i = 0; i < this.prefabList.Count; i++)
            {
                GameObject prefab = this.prefabList[i];
                Debug.LogFormat("index: {0}, prefab: {1}", i, prefab);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        //���� ����
        public MonsterController Generate(GameEnums.eMonsterType monsterType, Vector3 initPosition)
        {
            Debug.LogFormat("monsterType: {0}", monsterType);
            //���� Ÿ�Կ� ���� � ���������� ������ ���纻(�ν��Ͻ�)�� �������� ���� �ؾ� �� 
            //���� Ÿ���� �ε����� ���� 
            int index = (int)monsterType;
            Debug.LogFormat("index: {0}", index);
            //������ �迭���� �ε����� ������ ������
            GameObject prefab = this.prefabList[index];
            Debug.LogFormat("prefab: {0}", index);
            //������ �ν��Ͻ��� ���� 
            GameObject go = Instantiate(prefab);    //��ġ�� ���� ���� ���� �����̱⶧�� (�������� ������ ��ġ�� ������)
            //��ġ�� ����
            go.transform.position = initPosition;
            return go.GetComponent<MonsterController>();
        }
    }

    
}