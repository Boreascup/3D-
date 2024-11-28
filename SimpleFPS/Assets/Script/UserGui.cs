using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGui : MonoBehaviour
{
    // Start is called before the first frame update
    public float Score = 0;
    public ArcherController archerControllerScript;
    private string hitMessage = "";
    void Start()
    {
        archerControllerScript = GameObject.FindObjectOfType<ArcherController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 14;
        style.normal.textColor = Color.black;

        // ������Ϸ�����ı�����
        string introText = "�ƶ���WASD\n�����������ո�\n���乭�������\n��׼�����Ҽ�\n���ͼ��F1";

        // ����Ļ���Ͻǻ�����Ϸ�����ı�
        GUI.Label(new Rect(10, 10, 300, 100), introText, style);
        style.fontSize = 24;
        style.normal.textColor = Color.red;
        // ����Ļ���Ͻ���ʾ����
        GUI.Label(new Rect(Screen.width - 150, 20, 150, 30), "Score: " + Score, style);
        // ��ʾ�������
        style.fontSize = 18;
        GUI.Label(new Rect(Screen.width - 250, Screen.height - 20, 250, 30), "ÿ�����������ʮ��������� ", style);
        GUI.Label(new Rect(Screen.width - 150, Screen.height - 50, 150, 30), "ɽ���� Shots: " + archerControllerScript.area1Shots, style);
        GUI.Label(new Rect(Screen.width - 150, Screen.height - 80, 150, 30), "�Թ��� Shots: " + archerControllerScript.area2Shots, style);

        GUI.Label(new Rect(50, Screen.height - 50, 150, 30), hitMessage, style);
    }
    public void SetHitMessage(string message)
    {
        hitMessage = message;
    }

    public void ClearHitMessage()
    {
        hitMessage = "";
    }
}