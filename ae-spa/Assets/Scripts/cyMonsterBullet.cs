using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cyMonsterBullet : MonoBehaviour
{
    public GameObject cybulletObj;

    void Start()
    {
        InvokeRepeating("cyMonShot", 0f, 1.0f);    // 1초마다 총알 발사
    }

    void cyMonShot()
    {
        if(collMonster.isDie == false)  // 사망 상태에서 공격 중지
        {
            GameObject obj = Instantiate(cybulletObj);
            Vector3 shotPos = transform.position + transform.up * 0.05f;

            // 랜덤 방향으로 총알 발사
            Vector3 dir;
            dir.x = Random.Range(-0.2f, 0.2f);
            dir.y = 0;
            dir.z = Random.Range(-0.2f, 0.2f);

            obj.GetComponent<BulletMove>().SetPosDir(shotPos, dir);
            Destroy(obj, 5);    // 5초 후 제거
        }
    }
}
