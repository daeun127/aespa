using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cyMonsterCtrl : MonoBehaviour
{
    public float speed;  // 몬스터 이동 속도
    Vector3 moveDir;       // 몬스터 이동 방향

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        collMonster.isDie = false;

        // 플레이어를 향해 방향 설정
        moveDir = player.transform.position - transform.position;
        moveDir.y = 0;

        float angle = Vector3.SignedAngle(
            transform.forward, moveDir.normalized, Vector3.up);

        transform.Rotate(0, angle, 0);
    }

    void Update()
    {
        Vector3 deltaPos = moveDir.normalized * speed * Time.deltaTime;
        transform.Translate(deltaPos, Space.World);
    }
}
