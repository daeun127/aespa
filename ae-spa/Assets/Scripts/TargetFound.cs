using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFound : MonoBehaviour
{
    // ���Ǽ��� ���� �ڵ� �߰�: �̹��� Ÿ���� ã�� ���� ������Ʈ active
    public void OnTargetFound()
    {
        gameObject.SetActive(true);
    }

    public void OnTargetLost()
    {
        gameObject.SetActive(false);
    }    
}
