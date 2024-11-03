using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collMonster : MonoBehaviour
{
    public Material aftershot;          // �ǰ� �� ��
    public static bool isDie = false;   // ���� ��� ����

    private cyMonsterCtrl cyMove;
    private cyMonsterBullet cyShot;
    private caMonsterCtrl caMove;
    private caMonsterBullet caShot;
    GameObject thisMon;


    private void Start()
    {
        cyMove = GetComponent<cyMonsterCtrl>();
        cyShot = GetComponent<cyMonsterBullet>();
        caMove = GetComponent<caMonsterCtrl>();
        caShot = GetComponent<caMonsterBullet>();

    }

    public void OnTriggerEnter(Collider other)  // �浹 ó��
    {
        GameObject playerbullet = GameObject.FindGameObjectWithTag("PlayerBullet");

        if (other.tag == "PlayerBullet")                        // ���� & �÷��̾� �Ѿ�
        {
            if (isDie == false)                                 // if(live)
            {
                if (this.tag == "cyMon")
                {
                    isDie = true;                                       // die
                    playerBullet.countKill++;                           // ������ ���� ���� ����
                    GetComponent<Renderer>().material = aftershot;      // ���������� ����
                    cyMove.enabled = false;                             // �̵� ����
                    cyShot.enabled = false;                             // ���� ����
                    Destroy(playerbullet);                              // �÷��̾� �Ѿ� ����
                    Destroy(gameObject, 1.0f);                          // ���� 1�� �� ����
                }
                else if (this.tag == "caMon")
                {
                    isDie = true;                                       // die
                    playerBullet.countKill++;                           // ������ ���� ���� ����
                    GetComponent<Renderer>().material = aftershot;      // ���������� ����
                    caMove.enabled = false;                             // �̵� ����
                    caShot.enabled = false;                             // ���� ����
                    Destroy(playerbullet);                              // �÷��̾� �Ѿ� ����
                    Destroy(gameObject, 1.0f);                          // ���� 1�� �� ����
                }
            }
        }
    }
}
