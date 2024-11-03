using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCtrl : MonoBehaviour
{
    enum State      // 상태
    {
        Move,       // 이동 상태
        Rotate,     // 회전 상태
        Scale       // 크기 상태
    }

    Vector3 prePos;     // 마우스 위치 변수
    State curState = State.Move;    // 기본 상태 - 이동

    public AudioSource audioSavage;     // 노래 소스
    Animation aniSavage;                // 애니메이션 소스
    Animator aniMusic;                  // 애니메이터 소스

    GameObject title;                   // 노래 제목

    public bool isDetected; // 이미지 타겟 감지
    public Text stateMsg;  // 상태 UI

    private void Start()
    {
        aniSavage = GetComponent<Animation>();              // 춤 애니메이션 찾기
        audioSavage = GetComponent<AudioSource>();          // 노래 소스 찾기

        aniSavage.Play("Idle");                             // 기본 애니메이션 idle 재생
        aniMusic = GameObject.Find("Music").GetComponent<Animator>();       // Music 버튼의 animation 찾기
        title = GameObject.Find("Canvas").transform.Find("Title").gameObject;   // 비활성화 오브젝트 찾기
    }

    void Update()
    {
        if(Input.GetMouseButton(0))     // 마우스 왼쪽 버튼 클릭 시
        {
            Vector3 deltaPos = Input.mousePosition - prePos;    // 현재 마우스 위치 - 이전 마우스 위치 값

            switch (curState)   // 현재 상태에 따라 달라짐
            {
                case State.Move:                                                    // 현재 상태 - 이동일 때
                    deltaPos *= (Time.deltaTime * 0.1f);                            // 위치 이동되는 게 보이도록 곱함.
                    transform.Translate(deltaPos.x, deltaPos.y, 0, Space.World);    // x, y축 이동
                    break;                                                          // switch문 나오기

                case State.Rotate:                                                    // 현재 상태 - 회전일 때
                    deltaPos *= (Time.deltaTime * 10f);                               // 회전이 보이도록 곱함.
                    transform.Rotate(0, deltaPos.x, 0, Space.World);                  // y축 기준으로 마우스 가로 이동 만큼 회전
                    break;                                                          // switch문 나오기

                case State.Scale:                                                    // 현재 상태 - 크기일 때
                    deltaPos *= (Time.deltaTime * 0.1f);                               // 크기가 보이도록 곱함.
                    transform.localScale += new Vector3(deltaPos.x, deltaPos.x, deltaPos.x);    // 기존 크기에 마우스 가로 이동 만큼 일정 비율로 커짐
                    break;                                                          // switch문 나오기
            }
        }

        prePos = Input.mousePosition;       // 이전 마우스 위치 설정

    }

    public void OnclickMove()       // Move 버튼 클릭 시
    {
        curState = State.Move;      // 상태 Move 로 변경
    }

    public void OnclickRotate()       // Rotate 버튼 클릭 시
    {
        curState = State.Rotate;      // 상태 Rotate 로 변경
    }

    public void OnclickScale()       // Scale 버튼 클릭 시
    {
        curState = State.Scale;      // 상태 Scale 로 변경
    }

    public void OnclickMusic()       // Music 버튼 클릭 시
    {
        StartCoroutine(AniCtrl());  // 애니메이션 실행하는 코루틴 함수 실행
    }
    
    IEnumerator AniCtrl()               // 코루틴 함수 실행
    {
        float aniLen = PlayAni("Savage");       // 실질적 애니메이션 재생 함수 호출하여 애니메이션 길이 실수 값 저장

        yield return new WaitForSeconds(aniLen);    // 애니메이션 길이동안 대기

        PlayAni("Idle");        // 대기 후 기본 애니메이션 idle 재생
    }

    public float PlayAni(string ani)                // 애니메이션 재생 함수
    {
        aniSavage.Play(ani);                // 재생

        if (aniSavage.IsPlaying("Savage"))      // 애니메이션 플레이 중일 동안
        {
            audioSavage.Play();             // 노래 재생
            if (audioSavage.isPlaying)      // 노래 재생 중일 동안
            {
                title.SetActive(true);      // 노래 제목 활성화
                aniMusic.SetTrigger("Onclick");     // 애니메이션 트리거 onclick으로 설정
            }
        }
        else                                // 애니메이션 끝나면
        {
            title.SetActive(false);         // 노래 제목 비활성화
            aniMusic.SetTrigger("End");     // 애니메이션 트리거 end로 설정
        }

        return aniSavage.GetClip(ani).length;   // 애니메이션 길이 실수 값  반환
    }

    public void OnDetect(bool detect)           // 이미지 파일 감지 함수
    {
        isDetected = detect;                    // 매개변수 값 대입하여 판단

        if (detect)                  // 이미지 타겟 감지되면
        {
            stateMsg.enabled = false;       // text UI 끔
        }
        else                   // 이미지 타겟 감지 안되면
        {
            stateMsg.enabled = true;       // text UI 켬
        }
    }
}
