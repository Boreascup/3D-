using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 飞碟从界面左右两侧飞入，离开界面时运动结束
public class CCFlyAction : SSAction
{
    // 速度参数，分别控制飞碟在X轴和Y轴上的移动速度
    public float speedX;
    public float speedY;

    // 获取一个新的飞碟运动动作实例
    public static CCFlyAction GetSSAction(float x, float y)
    {
        // 创建并返回一个新的 CCFlyAction 实例，并设置其速度
        CCFlyAction action = ScriptableObject.CreateInstance<CCFlyAction>();
        action.speedX = x;
        action.speedY = y;
        return action;
    }

    // 在动作开始时调用
    public override void Start()
    {
        // 禁用飞碟的物理模拟（设置刚体为 Kinematic 模式）
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    // 每帧调用一次，用于更新飞碟的位置
    public override void Update()
    {
        // 如果飞碟已经被销毁或禁用（不再处于活动状态）
        if (this.transform.gameObject.activeSelf == false)
        {
            // 标记动作为完成
            this.destroy = true;
            // 通知回调函数，动作已完成
            this.callback.SSActionEvent(this);
            return;
        }

        // 将飞碟的世界坐标转换为屏幕坐标
        Vector3 vec3 = Camera.main.WorldToScreenPoint(this.transform.position);

        // 如果飞碟已经离开屏幕范围
        if (vec3.x < -100 || vec3.x > Camera.main.pixelWidth + 100 || vec3.y < -100 || vec3.y > Camera.main.pixelHeight + 100)
        {
            // 标记动作为完成
            this.destroy = true;
            // 通知回调函数，动作已完成
            this.callback.SSActionEvent(this);
            return;
        }

        // 更新飞碟的位置，基于当前速度和时间增量
        transform.position += new Vector3(speedX, speedY, 0) * Time.deltaTime * 2;
    }
}
