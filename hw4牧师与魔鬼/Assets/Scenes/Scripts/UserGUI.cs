//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//namespace Mygame{

//    public class UserGUI : MonoBehaviour {


//        private Controllor action;
//        public string timeStr = string.Empty;
//        void Start(){
//            action = SSDirector.GetInstance().CurrentScenceController as Controllor;
//        }

//        void OnGUI(){

//            /*设置计时器*/
//            timeStr = string.Format("用时：{0:D2}:{1:D2}:{2:D2}", action.getTimer().hour, action.getTimer().minute, action.getTimer().second);
//            GUI.Label(new Rect(500, 10, 100, 200), timeStr);

//            /*定义字体风格*/
//            GUIStyle text_style;
//            GUIStyle button_style;

//            text_style = new GUIStyle()
//            {
//                fontSize = 30
//            };
//            button_style = new GUIStyle("button")
//            {
//                fontSize = 15
//            };
//            /*游戏规则按钮*/
//            if (GUI.Button(new Rect(10, 10, 100, 30), "game rules", button_style)){
//            /*点一下打开游戏规则提示，再点一下关闭游戏规则提示*/
//                action.setIsShowRules(!action.getIsShowRules());
//            }

//            /*展示游戏规则*/
//            if(action.getIsShowRules()){
//                GUI.Label(new Rect(Screen.width / 2 - 150, 50, 300, 50), "Win: all priests and demons cross the river");
//                GUI.Label(new Rect(Screen.width / 2 - 150, 70, 400, 50), "Lose: there are more demons than priests on either side");
//                GUI.Label(new Rect(Screen.width / 2 - 150, 90, 300, 50), "Tap priest, demon, ship to move");
//            }

//            /*重新开始按钮*/
//            if (GUI.Button(new Rect(230, 10, 100, 30), "restart", button_style)) {
//                action.setGameState(0);
//                action.Restart();
//            }

//            /*游戏暂停按钮*/
//            if(GUI.Button(new Rect(340, 10, 100, 30), "pause", button_style)){
//                if (action.isPlaying()) { //游戏正在进行
//                    action.setGameState(3); //游戏的状态发生改变
//                    action.getTimer().StopTiming(); //事件暂停
//                }
//            }

//            if(action.isPause()){//游戏暂停，打印暂停并显示“Return Game”按钮提示玩家可以点击回到游戏中
//                GUI.Label(new Rect(Screen.width / 2-100, Screen.height / 2-120, 100, 50), "Game pause!", text_style);
//                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-40, 120, 50), "Return Game", button_style)){
//                    action.setGameState(0);
//                    action.getTimer().beginTiming();
//                }
//            }

