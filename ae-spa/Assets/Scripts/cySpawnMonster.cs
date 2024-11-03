using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cySpawnMonster : MonoBehaviour
{
    public GameObject monObj;   // 몬스터1

    void Start()
    {
        InvokeRepeating("SpawnMon", 0f, 3.0f);      // 3초마다 생성
    }

    void SpawnMon()
    {
        GameObject obj = Instantiate(monObj);
        obj.transform.position = transform.position;
        Destroy(obj, 10f);                         // 10초 후 사라짐
    }
}
