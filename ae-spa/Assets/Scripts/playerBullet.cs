using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBullet : MonoBehaviour
{
    public GameObject bulletObj;    // �Ѿ� ������Ʈ
    public Text hpText;     // hp ui
    public Text killText;  // monster ui
    public Text GameClear;  // ����Ŭ���� ui
    public Text Game0ver;   // ���ӿ��� ui
    public static int countKill = 0;    // ���� ���� Ƚ��

    int countItem = 0;   // ������ ȹ�� Ƚ��
    int hp = 10;                 // �÷��̾� ü�� 10���� ����
    
    void Start()
    {
        Game0ver.enabled = false;   // ui ����
        GameClear.enabled = false;  // ui ����
    }

    void Update()
    {
        hpText.text = hp.ToString();    // ü�� int -> string
        killText.text = countKill.ToString();   // ���� ���� Ƚ�� int -> string

        // �÷��̾� ü�� 0
        if (hp <= 0)    
        {
            Destroy(gameObject);    // �÷��̾� ����
            hp = 0;
            Game0ver.enabled = true;    // Game Over ǥ��
            Time.timeScale = 0;     // ���� ����
        }

        // ���� 10ȸ ����
        if (countKill == 10)
        {
            GameClear.enabled = true;   // Game Clear ǥ��
            Time.timeScale = 0;     // ���� ����
        }

        // ���콺 ���� ��ư Ŭ�� �� �Ѿ� �߻�
        if (Input.GetMouseButtonDown(1))
        {
            Shot(transform.forward);   // �������� �Ѿ� �߻�
            if (countItem == 1)         // ������ 1ȸ ȹ��
            {
                Shot(-transform.forward);   // �Ĺ� �߻� �߰�
            }
            else if (countItem == 2)         // ������ 2ȸ ȹ��
            {
                Shot(-transform.forward);
                Shot(transform.right);      // ��/�� �߻� �߰�
                Shot(-transform.right);
            }
            else if (countItem > 2)         // ������ 2ȸ ȹ��
            {
                Shot(-transform.forward);
                Shot(transform.right);
                Shot(-transform.right);
                Shot(new Vector3(1, 0, 1));     // �밢������ �Ѿ� �߻� �߰�
                Shot(new Vector3(1, 0, -1));
                Shot(new Vector3(-1, 0, 1));
                Shot(new Vector3(-1, 0, -1));
            }
        }
    }

    void Shot(Vector3 dir)  // �Ѿ� �߻� �Լ�
    {
        GameObject obj = Instantiate(bulletObj);    // �Ѿ� ����
        Vector3 shotPos = transform.position + transform.up * 0.05f;    // ��ġ
        obj.GetComponent<BulletMove>().SetPosDir(shotPos, dir);    // ��ũ��Ʈ �ҷ�����
        Destroy(obj, 5);   // 5�� �� ����
    }

    public void OnTriggerEnter(Collider other)  // �浹 ó��
    {
        GameObject cymon = GameObject.FindGameObjectWithTag("cyMon");
        GameObject camon = GameObject.FindGameObjectWithTag("caMon");
        GameObject monsterbullet = GameObject.FindGameObjectWithTag("MonsterBullet");
        GameObject item = GameObject.FindGameObjectWithTag("Item");

        if (other.tag == "cyMon")     // ���� & �÷��̾�
        {
            hp = hp - 2;                // �÷��̾� ü�� ���� -2
            Destroy(cymon, 1.0f);       // ���� ����
        }
        else if (other.tag == "caMon")     // ���� & �÷��̾�
        {
            hp = hp - 2;                // �÷��̾� ü�� ���� -2
            Destroy(camon, 1.0f);       // ���� ����
        }
        else if (other.tag == "MonsterBullet")  // ���� �Ѿ� & �÷��̾�
        {
            Destroy(monsterbullet);         // ���� �Ѿ� ����
            hp--;                           // �÷��̾� ü�� ���� -1
        }
        else if (other.tag == "Item")       // ������ & �÷��̾�
        {
            countItem++;                    // ������ ȹ�� Ƚ�� ����
            Destroy(item);                  // ������ ����
        }
    }
}