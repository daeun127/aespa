using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ArmCtrl : MonoBehaviour
{
    static public GameObject selectOb;   // ���� ��
    static public GameObject deselectOb; // �̼��� ��

    static public bool selectbool = false;      // ���� ���� �Ǵ�
    static public bool deselectbool = false;    // �̼��� ���� �Ǵ�

    public Text stateMsg;  // ���� UI
    public GameObject gun1;                     // ��1
    public GameObject gun2;                     // ��2

    private void Start()            // ���� �� ȣ��
    {
        selectOb = null;            // ���õ� ������Ʈ ����
        deselectOb = null;          // �̼��õ� ������Ʈ ����
    }

    public void OnclickArm()        // ���� ����
    {
        if ( EventSystem.current.currentSelectedGameObject == gun1)     // ��� ������ ���� ������Ʈ�� == ��1�̸�
        {
            selectOb = gun1;                          // ���� ���ӿ�����Ʈ ��1 ����
            deselectOb = gun2;                      // �̼��� ���ӿ�����Ʈ ��2 ����
        }
        else if (EventSystem.current.currentSelectedGameObject == gun2)     // ��� ������ ���� ������Ʈ�� == ��2�̸�
        {

            selectOb = gun2;                            // ���� ���ӿ�����Ʈ ��2 ����
            deselectOb = gun1;                        // �̼��� ���ӿ�����Ʈ ��1 ����

        }

        selectbool = true;                  // ���� �Ϸ�
        deselectbool = true;                // �̼��� �Ϸ�

        Invoke("Select", 0.5f);             // ��� �� �Լ� ȣ��
    }

    public void Select()                // ���õ� �� UIȿ��   
    {
        stateMsg.text = "ae-Winter�� ���Ⱑ ���õǾ����ϴ�";      // ���� text ����

        selectOb.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);      // ���õ� ���� ũ�� ����
        Invoke("SelectEffectF", 0.2f);          // ��� �� �Լ� ȣ��
    }

    public void SelectEffectF()             // �ڷ�ƾ �Լ� ȣ�� �Լ�
    {
        StartCoroutine(SelectEffectT(selectOb));    // �ڷ�ƾ �Լ� ȣ��
    }

    IEnumerator SelectEffectT(GameObject selectOb)      // ���õ��� �� ȿ��
    {
        selectOb.gameObject.SetActive(false);           // ���ӿ�����Ʈ ��
        yield return new WaitForSeconds(0.2f);          // 0.2�� ���
        selectOb.gameObject.SetActive(true);           // ���ӿ�����Ʈ ��
        yield return new WaitForSeconds(0.2f);          // 0.2�� ���
        selectOb.gameObject.SetActive(false);           // ���ӿ�����Ʈ ��
        yield return new WaitForSeconds(0.2f);          // 0.2�� ���
        selectOb.gameObject.SetActive(true);           // ���ӿ�����Ʈ ��
        yield return new WaitForSeconds(0.2f);          // 0.2�� ���
    }

}