//            if (action.isLose()){//游戏失败，打印失败并显示“Try again”按钮提示玩家再进行尝试
//                GUI.Label(new Rect(Screen.width / 2-90, Screen.height / 2-120, 100, 50), "Gameover!", text_style);
//                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-40, 100, 50), "Try again", button_style))
//                {
//                    action.setGameState(0);
//                    action.Restart(); 
//                }
//            }
//            else if (action.isWin()){//游戏成功，打印成功并显示“Play again”按钮提示玩家再玩一局
//                GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 120, 100, 50), "You win!", text_style);
//                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-40, 100, 50), "Play again", button_style)){
//                    action.setGameState(0);
//                    action.Restart();
//                }
//            }
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mygame
{

    public class UserGUI : MonoBehaviour
    {

        private Controllor action;
        public string timeStr = string.Empty;

        // 自定义贴图
        public Texture2D pauseButtonTexture;
        public Texture2D continueButtonTexture;
        public Texture2D restartButtonTexture;
        public Texture2D rulesButtonTexture;

        void Start()
        {
            action = SSDirector.GetInstance().CurrentScenceController as Controllor;

            // 加载贴图资源
            pauseButtonTexture = Resources.Load<Texture2D>("Textures/pause_button");
            continueButtonTexture = Resources.Load<Texture2D>("Textures/continue_button");
            restartButtonTexture = Resources.Load<Texture2D>("Textures/restart_button");
            rulesButtonTexture = Resources.Load<Texture2D>("Textures/rules_button");
        }

        void OnGUI()
        {

            /*设置计时器*/
            timeStr = string.Format("用时：{0:D2}:{1:D2}:{2:D2}", action.getTimer().hour, action.getTimer().minute, action.getTimer().second);

            /*定义字体风格和背景风格*/
            GUIStyle text_style = new GUIStyle();
            text_style.fontSize = 20;
            text_style.normal.textColor = Color.white;

            GUIStyle button_style = new GUIStyle("button");
            button_style.fontSize = 15;

            GUIStyle title_style = new GUIStyle();
            title_style.fontSize = 30;
            title_style.normal.textColor = Color.yellow;
            title_style.alignment = TextAnchor.MiddleCenter;

            GUIStyle background_style = new GUIStyle(GUI.skin.box);
            background_style.normal.background = Texture2D.whiteTexture;  // 背景颜色
            background_style.normal.textColor = Color.black;
            background_style.fontSize = 20;
            background_style.alignment = TextAnchor.MiddleCenter;


            /*显示计时器*/
            GUI.Label(new Rect(400, 10, 200, 50), timeStr, text_style);

            /*游戏规则按钮*/
            if (rulesButtonTexture != null)
            {
                if (GUI.Button(new Rect(20, 20, 45, 45), rulesButtonTexture))
                {
                    action.setIsShowRules(!action.getIsShowRules());
                }
            }
            else
            {
                if (GUI.Button(new Rect(20, 40, 50, 30), "Game Rules", button_style))
                {
                    action.setIsShowRules(!action.getIsShowRules());
                }
            }

            /*重新开始按钮*/
            if (restartButtonTexture != null)
            {
                if (GUI.Button(new Rect(130, 20, 45, 45), restartButtonTexture))
                {
                    action.setGameState(0);
                    action.Restart();
                }
            }
            else
            {
                if (GUI.Button(new Rect(130, 40, 50, 30), "Restart", button_style))
                {
                    action.setGameState(0);
                    action.Restart();
                }
            }

            /*游戏暂停按钮*/
            if (pauseButtonTexture != null)
            {
                if (GUI.Button(new Rect(240, 20, 45, 45), pauseButtonTexture))
                {
                    if (action.isPlaying())
                    {
                        action.setGameState(3);
                        action.getTimer().StopTiming();
                    }
                }
            }
            else
            {
                if (GUI.Button(new Rect(240, 40, 100, 30), "Pause", button_style))
                {
                    if (action.isPlaying())
                    {
                        action.setGameState(3);
                        action.getTimer().StopTiming();
                    }
                }
            }

            /*展示游戏规则*/
            if (action.getIsShowRules())
            {
                GUI.backgroundColor = new Color(0, 0, 0, 0.6f);  // 半透明黑色背景
                GUI.Label(new Rect(20, 80, 300, 30), "在一个夜黑风高的晚上，恶魔变成了黑山羊前往人间。", text_style);
                GUI.Label(new Rect(20, 110, 400, 30), "听闻消息后，一群善良的牧师假扮成了绵羊去逮捕恶魔。", text_style);
                GUI.Label(new Rect(20, 140, 300, 30), "它们在一条河边相遇了，河上只有一条船，一条船上最多载两只羊。", text_style);
                GUI.Label(new Rect(20, 170, 300, 30), "河流的任意一岸，若恶魔的数量大于牧师的数量，牧师会被吃掉。", text_style);
                GUI.Label(new Rect(20, 200, 300, 30), "点击山羊，绵羊和船进行移动！祝你成功", text_style);
            }

            /*暂停状态*/
            if (action.isPause())
            {
                GUI.backgroundColor = new Color(0, 0, 0, 0.8f); // 半透明背景
                GUI.Box(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 100, 240, 200), "", background_style);
                GUI.Label(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 100, 200, 50), "Game Paused", title_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 20, 50, 50), continueButtonTexture))
                {
                    action.setGameState(0);
                    action.getTimer().beginTiming();
                }
            }

            /*游戏失败状态*/
            if (action.isLose())
            {
                GUI.backgroundColor = new Color(0, 0, 0, 0.8f); // 半透明背景
                GUI.Box(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 100, 240, 200), "", background_style);
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 80, 200, 50), "Game Over!", title_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 20, 140, 50), "Try Again", button_style))
                {
                    action.setGameState(0);
                    action.Restart();
                }
            }
            /*游戏胜利状态*/
            else if (action.isWin())
            {
                GUI.backgroundColor = new Color(0, 0, 0, 0.8f); // 半透明背景
                GUI.Box(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 100, 240, 200), "", background_style);
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 80, 200, 50), "You Win!", title_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 20, 140, 50), "Play Again", button_style))
                {
                    action.setGameState(0);
                    action.Restart();
                }
            }
        }
    }
}
