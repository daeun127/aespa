using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BMCtrl : MonoBehaviour
{
    public GameObject nextImage;            // nextlevel UI
    public GameObject bulletObj;            // 총알 오브젝트
    public Image hpBM;                      // 블랙맘바 hp UI
    static public float hpBMf;                   // 블랙맘바 hp 실수

    public Text GiselleText;                // 지젤 text
    public Text NingText;                   // 닝닝 text
    GameObject Giselle;                     // 지젤 전체 UI
    GameObject Ning;                        // 닝닝 전체 UI

    float atTime;                           // 블랙맘바 공격 시간
    float soundTime;                        // 블랙맘바 소리 시간
    float textTime;                         // text 활성화 시간
    Vector3 randPos;                        // 블랙맘바 랜덤 이동 값

    AudioSource audioPlayer;                // 블랙맘바 오디오 소스

    void Start()
    {
        hpBMf = 1;      // 블랙맘바 hp 실수값
        audioPlayer = GetComponent<AudioSource>();                // 블랙맘바 오디오 컴포넌트 찾기

        Giselle = GameObject.Find("Canvas").transform.Find("Giselle").gameObject;   // 비활성화 오브젝트 찾기
        Ning = GameObject.Find("Canvas").transform.Find("Ningning").gameObject;   // 비활성화 오브젝트 찾기

        atTime = Random.Range(10.0f, 13.0f);   // 공격 랜덤값 설정
        soundTime = Random.Range(7.0f, 9.0f);   // 소리 랜덤값 설정

        InvokeRepeating("SetPos", 3.0f, 5f);            // 3초 후에 5초마다 실행
        InvokeRepeating("SoundPlay", 2.0f, soundTime);            // 2초 후에 랜덤 시간마다 실행
        InvokeRepeating("SetText", 3.0f, soundTime);            // 3초 후에 랜덤 시간마다 실행
        InvokeRepeating("attackBM", 4.0f, atTime);            // 4초 후에 랜덤 시간마다 실행
    }
    private void Update()       // 매 프레임 호출
    {
        hpBM.fillAmount = hpBMf;   // 블랙맘바 hp ui값 = hp 실수 값
        transform.position = transform.position + randPos;      // 블랙맘바 이동
    }
    
    void SetPos()       // 위치 설정
    {
        randPos.x = Random.Range(-0.003f, 0.003f);          // x축 이동값
        randPos.y = 0;                                      // y축 이동값
        randPos.z = 0;                                      // z축 이동값

        Ning.SetActive(true);                               // 닝닝 UI 활성화
        if (randPos.x < 0)                                  // x축 이동값이 음수
        {
            NingText.text = "왼쪽으로 이동해요!";           // 닝닝 텍스트 변경
        }
        else if (randPos.x > 0)                                  // x축 이동값이 양수
        {
            NingText.text = "오른쪽으로 이동해요!";           // 닝닝 텍스트 변경
        }
        StartCoroutine(closeText(Ning));                    // 함수 비활성화 코루틴 함수 호출           
    }

    void attackBM()                                     // 블랙 맘바 공격
    {
        Shot(new Vector3(1f, 0, 0));                     // 총알 발사
        Shot(new Vector3(-1f, 0, 0));                     // 총알 발사
        Shot(new Vector3(0, 0, 1f));                     // 총알 발사
        Shot(new Vector3(0, 0, -1f));                     // 총알 발사
        Shot(new Vector3(0.5f, 0, 0.5f));                     // 총알 발사
        Shot(new Vector3(0.5f, 0, -0.5f));                     // 총알 발사
        Shot(new Vector3(-0.5f, 0, 0.5f));                     // 총알 발사
        Shot(new Vector3(-0.5f, 0, -0.5f));                     // 총알 발사
    }

    void SetText()              // 지젤 UI 설정
    {
        Giselle.SetActive(true);                                            // 지젤 UI 활성화    
        GiselleText.text = (int)(atTime - soundTime) + "초 후에 총알 조심해!";  // 지젤 텍스트 변경
        StartCoroutine(closeText(Giselle));                                 // 함수 비활성화 코루틴 함수 호출      
    }

    void SoundPlay()                                    // 소리 재생
    {
        audioPlayer.Play();                             // 뱀 소리 재생
    }

    IEnumerator closeText(GameObject UI)                                // 비활성화 함수
    {
        yield return new WaitForSeconds(2.7f);                  // 대기 후
        UI.SetActive(false);                                    // 매개변수로 받은 ui 비활성화
    }

    void Shot(Vector3 dir)              // 총알 발사 함수
    {
        GameObject obj = Instantiate(bulletObj);            // 총알 생성
        Vector3 shotPos = transform.position + transform.up * 1.3f;     // 발사 위치 설정

        obj.GetComponent<BulletMove>().SetPosDir(shotPos, dir);         // BulletMove 스크립트의 SetPosDir 함수에 매개변수 전달
        Destroy(obj, 10);                                           //10 초 후 총알 삭제
    }

    public void OnTriggerEnter(Collider other)  // 충돌 처리
    {
        GameObject Bulletae = GameObject.FindGameObjectWithTag("Bulletae");     // 충돌한 오브젝트의 태그가 사용자 총알인 게임 오브젝트

        if (other.tag == "Bulletae")     // 플레이어 총알과 충돌
        {
            Destroy(Bulletae, 1.0f);              // 사용자 총알 삭제
            hpBMf -= 0.02f;   // hp 감소
            if (ShotBullet.gagef < 1)            // 카리나 게이지가 1보다 작으면
            {
                ShotBullet.gagef += 0.05f;       // 카리나 게이지 증가
            }

            if (hpBM.fillAmount == 0)       // hp가 0이면
            {
                Time.timeScale = 0;             // 일시정지
                if (SceneManager.GetActiveScene().name == "Battle")     // 현재 씬이 Battle 일때만
                {
                    nextImage.SetActive(true);      // nextlevel UI 활성화
                    Time.timeScale = 1;             // 다시 재생
                    SceneManager.LoadScene("BattleS");     // 씬 이동
                }
                else if (SceneManager.GetActiveScene().name == "BattleS")          // 레벨 2
                {
                    Time.timeScale = 1;             // 다시 재생
                    SceneManager.LoadScene("GameClear");     // 게임 클리어
                }
            }
        }
    }
}
