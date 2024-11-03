using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cySpawnMonster : MonoBehaviour
{
    public GameObject monObj;   // ����1

    void Start()
    {
        InvokeRepeating("SpawnMon", 0f, 3.0f);      // 3�ʸ��� ����
    }

    void SpawnMon()
    {
        GameObject obj = Instantiate(monObj);
        obj.transform.position = transform.position;
        Destroy(obj, 10f);                         // 10�� �� �����
    }
}
