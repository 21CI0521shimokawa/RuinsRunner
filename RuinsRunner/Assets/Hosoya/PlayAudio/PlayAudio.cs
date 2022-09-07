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

    //�Đ����Ă鉹�f�[�^���X�g
    static List<PlayAudioData> playAudioList_;

    // Start is called before the first frame update
    void Start()
    {
        //MainGameScene������������Ȃ��悤�ɂ���
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
        //���̃I�u�W�F�N�g��DontDestroyOnLoad��������
        if (gameObject.scene.name == "DontDestroyOnLoad")
        {
            //�A�N�e�B�u�V�[����MainGame�łȂ��Ȃ�
            if(SceneManager.GetActiveScene().name != "Scene_MainGame")
            {
                //���̃I�u�W�F�N�g���A�N�e�B�u�ȃV�[���Ɉړ��iDontDestroyOnLoad�̉����j
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            }
        }

        //�폜����v�f�̔ԍ����X�g
        List<int> deleteNumbers = new List<int>();

        //���Ԃ𑝂₷
        for(int i = 0; i < playAudioList_.Count; ++i)
        {
            PlayAudioData audioData = playAudioList_[i];

            audioData.playTime += Time.unscaledDeltaTime;

            //���̒������Đ����Ԃ̂ق�������������폜
            if (audioData.playTime >= audioData.length)
            {
                deleteNumbers.Add(i);
            }

            playAudioList_[i] = audioData;
        }

        //�v�f�̍폜 �t���ɏ���
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
            Debug.LogWarning("AudioSource��������܂���");
        }
    }

    /// <summary>
    /// �p����Dispose() override �e���v���[�g
    /// <summary>
    //�p����ł͈ȉ��̂悤��override���邱��
    //�}�l�[�W���\�[�X�A�A���}�l�[�W�h���\�[�X�ɂ��Ắ���URL���Q�l�ɁAnew�������̂��ǂ����Ŕ��f����
    //https://hilapon.hatenadiary.org/entry/20100904/1283570083

    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {
            return; // ����ς݂Ȃ̂ŏ������Ȃ�
        }

        if (_disposing)
        {
            // �}�l�[�W���\�[�X�̉���������L�q
            audioSource_ = null;

            playAudioList_.Clear();
            playAudioList_ = null;
        }

        // �A���}�l�[�W���\�[�X�̉���������L�q

        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(_disposing);
    }
}
