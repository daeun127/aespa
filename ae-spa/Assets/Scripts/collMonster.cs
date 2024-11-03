using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collMonster : MonoBehaviour
{
    public Material aftershot;          // 피격 시 색
    public static bool isDie = false;   // 몬스터 사망 판정

    private cyMonsterCtrl cyMove;
    private cyMonsterBullet cyShot;
    private caMonsterCtrl caMove;
    private caMonsterBullet caShot;
    GameObject thisMon;


    private void Start()
    {
        cyMove = GetComponent<cyMonsterCtrl>();
        cyShot = GetComponent<cyMonsterBullet>();
        caMove = GetComponent<caMonsterCtrl>();
        caShot = GetComponent<caMonsterBullet>();

    }

    public void OnTriggerEnter(Collider other)  // 충돌 처리
    {
        GameObject playerbullet = GameObject.FindGameObjectWithTag("PlayerBullet");

        if (other.tag == "PlayerBullet")                        // 몬스터 & 플레이어 총알
        {
            if (isDie == false)                                 // if(live)
            {
                if (this.tag == "cyMon")
                {
                    isDie = true;                                       // die
                    playerBullet.countKill++;                           // 제거한 몬스터 개수 증가
                    GetComponent<Renderer>().material = aftershot;      // 검은색으로 변경
                    cyMove.enabled = false;                             // 이동 중지
                    cyShot.enabled = false;                             // 공격 중지
                    Destroy(playerbullet);                              // 플레이어 총알 제거
                    Destroy(gameObject, 1.0f);                          // 몬스터 1초 후 제거
                }
                else if (this.tag == "caMon")
                {
                    isDie = true;                                       // die
                    playerBullet.countKill++;                           // 제거한 몬스터 개수 증가
                    GetComponent<Renderer>().material = aftershot;      // 검은색으로 변경
                    caMove.enabled = false;                             // 이동 중지
                    caShot.enabled = false;                             // 공격 중지
                    Destroy(playerbullet);                              // 플레이어 총알 제거
                    Destroy(gameObject, 1.0f);                          // 몬스터 1초 후 제거
                }
            }
        }
    }
}
