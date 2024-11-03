using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ShotBullet : MonoBehaviour
{
    public GameObject bulletObj;            // �Ѿ� ������Ʈ
    public GameObject BM;                   // �� ���� ������Ʈ
    public GameObject[] UIbullets;          // �Ѿ� UI ������Ʈ

    static public float gagef;            // ī���� ������ �Ǽ�
    public float dehpf;                     // ����� HP ���� ��
    public Image gage;                      // ī���� ������ �̹��� UI
    public Image hpae;                      // ����� HP �̹��� UI
    float hpaef = 1;                        // ����� HP �Ǽ�
    public Text stateMsg;                   // ���� UI

    public ParticleSystem KarinaEff;        // ī���� �Ѿ� ȿ��
    public GameObject KarinaBtn;            // ī���� ��ư
    int curBullet = 0;               // ���� �Ѿ� �߻� Ƚ�� == 0
    float time = 0;                      // �Ѿ� ����� �ð�   

    public AudioSource effSound;                // ����Ʈ ����� �ҽ�

    private void Start()        // ���� �Լ�
    {
        gagef = 0;      // ī���� ������ �� 0
    }

    void Update()          // �� ������ ȣ��
    {
        if (gage.enabled == true)                   // ui�� Ȱ��ȭ �Ǿ��ִ� ����
        {
            gage.fillAmount = gagef;                  // ī���� ������ �Ǽ� �� �ݿ�
        }
        if (hpae.enabled == true)                   // ui�� Ȱ��ȭ �Ǿ��ִ� ����
        {
            hpae.fillAmount = hpaef;                  // hp  �Ǽ� �� �ݿ�
        }

        if (UIbullets[UIbullets.Length-1].activeSelf)           // ������ �Ѿ��� �������� �� == �Ѿ� �������� ��
        {
            if (Input.GetMouseButtonDown(1))        // ���콺 ���� ��ư Ŭ�� ��
            {
                if (ArmCtrl.selectOb.name == "Gun1")            // ���� �гο��� ������ ���� ��1
                {
                    Shot1(Vector3.forward);                         // �Ѿ� �߻�
                    Shot1(new Vector3(-0.2f, 0, 1));                // �Ѿ� �߻�
                    Shot1(new Vector3(0.2f, 0, 1));                 // �Ѿ� �߻�
                }
                else if (ArmCtrl.selectOb.name == "Gun2")           // ���� �гο��� ������ ���� ��2
                {

                    StartCoroutine(Shot2(Vector3.forward));            // �Ѿ� �߻�
                    StartCoroutine(Shot2(new Vector3(0, 0.08f, 1)));       // �Ѿ� �߻�
                    StartCoroutine(Shot2(new Vector3(0, 0.14f, 1)));       // �Ѿ� �߻�
                }
                UIbullets[curBullet].SetActive(false);                          // ���� �Ѿ� UI ��Ȱ��ȭ
                curBullet++;                                                    // �Ѿ� �߻� Ƚ�� +1
            }
        }
        else                                                        // ������ �Ѿ��� �������� �� == �Ѿ� ���� ��
        {
            stateMsg.enabled = true;                                     // ���� text Ȱ��ȭ
            stateMsg.text = "�Ѿ� ������ " + (5-(int)time) + "�� ��";          // ���� text ����
            time += Time.deltaTime;

            if (time >= 5)            // 5�� �̻� �帣��
            {
                stateMsg.enabled = false;                                    // ���� text ��Ȱ��ȭ
                for (int i = 0; i < UIbullets.Length; i++)     // ��� �Ѿ� UI
                    UIbullets[i].SetActive(true);                    // �Ѿ� UI �ѱ�

                curBullet = 0;                                               // ���� �Ѿ� 0���� ����
                time = 0;                         // �Ѿ� ����� �ð� �ʱ�ȭ
            }
        }
    }

    public void OnClickKarina()         // ī���� ��ư Ŭ�� ��
    {
        if (gage.fillAmount == 1)           // ī���� �������� 1�̸�
        {
            curBullet = 0;                  // ���� �Ѿ� 0
            for (int i = curBullet; i < UIbullets.Length; i++)      // ��� �Ѿ� UI
            {
                UIbullets[i].SetActive(true);                   // �Ѿ� UI �ѱ�
            }

            ParticleSystem ps = Instantiate(KarinaEff);              //  ȿ�� ����
            ps.transform.position = BM.transform.position;              // ȿ�� ��ġ = ������ ��ġ
            effSound.Play();                                        // ȿ�� �Ҹ� �÷���

            BMCtrl.hpBMf = BMCtrl.hpBMf / 2;                   // ������ hp ����
            gagef = 0;                                          // ī���� ������ �Ǽ��� 0

        }
    }

    void Shot1(Vector3 dir)              // ��1 �Ѿ� �߻� �Լ�
    {
        GameObject obj = Instantiate(bulletObj);            // �Ѿ� ����
        Vector3 shotPos = transform.position + transform.up * 1.0f;     // �߻� ��ġ ����

        obj.GetComponent<BulletMove>().SetPosDir(shotPos, dir);         // BulletMove ��ũ��Ʈ�� SetPosDir �Լ��� �Ű����� ����
        Destroy(obj, 10);                                           //10 �� �� �Ѿ� ����
    }

    IEnumerator Shot2(Vector3 dir)              // ��2 �Ѿ� �߻� �Լ�
    {
        GameObject obj = Instantiate(bulletObj);            // �Ѿ� ����
        Vector3 shotPos = transform.position + transform.up * 1.0f;     // �߻� ��ġ ����

        obj.GetComponent<BulletMove>().SetPosDir(shotPos, dir);         // BulletMove ��ũ��Ʈ�� SetPosDir �Լ��� �Ű����� ����
        Destroy(obj, 10);                                           //10 �� �� �Ѿ� ����

        yield return new WaitForSeconds(1f);            // 1�� ���
    }

    public void OnTriggerEnter(Collider other)  // �浹 ó��
    {
        GameObject BulletBM = GameObject.FindGameObjectWithTag("BulletBM"); // �浹�� ������Ʈ�� �±װ� ������ �Ѿ��� ���� ������Ʈ

        if (other.tag == "BulletBM")     // ���� �Ѿ� & �÷��̾�
        {
            hpaef -= dehpf;       // ����� hp ����
            Destroy(BulletBM, 1.0f);       // ���� �Ѿ� ����

            if (hpae.fillAmount == 0)       // ����� hp�� 0�� ��
            {
                SceneManager.LoadScene("GameOver");     // GameOver �̵�
            }
        }
    }
}
