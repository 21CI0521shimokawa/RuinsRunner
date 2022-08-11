using System;
using System.Collections.Generic;
using UnityEngine;
///
/// シーンの名前を扱う時に参照する
///
namespace SceneDefine
{        //シーンを追加するごとにenumとdictionaryの要素を追加する
         //単にシーンの略名等を使いたい場合はこちらを使う
         //GameManagerを誤って複製しないために登録していない
    public enum SceneName
    {
        NULL,
        JEC,
        TITLE,
        DEMO,
        RUNGAME,
        MINIGAME1,
        GAMEOVER,
        RESULT,
        ENDING,
    }
    public class SceneDictionary
    {
        //実際のシーンの名前が欲しい場合はこちらに引数としてenum(key)を渡す
        public static string GetSceneNameString (SceneName _sName) 
        {
            return sceneDictionary_[_sName];
        }

        //実際のシーン名からenumがほしい場合はこちらに引数としてstring(value)を渡す
        //この検索はvalueであるシーン名がユニークであることを信頼して行っている
        public static SceneName GetSceneNameEnum(string _sName)
        {
            try
            {
                foreach (var key in sceneDictionary_.Keys)
                {
                    if (sceneDictionary_[key] == _sName)
                    {
                        return key;
                    }
                }
                throw new KeyNotFoundException();
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
            return SceneName.NULL;
        }

        private static readonly Dictionary<SceneName, string> sceneDictionary_ = new Dictionary<SceneName, string>(){
                { SceneName.JEC,        "Scene_Jec"      },
                { SceneName.TITLE,      "Scene_Title"    },
                { SceneName.DEMO,       "Scene_Demo"     },
                { SceneName.RUNGAME,    "Scene_MainGame" },
                { SceneName.MINIGAME1,  "Test_MiniGame1" },
                { SceneName.GAMEOVER,   "Scene_GameOver" },
                { SceneName.RESULT,     "Scene_Result"   },
                { SceneName.ENDING,     "Scene_Ending"   },
        };
    }
}

///
/// MainGameSceneのGameStateを表すenum
///
namespace GameStateDefine
{
    public enum GameState
    { 
        Run, 
        MiniGame1, 
        GameOver
    }
}