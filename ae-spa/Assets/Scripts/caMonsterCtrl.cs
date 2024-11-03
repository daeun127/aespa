using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caMonsterCtrl : MonoBehaviour
{
    public float speed; // 몬스터 이동 속도
    Vector3 deltaPos;   // 몬스터 위치

    void Start()
    {
        collMonster.isDie = false;

        InvokeRepeating("SetrandPos", 1.0f, 2.0f);     // 2초마다 함수 호출
    }

    void SetrandPos()   // 랜덤 방향 설정
    {
        deltaPos.x = Random.Range(-0.2f, 0.2f);
        deltaPos.y = 0;
        deltaPos.z = Random.Range(-0.2f, 0.2f);
    }

    void Update()
    {
        transform.Translate(deltaPos * speed * Time.deltaTime, Space.World);    // 위치 이동
    }
}
