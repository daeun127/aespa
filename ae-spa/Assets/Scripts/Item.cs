using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 50, 0, Space.World);        // ���ڸ����� y�� ���� ȸ��
    }
}
