using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBullet : MonoBehaviour
{
    public GameObject bulletObj;    // 총알 오브젝트
    public Text hpText;     // hp ui
    public Text killText;  // monster ui
    public Text GameClear;  // 게임클리어 ui
    public Text Game0ver;   // 게임오버 ui
    public static int countKill = 0;    // 몬스터 제거 횟수

    int countItem = 0;   // 아이템 획득 횟수
    int hp = 10;                 // 플레이어 체력 10으로 지정
    
    void Start()
    {
        Game0ver.enabled = false;   // ui 끄기
        GameClear.enabled = false;  // ui 끄기
    }

    void Update()
    {
        hpText.text = hp.ToString();    // 체력 int -> string
        killText.text = countKill.ToString();   // 몬스터 제거 횟수 int -> string

        // 플레이어 체력 0
        if (hp <= 0)    
        {
            Destroy(gameObject);    // 플레이어 제거
            hp = 0;
            Game0ver.enabled = true;    // Game Over 표시
            Time.timeScale = 0;     // 게임 중지
        }

        // 몬스터 10회 제거
        if (countKill == 10)
        {
            GameClear.enabled = true;   // Game Clear 표시
            Time.timeScale = 0;     // 게임 중지
        }

        // 마우스 우측 버튼 클릭 시 총알 발사
        if (Input.GetMouseButtonDown(1))
        {
            Shot(transform.forward);   // 전방으로 총알 발사
            if (countItem == 1)         // 아이템 1회 획득
            {
                Shot(-transform.forward);   // 후방 발사 추가
            }
            else if (countItem == 2)         // 아이템 2회 획득
            {
                Shot(-transform.forward);
                Shot(transform.right);      // 좌/우 발사 추가
                Shot(-transform.right);
            }
            else if (countItem > 2)         // 아이템 2회 획득
            {
                Shot(-transform.forward);
                Shot(transform.right);
                Shot(-transform.right);
                Shot(new Vector3(1, 0, 1));     // 대각선으로 총알 발사 추가
                Shot(new Vector3(1, 0, -1));
                Shot(new Vector3(-1, 0, 1));
                Shot(new Vector3(-1, 0, -1));
            }
        }
    }

    void Shot(Vector3 dir)  // 총알 발사 함수
    {
        GameObject obj = Instantiate(bulletObj);    // 총알 생성
        Vector3 shotPos = transform.position + transform.up * 0.05f;    // 위치
        obj.GetComponent<BulletMove>().SetPosDir(shotPos, dir);    // 스크립트 불러오기
        Destroy(obj, 5);   // 5초 후 삭제
    }

    public void OnTriggerEnter(Collider other)  // 충돌 처리
    {
        GameObject cymon = GameObject.FindGameObjectWithTag("cyMon");
        GameObject camon = GameObject.FindGameObjectWithTag("caMon");
        GameObject monsterbullet = GameObject.FindGameObjectWithTag("MonsterBullet");
        GameObject item = GameObject.FindGameObjectWithTag("Item");

        if (other.tag == "cyMon")     // 몬스터 & 플레이어
        {
            hp = hp - 2;                // 플레이어 체력 감소 -2
            Destroy(cymon, 1.0f);       // 몬스터 제거
        }
        else if (other.tag == "caMon")     // 몬스터 & 플레이어
        {
            hp = hp - 2;                // 플레이어 체력 감소 -2
            Destroy(camon, 1.0f);       // 몬스터 제거
        }
        else if (other.tag == "MonsterBullet")  // 몬스터 총알 & 플레이어
        {
            Destroy(monsterbullet);         // 몬스터 총알 제거
            hp--;                           // 플레이어 체력 감소 -1
        }
        else if (other.tag == "Item")       // 아이템 & 플레이어
        {
            countItem++;                    // 아이템 획득 횟수 증가
            Destroy(item);                  // 아이템 제거
        }
    }
}