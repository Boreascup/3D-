using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorBeh : MonoBehaviour
{

    // Model / 状态
    private string display = "0"; // 显示的值
    private string input = "";    // 当前输入的数字
    private string operation = ""; // 当前操作符
    private float result = 0;     // 当前结果
    private bool isNewOperation = true; // 是否是新操作
    private bool hasOperation = false;  // 是否已有操作符

    // System Handlers
    void Start()
    {
        ResetCalculator();
    }

    // View 
    void OnGUI()
    {
        // 显示屏
        GUI.Box(new Rect(210, 25, 280, 50), display);

        // 数字按钮
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

        // 0按钮
        if (GUI.Button(new Rect(210, 310, 140, 70), "0"))
        {
            PressNumber(0);
        }

        // 操作符按钮
        if (GUI.Button(new Rect(420, 100, 70, 70), "+")) PressOperation("+");
        if (GUI.Button(new Rect(420, 170, 70, 70), "-")) PressOperation("-");
        if (GUI.Button(new Rect(420, 240, 70, 70), "*")) PressOperation("*");
        if (GUI.Button(new Rect(420, 310, 70, 70), "/")) PressOperation("/");

        // 等于按钮
        if (GUI.Button(new Rect(350, 310, 70, 70), "=")) Calculate();

        // 重置按钮
        if (GUI.Button(new Rect(490, 100, 70, 70), "C")) ResetCalculator();
    }

    // Components / 控制器
    void PressNumber(int number)
    {
        if (isNewOperation || hasOperation)
        {
            input = number.ToString();
            isNewOperation = false;
            hasOperation = false; // 表示已输入数字
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
            // 只有在按下操作符后才进行计算
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
        hasOperation = true; // 标识符号已按下
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
        operation = ""; // 计算后清除操作符
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
