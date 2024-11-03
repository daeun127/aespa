using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caMonsterBullet : MonoBehaviour
{
    public GameObject cabulletObj;  // 총알 오브젝트
    GameObject player;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("caMonShot", 0f, 1.0f);   // 1초마다 총알 발사
    }

    void caMonShot()
    {
        if(collMonster.isDie == false)  // 사망 상태에서 공격 중지
        {
            GameObject obj = Instantiate(cabulletObj);  // 총알 생성

            Vector3 shotPos = transform.position + transform.up * 0.05f;    // 발사 위치
            Vector3 moveDir = player.transform.position- transform.position;   // 플레이어를 향해 방향 설정
            moveDir.y = 0;

            obj.GetComponent<BulletMove>().SetPosDir(shotPos, moveDir);     // 총알 발사
            Destroy(obj, 5);    // 5초 후 제거
        }
    }
}
