using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ArmCtrl : MonoBehaviour
{
    static public GameObject selectOb;   // 선택 총
    static public GameObject deselectOb; // 미선택 총

    static public bool selectbool = false;      // 선택 여부 판단
    static public bool deselectbool = false;    // 미선택 여부 판단

    public Text stateMsg;  // 상태 UI
    public GameObject gun1;                     // 총1
    public GameObject gun2;                     // 총2

    private void Start()            // 시작 시 호출
    {
        selectOb = null;            // 선택된 오브젝트 없음
        deselectOb = null;          // 미선택된 오브젝트 없음
    }

    public void OnclickArm()        // 무기 선택
    {
        if ( EventSystem.current.currentSelectedGameObject == gun1)     // 방금 선택한 게임 오브젝트가 == 총1이면
        {
            selectOb = gun1;                          // 선택 게임오브젝트 총1 대입
            deselectOb = gun2;                      // 미선택 게임오브젝트 총2 대입
        }
        else if (EventSystem.current.currentSelectedGameObject == gun2)     // 방금 선택한 게임 오브젝트가 == 총2이면
        {

            selectOb = gun2;                            // 선택 게임오브젝트 총2 대입
            deselectOb = gun1;                        // 미선택 게임오브젝트 총1 대입

        }

        selectbool = true;                  // 선택 완료
        deselectbool = true;                // 미선택 완료

        Invoke("Select", 0.5f);             // 대기 후 함수 호출
    }

    public void Select()                // 선택된 후 UI효과   
    {
        stateMsg.text = "ae-Winter의 무기가 선택되었습니다";      // 상태 text 변경

        selectOb.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);      // 선택된 무기 크기 증가
        Invoke("SelectEffectF", 0.2f);          // 대기 후 함수 호출
    }

    public void SelectEffectF()             // 코루틴 함수 호출 함수
    {
        StartCoroutine(SelectEffectT(selectOb));    // 코루틴 함수 호출
    }

    IEnumerator SelectEffectT(GameObject selectOb)      // 선택됐을 때 효과
    {
        selectOb.gameObject.SetActive(false);           // 게임오브젝트 끔
        yield return new WaitForSeconds(0.2f);          // 0.2초 대기
        selectOb.gameObject.SetActive(true);           // 게임오브젝트 켬
        yield return new WaitForSeconds(0.2f);          // 0.2초 대기
        selectOb.gameObject.SetActive(false);           // 게임오브젝트 끔
        yield return new WaitForSeconds(0.2f);          // 0.2초 대기
        selectOb.gameObject.SetActive(true);           // 게임오브젝트 켬
        yield return new WaitForSeconds(0.2f);          // 0.2초 대기
    }

}
