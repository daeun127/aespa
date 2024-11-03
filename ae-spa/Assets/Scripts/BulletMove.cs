using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    Vector3 direction;

    void Update()
    {
        // 총알 위치
        Vector3 deltaPos = direction * speed * Time.deltaTime;
        transform.Translate(deltaPos);
    }

    // 총알 위치 방향 설정
    public void SetPosDir(Vector3 pos, Vector3 dir)
    {
        transform.position = pos;
        direction = dir;
    }
}
