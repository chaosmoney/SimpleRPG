using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Test3
{
    public class MonsterGenerator : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> prefabList;    //동적 배열
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

        //몬스터 생성
        public MonsterController Generate(GameEnums.eMonsterType monsterType, Vector3 initPosition)
        {
            Debug.LogFormat("monsterType: {0}", monsterType);
            //몬스터 타입에 따라 어떤 프리팹으로 프리팹 복사본(인스턴스)를 생성할지 결정 해야 함 
            //몬스터 타입을 인덱스로 변경 
            int index = (int)monsterType;
            Debug.LogFormat("index: {0}", index);
            //프리팹 배열에서 인덱스로 프리팹 가져옴
            GameObject prefab = this.prefabList[index];
            Debug.LogFormat("prefab: {0}", index);
            //프리팹 인스턴스를 생성 
            GameObject go = Instantiate(prefab);    //위치를 결정 하지 않은 상태이기때문 (프리팹의 설정된 위치에 생성됨)
            //위치를 설정
            go.transform.position = initPosition;
            return go.GetComponent<MonsterController>();
        }
    }

    
}