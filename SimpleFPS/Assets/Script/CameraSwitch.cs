using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;  // ��Ϸ�����ӽ������
    public Camera topDownCamera;  // ���ͼ�ӽ������
    public KeyCode toggleKey = KeyCode.F1;  // �л��ӽǵļ�

    void Start()
    {
        // ��ʼʱ��������������������ͼ�����
        mainCamera.gameObject.SetActive(true);
        topDownCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        // �����л����л��ӽ�
        if (Input.GetKeyDown(toggleKey))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // �л�������ļ���״̬
        bool isMainCameraActive = mainCamera.gameObject.activeSelf;
        mainCamera.gameObject.SetActive(!isMainCameraActive);
        topDownCamera.gameObject.SetActive(isMainCameraActive);
    }
}

