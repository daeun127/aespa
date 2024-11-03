using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caSpawnMonster : MonoBehaviour
{
    public GameObject camonObj;

    void Start()
    {
        InvokeRepeating("SpawnMon", 0f, 4.0f);      // 4초마다 생성
    }

    void SpawnMon()
    {
        GameObject obj = Instantiate(camonObj);
        obj.transform.position = transform.position;
        Destroy(obj, 10f);                          // 10초 후 사라짐
    }
}
