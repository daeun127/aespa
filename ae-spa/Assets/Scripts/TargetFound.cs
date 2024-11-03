using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFound : MonoBehaviour
{
    // 편의성을 위해 코드 추가: 이미지 타겟을 찾을 때만 오브젝트 active
    public void OnTargetFound()
    {
        gameObject.SetActive(true);
    }

    public void OnTargetLost()
    {
        gameObject.SetActive(false);
    }    
}
