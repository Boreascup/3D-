using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class RoundController : MonoBehaviour, ISceneController, IUserAction
{
    int round = 0;                // 当前轮次
    int max_round = 5;            // 最大轮次
    float timer = 0.5f;           // 每轮飞碟生成的计时器
    GameObject disk;              // 当前飞碟
    DiskFactory factory;          // 飞碟工厂
    public IActionManager actionManager;  // 动作管理器
    public ScoreController scoreController; // 分数控制器
    public UserGUI userGUI;       // 用户界面

    // Start is called before the first frame update
    void Start()
    {
        // 初始化会在Awake中完成，这里不需要初始化
    }

    // Update is called once per frame
    void Update()
    {
        // 检查游戏模式，若未开始（mode == 0），则返回
        if (userGUI.mode == 0) return;

        // 根据是否启用物理控制，选择相应的动作管理器
        //if (userGUI.isKinematic == false)
        //{
        //    actionManager = gameObject.GetComponent<PhysicActionManager>() as IActionManager;
        //}
        //else
        //{
        //    actionManager = gameObject.GetComponent<CCActionManager>() as IActionManager;
        //}
        actionManager = gameObject.GetComponent<PhysicActionManager>() as IActionManager;

        // 检测点击事件
        GetHit();

        // 检查是否游戏结束
        gameOver();

        // 检查轮次是否超过最大轮次，若超过则返回
        if (round > max_round)
        {
            return;
        }

        // 计时器递减
        timer -= Time.deltaTime;

        // 如果计时器到达0，且所有飞碟动作已完成，则生成新一轮飞碟
        if (timer <= 0 && actionManager.RemainActionCount() == 0)
        {
            // 从工厂中获取10个飞碟，并为其添加飞行动作
            for (int i = 0; i < 10; ++i)
            {
                disk = factory.GetDisk(round);  // 获取飞碟
                actionManager.Fly(disk);       // 设置飞行动作
            }

            // 更新轮次并重置计时器
            round += 1;
            if (round <= max_round)
            {
                userGUI.round = round;  // 更新UI中的轮次显示
            }
            timer = 4.0f; // 新一轮的计时
        }
    }

    // Awake在对象激活时调用，初始化组件和工厂
    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadSource();

        // 添加相关组件
        gameObject.AddComponent<UserGUI>();
        gameObject.AddComponent<PhysicActionManager>();
        gameObject.AddComponent<CCActionManager>();
        gameObject.AddComponent<ScoreController>();
        gameObject.AddComponent<DiskFactory>();

        // 获取飞碟工厂和用户界面
        factory = Singleton<DiskFactory>.Instance;
        userGUI = gameObject.GetComponent<UserGUI>();
    }

    // 加载场景资源
    public void LoadSource()
    {
        // 可以根据需求加载场景资源
    }

    // 检查游戏是否结束
    public void gameOver()
    {
        if (round > max_round && actionManager.RemainActionCount() == 0)
            userGUI.gameMessage = "Game Over!";
    }

    // 检测用户点击事件，用于打击飞碟
    public void GetHit()
    {
        // 检测左键点击事件
        if (Input.GetButtonDown("Fire1"))
        {
            // 获取主摄像机，并从鼠标点击位置生成射线
            Camera ca = Camera.main;
            Ray ray = ca.ScreenPointToRay(Input.mousePosition);

            // 如果射线击中了对象
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // 记录分数
                scoreController.Record(hit.transform.gameObject);

                // 将飞碟设置为不活跃
                hit.transform.gameObject.SetActive(false);
            }
        }
    }
}
