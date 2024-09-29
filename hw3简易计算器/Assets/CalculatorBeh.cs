using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorBeh : MonoBehaviour
{

    // Model / ״̬
    private string display = "0"; // ��ʾ��ֵ
    private string input = "";    // ��ǰ���������
    private string operation = ""; // ��ǰ������
    private float result = 0;     // ��ǰ���
    private bool isNewOperation = true; // �Ƿ����²���
    private bool hasOperation = false;  // �Ƿ����в�����

    // System Handlers
    void Start()
    {
        ResetCalculator();
    }

    // View 
    void OnGUI()
    {
        // ��ʾ��
        GUI.Box(new Rect(210, 25, 280, 50), display);

        // ���ְ�ť
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int number = i * 3 + (j + 1);
                if (GUI.Button(new Rect(210 + j * 70, 100 + (2 - i) * 70, 70, 70), number.ToString()))
                {
                    PressNumber(number);
                }
            }
        }

        // 0��ť
        if (GUI.Button(new Rect(210, 310, 140, 70), "0"))
        {
            PressNumber(0);
        }

        // ��������ť
        if (GUI.Button(new Rect(420, 100, 70, 70), "+")) PressOperation("+");
        if (GUI.Button(new Rect(420, 170, 70, 70), "-")) PressOperation("-");
        if (GUI.Button(new Rect(420, 240, 70, 70), "*")) PressOperation("*");
        if (GUI.Button(new Rect(420, 310, 70, 70), "/")) PressOperation("/");

        // ���ڰ�ť
        if (GUI.Button(new Rect(350, 310, 70, 70), "=")) Calculate();

        // ���ð�ť
        if (GUI.Button(new Rect(490, 100, 70, 70), "C")) ResetCalculator();
    }

    // Components / ������
    void PressNumber(int number)
    {
        if (isNewOperation || hasOperation)
        {
            input = number.ToString();
            isNewOperation = false;
            hasOperation = false; // ��ʾ����������
        }
        else
        {
            input += number.ToString();
        }
        display = input;
    }

    void PressOperation(string op)
    {
        if (!isNewOperation)
        {
            // ֻ���ڰ��²�������Ž��м���
            if (!string.IsNullOrEmpty(operation))
            {
                Calculate();
            }
            else
            {
                result = float.Parse(input);
            }
        }

        operation = op;
        isNewOperation = true;
        hasOperation = true; // ��ʶ�����Ѱ���
    }

    void Calculate()
    {
        if (string.IsNullOrEmpty(input)) return;

        float secondOperand = float.Parse(input);
        switch (operation)
        {
            case "+":
                result += secondOperand;
                break;
            case "-":
                result -= secondOperand;
                break;
            case "*":
                result *= secondOperand;
                break;
            case "/":
                if (secondOperand != 0)
                    result /= secondOperand;
                else
                    display = "Error";
                break;
        }

        if (display != "Error")
        {
            display = result.ToString();
        }

        input = result.ToString();
        operation = ""; // ��������������
        isNewOperation = true;
    }

    void ResetCalculator()
    {
        input = "";
        operation = "";
        result = 0;
        display = "0";
        isNewOperation = true;
        hasOperation = false;
    }
}
