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

        // 定义游戏介绍文本内容
        string introText = "移动：WASD\n蓄力拉弓：空格\n发射弓箭：左键\n瞄准镜：右键\n鸟瞰图：F1";

        // 在屏幕左上角绘制游戏介绍文本
        GUI.Label(new Rect(10, 10, 300, 100), introText, style);
        style.fontSize = 24;
        style.normal.textColor = Color.red;
        // 在屏幕右上角显示分数
        GUI.Label(new Rect(Screen.width - 150, 20, 150, 30), "Score: " + Score, style);
        // 显示射击次数
        style.fontSize = 18;
        GUI.Label(new Rect(Screen.width - 250, Screen.height - 20, 250, 30), "每个射击区域有十次射击机会 ", style);
        GUI.Label(new Rect(Screen.width - 150, Screen.height - 50, 150, 30), "山顶区 Shots: " + archerControllerScript.area1Shots, style);
        GUI.Label(new Rect(Screen.width - 150, Screen.height - 80, 150, 30), "迷宫区 Shots: " + archerControllerScript.area2Shots, style);

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