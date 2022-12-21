using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SceneDefine;
using Cysharp.Threading.Tasks;

namespace Loading.Utility
{
    public class LodingManeger : SceneSuperClass
    {
        [Header("�t�F�[�h�֌W")]
        [SerializeField, Tooltip("�t�F�[�h�C����������L�����p�X�O���[�v")] CanvasGroup lodingUiCanvas;
        [SerializeField, Tooltip("�t�F�[�h�C���ɂ����鎞��")] float fadeTime;
        [SerializeField, Tooltip("�t�F�[�h�����鏈�����Ăяo���֐��擾")] LodingUIFade lodingUiFadeManeger;
        [Header("���[�hUI�֌W")]
        [SerializeField, Tooltip("���[�h��ʑS�̂̃I�u�W�F�N�g")] GameObject lodingUI;
        [SerializeField, Tooltip("���[�h�̐i���󋵂�\������UI")] Slider lodingSlider;
        private const float lodingEndValue = 0.9f; //loadOperation���[�h�����̒l(1�ł͂Ȃ�0.9�Ŋ���)
        private AsyncOperation loadOperation; //���[�h�𑀍�
        private AsyncOperation unloadOperation; //�A�����[�h�𑀍삷��

        /// <summary>
        /// �����Ɏ��̃V�[���̖��O����ꃍ�[�h�A�A�����[�h���s��
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadToNextScene(string sceneName)
        {
            LoadSceneAsync(sceneName).Forget();
        }

        /// <summary>
        /// ���݂̃V�[�����A�����[�h�A���̃V�[�������[�h����
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private async UniTask LoadSceneAsync(string sceneName)
        {
            //FadeInFunction����܂őҋ@
            await lodingUiFadeManeger.FadeInFunction(lodingUiCanvas, fadeTime);
            //���Ƀ��[�h����V�[�����擾�����[�h�J�n
            loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            //�V�[�����̊Ǘ� true�ɂȂ�ƃV�[�����؂�ւ��
            loadOperation.allowSceneActivation = false;
            //���[�h�̐g���󋵂�UI�ɔ��f
            lodingSlider.value = loadOperation.progress;

            //���[�h����������܂Ń��[�v
            while (true)
            {
                //��t���[���҂�
                await UniTask.Yield();
                //���[�h������������break
                if (loadOperation.progress >= lodingEndValue)
                {
                    break;
                }
            }
            //�A�����[�h����V�[�����擾(���݃A�N�e�B�u�ȃV�[������Manager�V�[���ȊO)
            var unLoadScene = GetCurrentSceneName();
            //���[�h�������������̃R�[���o�b�N
            loadOperation.completed += Do =>
            {
                //UnloadEnum�Ŏ擾�����V�[�����A�����[�h����
                unloadOperation = SceneManager.UnloadSceneAsync(SceneDictionary.GetSceneNameString(unLoadScene), UnloadSceneOptions.None);
            };
            //�S�Ċ���������V�[�����ڍs������
            loadOperation.allowSceneActivation = true;
        }
    }
}