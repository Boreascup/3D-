using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;  // 游戏的主视角摄像机
    public Camera topDownCamera;  // 鸟瞰图视角摄像机
    public KeyCode toggleKey = KeyCode.F1;  // 切换视角的键

    void Start()
    {
        // 初始时启用主摄像机，禁用鸟瞰图摄像机
        mainCamera.gameObject.SetActive(true);
        topDownCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        // 按下切换键切换视角
        if (Input.GetKeyDown(toggleKey))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // 切换摄像机的激活状态
        bool isMainCameraActive = mainCamera.gameObject.activeSelf;
        mainCamera.gameObject.SetActive(!isMainCameraActive);
        topDownCamera.gameObject.SetActive(isMainCameraActive);
    }
}

