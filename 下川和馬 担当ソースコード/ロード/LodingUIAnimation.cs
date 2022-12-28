using UnityEngine;
using DG.Tweening;

public class LodingUIAnimation : MonoBehaviour
{
    [SerializeField, Tooltip("�A�j���[�V����������UI�擾")] RectTransform uiRectTransform;
    [SerializeField, Tooltip("�A�j���[�V�������W�擾")] float doMoveToYValue;
    [SerializeField, Tooltip("�ړ�����")] float moveTime;
    [SerializeField, Tooltip("�C�[�W���O�^�C�v")] Ease easeType;
    [SerializeField, Tooltip("���[�v���鎞�̃C�[�W���O�^�C�v")] LoopType loopType;
    private const int loopTime = -1; //���[�v�����邽�߂̕ϐ�

    void Start()
    {
        //doMoveToYValue��moveTime�̑����ňړ�������
        uiRectTransform.DOLocalMoveY(doMoveToYValue, moveTime)
            //�A�j���[�V�����𑊑ΓI�ɂ���
            .SetRelative(true)
            //�C�[�W���O�^�C�v���w��
            .SetEase(easeType)
            //���[�v���鎞�̃C�[�W���O�^�C�v���w�肵�ړ��I�������ŏ��̈ʒu�ɖ߂�悤�ɂ���
            .SetLoops(loopTime, loopType);
    }
}
