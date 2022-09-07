using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAudio : ObjectSuperClass
{
    static AudioSource audioSource_;
    [SerializeField] AudioSource audioSourceSet_;

    struct PlayAudioData
    {
        public AudioClip audioClip;
        public float length;
        public float playTime;
    }

    //再生してる音データリスト
    static List<PlayAudioData> playAudioList_;

    // Start is called before the first frame update
    void Start()
    {
        //MainGameSceneだったら消えないようにする
        if(gameObject.scene.name == "Scene_MainGame")
        {
            DontDestroyOnLoad(gameObject);
        }


        if(audioSourceSet_ != null)
        {
            audioSource_ = audioSourceSet_;
        }

        playAudioList_ = new List<PlayAudioData>();
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクトがDontDestroyOnLoadだったら
        if (gameObject.scene.name == "DontDestroyOnLoad")
        {
            //アクティブシーンがMainGameでないなら
            if(SceneManager.GetActiveScene().name != "Scene_MainGame")
            {
                //このオブジェクトをアクティブなシーンに移動（DontDestroyOnLoadの解除）
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            }
        }

        //削除する要素の番号リスト
        List<int> deleteNumbers = new List<int>();

        //時間を増やす
        for(int i = 0; i < playAudioList_.Count; ++i)
        {
            PlayAudioData audioData = playAudioList_[i];

            audioData.playTime += Time.unscaledDeltaTime;

            //音の長さより再生時間のほうが長かったら削除
            if (audioData.playTime >= audioData.length)
            {
                deleteNumbers.Add(i);
            }

            playAudioList_[i] = audioData;
        }

        //要素の削除 逆順に処理
        for(int i = deleteNumbers.Count - 1; i >= 0; --i)
        {
            playAudioList_.RemoveAt(deleteNumbers[i]);
        }
    }

    public static void PlaySE(AudioClip _audioClip)
    {
        PlayAudioData audioData = new PlayAudioData();
        audioData.audioClip = _audioClip;
        audioData.length = _audioClip.length;
        audioData.playTime = 0.0f;

        playAudioList_.Add(audioData);

        if (audioSource_ != null)
        {
            audioSource_.PlayOneShot(audioData.audioClip);
        }
        else
        {
            Debug.LogWarning("AudioSourceが見つかりません");
        }
    }

    /// <summary>
    /// 継承先Dispose() override テンプレート
    /// <summary>
    //継承先では以下のようにoverrideすること
    //マネージリソース、アンマネージドリソースについては↓のURLを参考に、newしたものかどうかで判断する
    //https://hilapon.hatenadiary.org/entry/20100904/1283570083

    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {
            return; // 解放済みなので処理しない
        }

        if (_disposing)
        {
            // マネージリソースの解放処理を記述
            audioSource_ = null;

            playAudioList_.Clear();
            playAudioList_ = null;
        }

        // アンマネージリソースの解放処理を記述

        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(_disposing);
    }
}
