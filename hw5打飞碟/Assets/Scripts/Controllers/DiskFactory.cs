using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自定义异常类，用于在特定情况下抛出异常
public class MyException : System.Exception
{
    public MyException() { }
    public MyException(string message) : base(message) { }
}

// 表示飞碟属性的类
public class DiskAttributes : MonoBehaviour
{
    // 分数、水平速度和垂直速度
    public int score;
    public float speedX;
    public float speedY;

    public float maxRotationAngle = 30f; // 最大旋转角度
    private float currentRotationX = 0f; // 当前旋转角度
    private System.Random rand;

    void Start()
    {
        rand = new System.Random();
    }

    void Update()
    {
        // 每次旋转时，确保角度不会超过最大限制
        float randomRotation = rand.Next(1, 5); 
        currentRotationX += randomRotation;

        // 如果当前旋转角度超过最大角度限制，则限制在最大角度
        if (currentRotationX > maxRotationAngle)
        {
            currentRotationX = maxRotationAngle;
        }

        // 通过改变飞碟的旋转角度来实现限制
        transform.localEulerAngles = new Vector3(-currentRotationX, 0, 0);

  
    }
}

// 飞碟工厂类，管理飞碟的创建、回收等逻辑
public class DiskFactory : MonoBehaviour
{
    // 存储使用中的飞碟对象
    List<GameObject> used;

    // 存储未使用的飞碟对象
    List<GameObject> free;

    // 用于生成随机数的工具
    System.Random rand;

    float minDistance = 2.0f; // 最小间隔距离

    // 在游戏开始时初始化列表和随机数生成器
    void Start()
    {
        used = new List<GameObject>();
        free = new List<GameObject>();
        rand = new System.Random();
    }


    // 获取一个飞碟对象，根据round设置不同属性
    public GameObject GetDisk(int round)
    {
        GameObject disk;

        // 如果有未使用的飞碟，则从free列表中获取
        if (free.Count != 0)
        {
            disk = free[0];
            free.Remove(disk); // 从未使用列表移除
        }
        else
        {
            // 否则实例化一个新的飞碟对象
            disk = GameObject.Instantiate(Resources.Load("Prefabs/disk", typeof(GameObject))) as GameObject;
            disk.AddComponent<DiskAttributes>(); // 添加飞碟属性组件
            disk.AddComponent<Rigidbody>(); // 添加刚体组件
            disk.GetComponent<Rigidbody>().useGravity = true;
        }

        // 旋转
        disk.transform.localEulerAngles = new Vector3(-rand.Next(5, 15), 0, 0);


        // 获取飞碟的属性组件，并根据round设置属性
        DiskAttributes attri = disk.GetComponent<DiskAttributes>();
        attri.score = rand.Next(1, 4); // 设置分数

        // 根据分数和round调整飞碟速度
        // 减缓速度
        attri.speedX = (rand.Next(1, 3) + attri.score + round) * 0.2f; 
        if(attri.speedX <= 1f) attri.speedX = 1f;
        attri.speedY = (rand.Next(1, 3) + attri.score + round) * 0.01f; 

        // 根据分数设置飞碟颜色和大小
        if (attri.score == 3)
        {
            disk.GetComponent<Renderer>().material.color = Color.red;
            disk.transform.localScale += new Vector3(-0.5f, 0, -0.5f);
        }
        else if (attri.score == 2)
        {
            disk.GetComponent<Renderer>().material.color = Color.green;
            disk.transform.localScale += new Vector3(-0.2f, 0, -0.2f);
        }
        else if (attri.score == 1)
        {
            disk.GetComponent<Renderer>().material.color = Color.blue;
        }

        // 随机选择飞碟的进入方向
        int direction = rand.Next(1, 3);

        // 将屏幕边缘坐标定义为生成范围
        float screenEdgeBuffer = 50f; // 距屏幕边缘的缓冲值
        float spawnDistance = Camera.main.nearClipPlane + (float)(rand.NextDouble() * 4f + 4f);
        // 控制生成位置的 Z 轴距离
        float spawnY = Camera.main.pixelHeight * (0.5f + 0.5f * (float)rand.NextDouble());

        // 根据选择的方向设置飞碟初始位置和速度方向
        if (direction == 1)
        {
            // 从屏幕左上方进入
            Vector3 spawnPosition = new Vector3(-screenEdgeBuffer, spawnY, spawnDistance);
            disk.transform.position = Camera.main.ScreenToWorldPoint(spawnPosition);
            attri.speedY *= -1;
        }
        else if (direction == 2)
        {
            // 从屏幕右上方进入
            Vector3 spawnPosition = new Vector3(Camera.main.pixelWidth + screenEdgeBuffer, spawnY, spawnDistance);
            disk.transform.position = Camera.main.ScreenToWorldPoint(spawnPosition);
            attri.speedX *= -1;
            attri.speedY *= -1;
        }
       

        // 将飞碟添加到使用中的列表
        used.Add(disk);
        disk.SetActive(true); // 激活飞碟对象
        Debug.Log("generate disk"); // 输出生成飞碟信息
        return disk;
    }

    // 回收飞碟对象，将其从使用中的列表转移到未使用列表
    public void FreeDisk(GameObject disk)
    {
        disk.SetActive(false); // 隐藏飞碟
        disk.transform.position = new Vector3(0, 0, 0); // 重置位置
        disk.transform.localScale = new Vector3(2f, 0.1f, 2f); // 重置大小
        disk.transform.localEulerAngles = new Vector3(0, 0, 0);

        // 检查该飞碟是否在used列表中
        if (!used.Contains(disk))
        {
            // 如果不在，抛出自定义异常
            throw new MyException("Try to remove a item from a list which doesn't contain it.");
        }

        Debug.Log("free disk"); // 输出回收飞碟信息
        used.Remove(disk); // 从使用中的列表中移除
        free.Add(disk); // 添加到未使用列表
    }
}
