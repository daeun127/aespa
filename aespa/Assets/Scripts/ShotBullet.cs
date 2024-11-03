using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ShotBullet : MonoBehaviour
{
    public GameObject bulletObj;            // 총알 오브젝트
    public GameObject BM;                   // 블랙 맘바 오브젝트
    public GameObject[] UIbullets;          // 총알 UI 오브젝트

    static public float gagef;            // 카리나 게이지 실수
    public float dehpf;                     // 사용자 HP 감소 값
    public Image gage;                      // 카리나 게이지 이미지 UI
    public Image hpae;                      // 사용자 HP 이미지 UI
    float hpaef = 1;                        // 사용자 HP 실수
    public Text stateMsg;                   // 상태 UI

    public ParticleSystem KarinaEff;        // 카리나 총알 효과
    public GameObject KarinaBtn;            // 카리나 버튼
    int curBullet = 0;               // 현재 총알 발사 횟수 == 0
    float time = 0;                      // 총알 재생성 시간   

    public AudioSource effSound;                // 이펙트 오디오 소스

    private void Start()        // 시작 함수
    {
        gagef = 0;      // 카리나 게이지 값 0
    }

    void Update()          // 매 프레임 호출
    {
        if (gage.enabled == true)                   // ui가 활성화 되어있는 동안
        {
            gage.fillAmount = gagef;                  // 카리나 게이지 실수 값 반영
        }
        if (hpae.enabled == true)                   // ui가 활성화 되어있는 동안
        {
            hpae.fillAmount = hpaef;                  // hp  실수 값 반영
        }

        if (UIbullets[UIbullets.Length-1].activeSelf)           // 마지막 총알이 켜져있을 때 == 총알 남아있을 때
        {
            if (Input.GetMouseButtonDown(1))        // 마우스 왼쪽 버튼 클릭 시
            {
                if (ArmCtrl.selectOb.name == "Gun1")            // 무기 패널에서 선택한 총이 총1
                {
                    Shot1(Vector3.forward);                         // 총알 발사
                    Shot1(new Vector3(-0.2f, 0, 1));                // 총알 발사
                    Shot1(new Vector3(0.2f, 0, 1));                 // 총알 발사
                }
                else if (ArmCtrl.selectOb.name == "Gun2")           // 무기 패널에서 선택한 총이 총2
                {

                    StartCoroutine(Shot2(Vector3.forward));            // 총알 발사
                    StartCoroutine(Shot2(new Vector3(0, 0.08f, 1)));       // 총알 발사
                    StartCoroutine(Shot2(new Vector3(0, 0.14f, 1)));       // 총알 발사
                }
                UIbullets[curBullet].SetActive(false);                          // 현재 총알 UI 비활성화
                curBullet++;                                                    // 총알 발사 횟수 +1
            }
        }
        else                                                        // 마지막 총알이 꺼져있을 때 == 총알 없을 때
        {
            stateMsg.enabled = true;                                     // 상태 text 활성화
            stateMsg.text = "총알 재장전 " + (5-(int)time) + "초 전";          // 상태 text 변경
            time += Time.deltaTime;

            if (time >= 5)            // 5초 이상 흐르면
            {
                stateMsg.enabled = false;                                    // 상태 text 비활성화
                for (int i = 0; i < UIbullets.Length; i++)     // 모든 총알 UI
                    UIbullets[i].SetActive(true);                    // 총알 UI 켜기

                curBullet = 0;                                               // 현재 총알 0으로 설정
                time = 0;                         // 총알 재생성 시간 초기화
            }
        }
    }

    public void OnClickKarina()         // 카리나 버튼 클릭 시
    {
        if (gage.fillAmount == 1)           // 카리나 게이지가 1이면
        {
            curBullet = 0;                  // 현재 총알 0
            for (int i = curBullet; i < UIbullets.Length; i++)      // 모든 총알 UI
            {
                UIbullets[i].SetActive(true);                   // 총알 UI 켜기
            }

            ParticleSystem ps = Instantiate(KarinaEff);              //  효과 생성
            ps.transform.position = BM.transform.position;              // 효과 위치 = 블랙맘바 위치
            effSound.Play();                                        // 효과 소리 플레이

            BMCtrl.hpBMf = BMCtrl.hpBMf / 2;                   // 블랙맘바 hp 감소
            gagef = 0;                                          // 카리나 게이지 실수값 0

        }
    }

    void Shot1(Vector3 dir)              // 총1 총알 발사 함수
    {
        GameObject obj = Instantiate(bulletObj);            // 총알 생성
        Vector3 shotPos = transform.position + transform.up * 1.0f;     // 발사 위치 설정

        obj.GetComponent<BulletMove>().SetPosDir(shotPos, dir);         // BulletMove 스크립트의 SetPosDir 함수에 매개변수 전달
        Destroy(obj, 10);                                           //10 초 후 총알 삭제
    }

    IEnumerator Shot2(Vector3 dir)              // 총2 총알 발사 함수
    {
        GameObject obj = Instantiate(bulletObj);            // 총알 생성
        Vector3 shotPos = transform.position + transform.up * 1.0f;     // 발사 위치 설정

        obj.GetComponent<BulletMove>().SetPosDir(shotPos, dir);         // BulletMove 스크립트의 SetPosDir 함수에 매개변수 전달
        Destroy(obj, 10);                                           //10 초 후 총알 삭제

        yield return new WaitForSeconds(1f);            // 1초 대기
    }

    public void OnTriggerEnter(Collider other)  // 충돌 처리
    {
        GameObject BulletBM = GameObject.FindGameObjectWithTag("BulletBM"); // 충돌한 오브젝트의 태그가 블랙맘바 총알인 게임 오브젝트

        if (other.tag == "BulletBM")     // 몬스터 총알 & 플레이어
        {
            hpaef -= dehpf;       // 사용자 hp 감소
            Destroy(BulletBM, 1.0f);       // 몬스터 총알 제거

            if (hpae.fillAmount == 0)       // 사용자 hp가 0일 때
            {
                SceneManager.LoadScene("GameOver");     // GameOver 이동
            }
        }
    }
}
