using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void OnClickHow()                //  ��ư Ŭ�� ��
    {
        SceneManager.LoadScene("HowtoPlay");    // how to play �޴���
    }

    public void OnClickBattle()                //  ��ư Ŭ�� ��
    {
        SceneManager.LoadScene("Battle");       // battle �޴���
    }

    public void OnClickDance()                //  ��ư Ŭ�� ��
    {
       SceneManager.LoadScene("Savage");        // dance �޴���
    }

    public void OnClickBack()                //  ��ư Ŭ�� ��
    {
        SceneManager.LoadScene("Start");     // ����ȭ������
    }
}
