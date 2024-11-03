using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCtrl : MonoBehaviour
{
    enum State      // ����
    {
        Move,       // �̵� ����
        Rotate,     // ȸ�� ����
        Scale       // ũ�� ����
    }

    Vector3 prePos;     // ���콺 ��ġ ����
    State curState = State.Move;    // �⺻ ���� - �̵�

    public AudioSource audioSavage;     // �뷡 �ҽ�
    Animation aniSavage;                // �ִϸ��̼� �ҽ�
    Animator aniMusic;                  // �ִϸ����� �ҽ�

    GameObject title;                   // �뷡 ����

    public bool isDetected; // �̹��� Ÿ�� ����
    public Text stateMsg;  // ���� UI

    private void Start()
    {
        aniSavage = GetComponent<Animation>();              // �� �ִϸ��̼� ã��
        audioSavage = GetComponent<AudioSource>();          // �뷡 �ҽ� ã��

        aniSavage.Play("Idle");                             // �⺻ �ִϸ��̼� idle ���
        aniMusic = GameObject.Find("Music").GetComponent<Animator>();       // Music ��ư�� animation ã��
        title = GameObject.Find("Canvas").transform.Find("Title").gameObject;   // ��Ȱ��ȭ ������Ʈ ã��
    }

    void Update()
    {
        if(Input.GetMouseButton(0))     // ���콺 ���� ��ư Ŭ�� ��
        {
            Vector3 deltaPos = Input.mousePosition - prePos;    // ���� ���콺 ��ġ - ���� ���콺 ��ġ ��

            switch (curState)   // ���� ���¿� ���� �޶���
            {
                case State.Move:                                                    // ���� ���� - �̵��� ��
                    deltaPos *= (Time.deltaTime * 0.1f);                            // ��ġ �̵��Ǵ� �� ���̵��� ����.
                    transform.Translate(deltaPos.x, deltaPos.y, 0, Space.World);    // x, y�� �̵�
                    break;                                                          // switch�� ������

                case State.Rotate:                                                    // ���� ���� - ȸ���� ��
                    deltaPos *= (Time.deltaTime * 10f);                               // ȸ���� ���̵��� ����.
                    transform.Rotate(0, deltaPos.x, 0, Space.World);                  // y�� �������� ���콺 ���� �̵� ��ŭ ȸ��
                    break;                                                          // switch�� ������

                case State.Scale:                                                    // ���� ���� - ũ���� ��
                    deltaPos *= (Time.deltaTime * 0.1f);                               // ũ�Ⱑ ���̵��� ����.
                    transform.localScale += new Vector3(deltaPos.x, deltaPos.x, deltaPos.x);    // ���� ũ�⿡ ���콺 ���� �̵� ��ŭ ���� ������ Ŀ��
                    break;                                                          // switch�� ������
            }
        }

        prePos = Input.mousePosition;       // ���� ���콺 ��ġ ����

    }

    public void OnclickMove()       // Move ��ư Ŭ�� ��
    {
        curState = State.Move;      // ���� Move �� ����
    }

    public void OnclickRotate()       // Rotate ��ư Ŭ�� ��
    {
        curState = State.Rotate;      // ���� Rotate �� ����
    }

    public void OnclickScale()       // Scale ��ư Ŭ�� ��
    {
        curState = State.Scale;      // ���� Scale �� ����
    }

    public void OnclickMusic()       // Music ��ư Ŭ�� ��
    {
        StartCoroutine(AniCtrl());  // �ִϸ��̼� �����ϴ� �ڷ�ƾ �Լ� ����
    }
    
    IEnumerator AniCtrl()               // �ڷ�ƾ �Լ� ����
    {
        float aniLen = PlayAni("Savage");       // ������ �ִϸ��̼� ��� �Լ� ȣ���Ͽ� �ִϸ��̼� ���� �Ǽ� �� ����

        yield return new WaitForSeconds(aniLen);    // �ִϸ��̼� ���̵��� ���

        PlayAni("Idle");        // ��� �� �⺻ �ִϸ��̼� idle ���
    }

    public float PlayAni(string ani)                // �ִϸ��̼� ��� �Լ�
    {
        aniSavage.Play(ani);                // ���

        if (aniSavage.IsPlaying("Savage"))      // �ִϸ��̼� �÷��� ���� ����
        {
            audioSavage.Play();             // �뷡 ���
            if (audioSavage.isPlaying)      // �뷡 ��� ���� ����
            {
                title.SetActive(true);      // �뷡 ���� Ȱ��ȭ
                aniMusic.SetTrigger("Onclick");     // �ִϸ��̼� Ʈ���� onclick���� ����
            }
        }
        else                                // �ִϸ��̼� ������
        {
            title.SetActive(false);         // �뷡 ���� ��Ȱ��ȭ
            aniMusic.SetTrigger("End");     // �ִϸ��̼� Ʈ���� end�� ����
        }

        return aniSavage.GetClip(ani).length;   // �ִϸ��̼� ���� �Ǽ� ��  ��ȯ
    }

    public void OnDetect(bool detect)           // �̹��� ���� ���� �Լ�
    {
        isDetected = detect;                    // �Ű����� �� �����Ͽ� �Ǵ�

        if (detect)                  // �̹��� Ÿ�� �����Ǹ�
        {
            stateMsg.enabled = false;       // text UI ��
        }
        else                   // �̹��� Ÿ�� ���� �ȵǸ�
        {
            stateMsg.enabled = true;       // text UI ��
        }
    }
}
