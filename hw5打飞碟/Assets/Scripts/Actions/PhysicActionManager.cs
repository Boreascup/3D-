using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicActionManager : SSActionManager, IActionCallback, IActionManager
{
    public RoundController sceneController; // 场景控制器，用于管理游戏轮次和状态
    public PhysicFlyAction action;          // 物理飞行动作
    public DiskFactory factory;             // 飞碟工厂，用于生成和回收飞碟

    // Start is called before the first frame update
    protected new void Start()
    {
        // 获取当前场景控制器，并设置为该动作管理器
        sceneController = (RoundController)SSDirector.getInstance().currentSceneController;
        sceneController.actionManager = this as IActionManager;

        // 获取飞碟工厂的实例
        factory = Singleton<DiskFactory>.Instance;
    }

    // SSActionEvent 回调函数，当动作结束时调用
    public void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Completed,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null)
    {
        // 动作完成后将飞碟释放回飞碟工厂
        factory.FreeDisk(source.transform.gameObject);
    }

    // 重写 MoveDisk 方法，为飞碟设置物理飞行动作并执行
    public override void MoveDisk(GameObject disk)
    {
        // 获取飞行动作并为飞碟设置初始速度
        action = PhysicFlyAction.GetSSAction(disk.GetComponent<DiskAttributes>().speedX);

        // 运行该动作，并指定回调为当前管理器
        RunAction(disk, action, this);
    }

    // Fly 方法接口实现，用于启动飞碟的飞行动作
    public void Fly(GameObject disk)
    {
        MoveDisk(disk);
    }

    // RemainActionCount 返回剩余动作的数量
    public int RemainActionCount()
    {
        return actions.Count;
    }
}
