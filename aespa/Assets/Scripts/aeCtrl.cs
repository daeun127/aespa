using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aeCtrl : MonoBehaviour
{
    public Text stateMsg;  // 상태 UI

    public bool isDetected; // 이미지 타겟 감지
    public Transform camTr; // AR 카메라

    public GameObject BM;   // 블랙맘바 오브젝트

    GameObject armPanel;    // 무기 선택 패널
    GameObject Karina;      // 카리나 아이템
    GameObject BulletPanel; // 총알 개수 UI
    GameObject HPae;        // 사용자 hp UI
    GameObject UIgun1;      // 총1 UI
    GameObject UIgun2;      // 총2 UI

    AudioSource audioPlayer;    // 오디오 소스

    private void Start()
    {
        StopAllCoroutines();    // 시작할 때 모든 코루틴 중단

        armPanel = GameObject.Find("Canvas").transform.Find("Armament").gameObject;         // 비활성화 오브젝트 찾기
        Karina = GameObject.Find("Canvas").transform.Find("Karina").gameObject;         // 비활성화 오브젝트 찾기
        BulletPanel = GameObject.Find("Canvas").transform.Find("BulletPanel").gameObject;         // 비활성화 오브젝트 찾기
        HPae = GameObject.Find("Canvas").transform.Find("HPae").gameObject;         // 비활성화 오브젝트 찾기
        UIgun1 = GameObject.Find("Canvas").transform.Find("ShotGun1").gameObject;         // 비활성화 오브젝트 찾기
        UIgun2 = GameObject.Find("Canvas").transform.Find("ShotGun2").gameObject;         // 비활성화 오브젝트 찾기

        audioPlayer = GetComponent<AudioSource>();      // 오디오 컴포넌트 찾기
    }

    void Update()
    {
        if (ArmCtrl.selectbool == true && ArmCtrl.deselectbool == true)     // 선택된 총과 선택되지 않은 총이 결정되면
        {
            Invoke("SetUI", 2.5f);  // UI 세팅 함수 호출
        }
    }

    void SetUI()        // 기본 UI 세팅 함수
    {
        ArmCtrl.selectbool = false;         // 선택된 총 취소 - update 함수에서 계속 불러오지 않기 위함.
        ArmCtrl.deselectbool = false;       // 미선택 총 취소

        if (ArmCtrl.selectOb.name == "Gun1") UIgun1.SetActive(true);            // 선택된 총의 이름이 총1일 때 총1 활성화
        if (ArmCtrl.selectOb.name == "Gun2") UIgun2.SetActive(true);            // 선택된 총의 이름이 총2일 때 총2 활성화

        transform.position = camTr.transform.position - new Vector3(0, 4, 0.5f);    // 사용자 위치 는 카메라 아래로 이동
        transform.rotation = Quaternion.Euler(0, 0, 0);                         // 각도를 블랙맘바 바라보게

        BM.SetActive(true);     // 블랙맘바 활성화
        BM.transform.position = new Vector3(BM.transform.position.x, camTr.transform.position.y-3, BM.transform.position.z);    // 블랙맘바 위치값 조절

        Karina.SetActive(true);     // 카리나 아이템 활성화
        BulletPanel.SetActive(true);    // 총알 활성화
        HPae.SetActive(true);           // 사용자 hp 활성화

        armPanel.SetActive(false);      // 무기 선택 패널 비활성화
        stateMsg.enabled = false;       // 상태 text 비활성화
    }

    public void OnDetect(bool detect)           // 이미지 파일 감지 함수
    {
        isDetected = detect;                    // 매개변수 값 대입하여 판단

        if (detect)                  // 이미지 타겟 감지되면
        {
            audioPlayer.Play(); // 음악 재생

            armPanel.SetActive(true);      // 무기 선택 패널 활성화
            stateMsg.text = "ae-Winter의 무기를 선택해주세요";    // 상태 text 변경
        }
        else
        {
            audioPlayer.Stop(); // 음악 멈춤

            armPanel.SetActive(false);      // 무기 선택 패널 비활성화
            stateMsg.text = "ae 카드를 인식시켜주세요";    // 상태 text 변경
        }
    }
}
