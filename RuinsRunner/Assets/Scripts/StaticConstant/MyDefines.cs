using System;
using System.Collections.Generic;
using UnityEngine;
///
/// �V�[���̖��O���������ɎQ�Ƃ���
///
namespace SceneDefine
{        //�V�[����ǉ����邲�Ƃ�enum��dictionary�̗v�f��ǉ�����
         //�P�ɃV�[���̗��������g�������ꍇ�͂�������g��
         //GameManager������ĕ������Ȃ����߂ɓo�^���Ă��Ȃ�
    public enum SceneName
    {
        NULL,
        JEC,
        TITLE,
        DEMO,
        INGAME,
        GAMEOVER,
        RESULT,
        ENDING,
    }
    public class SceneDictionary
    {
        //���ۂ̃V�[���̖��O���~�����ꍇ�͂�����Ɉ����Ƃ���enum(key)��n��
        public static string GetSceneNameString (SceneName _sName) 
        {
            return sceneDictionary_[_sName];
        }

        //���ۂ̃V�[��������enum���ق����ꍇ�͂�����Ɉ����Ƃ���string(value)��n��
        //���̌�����value�ł���V�[���������j�[�N�ł��邱�Ƃ�M�����čs���Ă���
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
                    throw new KeyNotFoundException();
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
            return SceneName.NULL;
        }

        private static Dictionary<SceneName, string> sceneDictionary_ = new Dictionary<SceneName, string>(){
                { SceneName.JEC,        "Scene_Jec"      },
                { SceneName.TITLE,      "Scene_Title"    },
                { SceneName.DEMO,       "Scene_Demo"     },
                { SceneName.INGAME,     "Scene_MainGame" },
                { SceneName.GAMEOVER,   "Scene_GameOver" },
                { SceneName.RESULT,     "Scene_Result"   },
                { SceneName.ENDING,     "Scene_Ending"   },
        };
    }
}

///
/// MainGameScene��GameState��\��enum
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