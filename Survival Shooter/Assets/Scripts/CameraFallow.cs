using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour {

	// Use this for initialization
    public Transform target;//玩家的坐标
    private float smoothing = 5;//摄像机移动的平滑度
    private Vector3 offset;//射线机当前位置和目标位置的向量差
    void Start()
    {
        offset = new Vector3(0f, 4.52f, -6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        }
    }
}
