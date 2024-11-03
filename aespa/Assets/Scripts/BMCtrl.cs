using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BMCtrl : MonoBehaviour
{
    public GameObject nextImage;            // nextlevel UI
    public GameObject bulletObj;            // �Ѿ� ������Ʈ
    public Image hpBM;                      // ������ hp UI
    static public float hpBMf;                   // ������ hp �Ǽ�

    public Text GiselleText;                // ���� text
    public Text NingText;                   // �״� text
    GameObject Giselle;                     // ���� ��ü UI
    GameObject Ning;                        // �״� ��ü UI

    float atTime;                           // ������ ���� �ð�
    float soundTime;                        // ������ �Ҹ� �ð�
    float textTime;                         // text Ȱ��ȭ �ð�
    Vector3 randPos;                        // ������ ���� �̵� ��

    AudioSource audioPlayer;                // ������ ����� �ҽ�

    void Start()
    {
        hpBMf = 1;      // ������ hp �Ǽ���
        audioPlayer = GetComponent<AudioSource>();                // ������ ����� ������Ʈ ã��

        Giselle = GameObject.Find("Canvas").transform.Find("Giselle").gameObject;   // ��Ȱ��ȭ ������Ʈ ã��
        Ning = GameObject.Find("Canvas").transform.Find("Ningning").gameObject;   // ��Ȱ��ȭ ������Ʈ ã��

        atTime = Random.Range(10.0f, 13.0f);   // ���� ������ ����
        soundTime = Random.Range(7.0f, 9.0f);   // �Ҹ� ������ ����

        InvokeRepeating("SetPos", 3.0f, 5f);            // 3�� �Ŀ� 5�ʸ��� ����
        InvokeRepeating("SoundPlay", 2.0f, soundTime);            // 2�� �Ŀ� ���� �ð����� ����
        InvokeRepeating("SetText", 3.0f, soundTime);            // 3�� �Ŀ� ���� �ð����� ����
        InvokeRepeating("attackBM", 4.0f, atTime);            // 4�� �Ŀ� ���� �ð����� ����
    }
    private void Update()       // �� ������ ȣ��
    {
        hpBM.fillAmount = hpBMf;   // ������ hp ui�� = hp �Ǽ� ��
        transform.position = transform.position + randPos;      // ������ �̵�
    }
    
    void SetPos()       // ��ġ ����
    {
        randPos.x = Random.Range(-0.003f, 0.003f);          // x�� �̵���
        randPos.y = 0;                                      // y�� �̵���
        randPos.z = 0;                                      // z�� �̵���

        Ning.SetActive(true);                               // �״� UI Ȱ��ȭ
        if (randPos.x < 0)                                  // x�� �̵����� ����
        {
            NingText.text = "�������� �̵��ؿ�!";           // �״� �ؽ�Ʈ ����
        }
        else if (randPos.x > 0)                                  // x�� �̵����� ���
        {
            NingText.text = "���������� �̵��ؿ�!";           // �״� �ؽ�Ʈ ����
        }
        StartCoroutine(closeText(Ning));                    // �Լ� ��Ȱ��ȭ �ڷ�ƾ �Լ� ȣ��           
    }

    void attackBM()                                     // �� ���� ����
    {
        Shot(new Vector3(1f, 0, 0));                     // �Ѿ� �߻�
        Shot(new Vector3(-1f, 0, 0));                     // �Ѿ� �߻�
        Shot(new Vector3(0, 0, 1f));                     // �Ѿ� �߻�
        Shot(new Vector3(0, 0, -1f));                     // �Ѿ� �߻�
        Shot(new Vector3(0.5f, 0, 0.5f));                     // �Ѿ� �߻�
        Shot(new Vector3(0.5f, 0, -0.5f));                     // �Ѿ� �߻�
        Shot(new Vector3(-0.5f, 0, 0.5f));                     // �Ѿ� �߻�
        Shot(new Vector3(-0.5f, 0, -0.5f));                     // �Ѿ� �߻�
    }

    void SetText()              // ���� UI ����
    {
        Giselle.SetActive(true);                                            // ���� UI Ȱ��ȭ    
        GiselleText.text = (int)(atTime - soundTime) + "�� �Ŀ� �Ѿ� ������!";  // ���� �ؽ�Ʈ ����
        StartCoroutine(closeText(Giselle));                                 // �Լ� ��Ȱ��ȭ �ڷ�ƾ �Լ� ȣ��      
    }

    void SoundPlay()                                    // �Ҹ� ���
    {
        audioPlayer.Play();                             // �� �Ҹ� ���
    }

    IEnumerator closeText(GameObject UI)                                // ��Ȱ��ȭ �Լ�
    {
        yield return new WaitForSeconds(2.7f);                  // ��� ��
        UI.SetActive(false);                                    // �Ű������� ���� ui ��Ȱ��ȭ
    }

    void Shot(Vector3 dir)              // �Ѿ� �߻� �Լ�
    {
        GameObject obj = Instantiate(bulletObj);            // �Ѿ� ����
        Vector3 shotPos = transform.position + transform.up * 1.3f;     // �߻� ��ġ ����

        obj.GetComponent<BulletMove>().SetPosDir(shotPos, dir);         // BulletMove ��ũ��Ʈ�� SetPosDir �Լ��� �Ű����� ����
        Destroy(obj, 10);                                           //10 �� �� �Ѿ� ����
    }

    public void OnTriggerEnter(Collider other)  // �浹 ó��
    {
        GameObject Bulletae = GameObject.FindGameObjectWithTag("Bulletae");     // �浹�� ������Ʈ�� �±װ� ����� �Ѿ��� ���� ������Ʈ

        if (other.tag == "Bulletae")     // �÷��̾� �Ѿ˰� �浹
        {
            Destroy(Bulletae, 1.0f);              // ����� �Ѿ� ����
            hpBMf -= 0.02f;   // hp ����
            if (ShotBullet.gagef < 1)            // ī���� �������� 1���� ������
            {
                ShotBullet.gagef += 0.05f;       // ī���� ������ ����
            }

            if (hpBM.fillAmount == 0)       // hp�� 0�̸�
            {
                Time.timeScale = 0;             // �Ͻ�����
                if (SceneManager.GetActiveScene().name == "Battle")     // ���� ���� Battle �϶���
                {
                    nextImage.SetActive(true);      // nextlevel UI Ȱ��ȭ
                    Time.timeScale = 1;             // �ٽ� ���
                    SceneManager.LoadScene("BattleS");     // �� �̵�
                }
                else if (SceneManager.GetActiveScene().name == "BattleS")          // ���� 2
                {
                    Time.timeScale = 1;             // �ٽ� ���
                    SceneManager.LoadScene("GameClear");     // ���� Ŭ����
                }
            }
        }
    }
}
