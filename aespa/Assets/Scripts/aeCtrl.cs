using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aeCtrl : MonoBehaviour
{
    public Text stateMsg;  // ���� UI

    public bool isDetected; // �̹��� Ÿ�� ����
    public Transform camTr; // AR ī�޶�

    public GameObject BM;   // ������ ������Ʈ

    GameObject armPanel;    // ���� ���� �г�
    GameObject Karina;      // ī���� ������
    GameObject BulletPanel; // �Ѿ� ���� UI
    GameObject HPae;        // ����� hp UI
    GameObject UIgun1;      // ��1 UI
    GameObject UIgun2;      // ��2 UI

    AudioSource audioPlayer;    // ����� �ҽ�

    private void Start()
    {
        StopAllCoroutines();    // ������ �� ��� �ڷ�ƾ �ߴ�

        armPanel = GameObject.Find("Canvas").transform.Find("Armament").gameObject;         // ��Ȱ��ȭ ������Ʈ ã��
        Karina = GameObject.Find("Canvas").transform.Find("Karina").gameObject;         // ��Ȱ��ȭ ������Ʈ ã��
        BulletPanel = GameObject.Find("Canvas").transform.Find("BulletPanel").gameObject;         // ��Ȱ��ȭ ������Ʈ ã��
        HPae = GameObject.Find("Canvas").transform.Find("HPae").gameObject;         // ��Ȱ��ȭ ������Ʈ ã��
        UIgun1 = GameObject.Find("Canvas").transform.Find("ShotGun1").gameObject;         // ��Ȱ��ȭ ������Ʈ ã��
        UIgun2 = GameObject.Find("Canvas").transform.Find("ShotGun2").gameObject;         // ��Ȱ��ȭ ������Ʈ ã��

        audioPlayer = GetComponent<AudioSource>();      // ����� ������Ʈ ã��
    }

    void Update()
    {
        if (ArmCtrl.selectbool == true && ArmCtrl.deselectbool == true)     // ���õ� �Ѱ� ���õ��� ���� ���� �����Ǹ�
        {
            Invoke("SetUI", 2.5f);  // UI ���� �Լ� ȣ��
        }
    }

    void SetUI()        // �⺻ UI ���� �Լ�
    {
        ArmCtrl.selectbool = false;         // ���õ� �� ��� - update �Լ����� ��� �ҷ����� �ʱ� ����.
        ArmCtrl.deselectbool = false;       // �̼��� �� ���

        if (ArmCtrl.selectOb.name == "Gun1") UIgun1.SetActive(true);            // ���õ� ���� �̸��� ��1�� �� ��1 Ȱ��ȭ
        if (ArmCtrl.selectOb.name == "Gun2") UIgun2.SetActive(true);            // ���õ� ���� �̸��� ��2�� �� ��2 Ȱ��ȭ

        transform.position = camTr.transform.position - new Vector3(0, 4, 0.5f);    // ����� ��ġ �� ī�޶� �Ʒ��� �̵�
        transform.rotation = Quaternion.Euler(0, 0, 0);                         // ������ ������ �ٶ󺸰�

        BM.SetActive(true);     // ������ Ȱ��ȭ
        BM.transform.position = new Vector3(BM.transform.position.x, camTr.transform.position.y-3, BM.transform.position.z);    // ������ ��ġ�� ����

        Karina.SetActive(true);     // ī���� ������ Ȱ��ȭ
        BulletPanel.SetActive(true);    // �Ѿ� Ȱ��ȭ
        HPae.SetActive(true);           // ����� hp Ȱ��ȭ

        armPanel.SetActive(false);      // ���� ���� �г� ��Ȱ��ȭ
        stateMsg.enabled = false;       // ���� text ��Ȱ��ȭ
    }

    public void OnDetect(bool detect)           // �̹��� ���� ���� �Լ�
    {
        isDetected = detect;                    // �Ű����� �� �����Ͽ� �Ǵ�

        if (detect)                  // �̹��� Ÿ�� �����Ǹ�
        {
            audioPlayer.Play(); // ���� ���

            armPanel.SetActive(true);      // ���� ���� �г� Ȱ��ȭ
            stateMsg.text = "ae-Winter�� ���⸦ �������ּ���";    // ���� text ����
        }
        else
        {
            audioPlayer.Stop(); // ���� ����

            armPanel.SetActive(false);      // ���� ���� �г� ��Ȱ��ȭ
            stateMsg.text = "ae ī�带 �νĽ����ּ���";    // ���� text ����
        }
    }
}
