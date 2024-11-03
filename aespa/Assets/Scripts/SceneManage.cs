using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void OnClickHow()                //  버튼 클릭 시
    {
        SceneManager.LoadScene("HowtoPlay");    // how to play 메뉴로
    }

    public void OnClickBattle()                //  버튼 클릭 시
    {
        SceneManager.LoadScene("Battle");       // battle 메뉴로
    }

    public void OnClickDance()                //  버튼 클릭 시
    {
       SceneManager.LoadScene("Savage");        // dance 메뉴로
    }

    public void OnClickBack()                //  버튼 클릭 시
    {
        SceneManager.LoadScene("Start");     // 시작화면으로
    }
}
