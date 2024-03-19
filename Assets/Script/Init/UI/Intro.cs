using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script.Init.UI
{
    public class Intro : MonoBehaviour
    {
        //todo 加载场景Tip
        private List<string> _intro = new();
        private TMP_Text _introText;
        private void Awake()
        {
            _introText = GetComponent<TMP_Text>();
            _intro.Add("正在返回主菜单");
            _intro.Add("第一幕\n道路是未知的，方向是迷茫的，指引是缺失的，但这并不妨碍我去探寻前方。\n我在探寻中了解道路，在探寻中得知方向，在探寻中获得光亮。");
            _intro.Add("第二幕\n在黑白的世界寻找如火光亮，光亮是我渴望飞翔的翅膀，轻盈坚韧载着我去往前方。\n愿我身轻如燕，让我能够用尽全身这微小的力气踏向高处。\n即使我失足从悬崖摔下，我粉身碎骨，我也仍会挣扎爬起。");
            _intro.Add("第三幕\n亲爱的光亮，你填补我几近破损身体的缝隙。\n让我那颗半透明的心与你共鸣，让我在路上疾驰。");
            _intro.Add("第四幕\n我能化作光亮与你同行吗？\n我能化作光亮引领他人吗？");
        }

        //随机加载tip
        private void Start()
        {
            _introText.text = Init.Scene switch
            {
                Loader.Scene.MainMenu => _intro[0],
                Loader.Scene.Level1 => _intro[1],
                Loader.Scene.Level2 => _intro[2],
                Loader.Scene.Level3 => _intro[3],
                Loader.Scene.Level4 => _intro[4],
                _ => _introText.text
            };
            
        }
    }
}