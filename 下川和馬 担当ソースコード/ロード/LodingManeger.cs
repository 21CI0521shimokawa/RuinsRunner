using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SceneDefine;
using Cysharp.Threading.Tasks;

namespace Loading.Utility
{
    public class LodingManeger : SceneSuperClass
    {
        [Header("フェード関係")]
        [SerializeField, Tooltip("フェードインをさせるキャンパスグループ")] CanvasGroup lodingUiCanvas;
        [SerializeField, Tooltip("フェードインにかける時間")] float fadeTime;
        [SerializeField, Tooltip("フェードさせる処理を呼び出す関数取得")] LodingUIFade lodingUiFadeManeger;
        [Header("ロードUI関係")]
        [SerializeField, Tooltip("ロード画面全体のオブジェクト")] GameObject lodingUI;
        [SerializeField, Tooltip("ロードの進捗状況を表示するUI")] Slider lodingSlider;
        private const float lodingEndValue = 0.9f; //loadOperationロード完了の値(1ではなく0.9で完了)
        private AsyncOperation loadOperation; //ロードを操作
        private AsyncOperation unloadOperation; //アンロードを操作する

        /// <summary>
        /// 引数に次のシーンの名前を入れロード、アンロードを行う
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadToNextScene(string sceneName)
        {
            LoadSceneAsync(sceneName).Forget();
        }

        /// <summary>
        /// 現在のシーンをアンロード、次のシーンをロードする
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private async UniTask LoadSceneAsync(string sceneName)
        {
            //FadeInFunctionするまで待機
            await lodingUiFadeManeger.FadeInFunction(lodingUiCanvas, fadeTime);
            //次にロードするシーンを取得しロード開始
            loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            //シーン許可の管理 trueになるとシーンが切り替わる
            loadOperation.allowSceneActivation = false;
            //ロードの身長状況をUIに反映
            lodingSlider.value = loadOperation.progress;

            //ロードが完了するまでループ
            while (true)
            {
                //一フレーム待つ
                await UniTask.Yield();
                //ロードが完了したらbreak
                if (loadOperation.progress >= lodingEndValue)
                {
                    break;
                }
            }
            //アンロードするシーンを取得(現在アクティブなシーンかつManagerシーン以外)
            var unLoadScene = GetCurrentSceneName();
            //ロードが完了した時のコールバック
            loadOperation.completed += Do =>
            {
                //UnloadEnumで取得したシーンをアンロードする
                unloadOperation = SceneManager.UnloadSceneAsync(SceneDictionary.GetSceneNameString(unLoadScene), UnloadSceneOptions.None);
            };
            //全て完了したらシーンを移行許可する
            loadOperation.allowSceneActivation = true;
        }
    }
}