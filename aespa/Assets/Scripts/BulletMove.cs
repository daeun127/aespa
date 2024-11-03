using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speedBullet;       // 총알 속도
    Vector3 direction;          // 총알 방향

    void Update()
    {
        Vector3 deltaPos = direction * speedBullet * Time.deltaTime;        // 위치 값 = 방향 * 속도 * 이동 시간
        transform.Translate(deltaPos);                                      // 총알 이동
    }

    public void SetPosDir(Vector3 pos, Vector3 dir)         // 총알 발사 위치, 방향 받아오는 함수
    {
        transform.position = pos;           // 총알 위치 설정
        direction = dir;                    // 총알 방향 설정

    }
}
