using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 飞碟从界面左右两侧飞入，离开界面时运动结束
public class PhysicFlyAction : SSAction
{
    // 控制飞碟在 X 轴上的速度
    public float speedX;

    // 获取一个新的物理飞行动作实例
    public static PhysicFlyAction GetSSAction(float x)
    {
        // 创建并返回一个新的 PhysicFlyAction 实例，并设置其速度
        PhysicFlyAction action = ScriptableObject.CreateInstance<PhysicFlyAction>();
        action.speedX = x;
        return action;
    }

    // 在动作开始时调用
    public override void Start()
    {
        //// 获取飞碟的 Rigidbody 组件并将其设置为物理模拟模式（设置为非 Kinematic）
        //gameObject.GetComponent<Rigidbody>().isKinematic = false;

        // 设置飞碟的初始速度，沿 X 轴的速度是 speedX 的 10 倍，Y 和 Z 轴速度为 0
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(speedX * 10, 0, 0);

        // 设置飞碟的空气阻力，使飞碟减速
        gameObject.GetComponent<Rigidbody>().drag = 1f;
    }

    // 每帧调用一次，用于更新飞碟的位置和检测其是否离开屏幕
    public override void Update()
    {
        // 如果飞碟已经被销毁（即飞碟对象不再激活）
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
    }
}
