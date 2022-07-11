using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameStickRoundAndRound_ImageManager : ObjectSuperClass
{
    MiniGameStickRoundAndRoundManager manager_;
    [SerializeField] H_GaugeManager powerGauge_;              //�p���[�Q�[�W
    [SerializeField] Image clearArea_;

    [SerializeField] Image arrow_;

    // Start is called before the first frame update
    void Start()
    {
        manager_ = gameObject.GetComponent<MiniGameStickRoundAndRoundManager>();
    }
    // Update is called once per frame
    void Update()
    {
        //�X�V
        powerGauge_.GaugeValueChange(0.0f, 1.0f, manager_.power);

        clearArea_.rectTransform.offsetMin = new Vector2(MiniGameStickRoundAndRoundManager.clearPower * 100, clearArea_.rectTransform.offsetMin.y);

        arrow_.rectTransform.Rotate(new Vector3(0, 0, -180 * Time.unscaledDeltaTime));
    }

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

        }

        // �A���}�l�[�W���\�[�X�̉���������L�q


        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(_disposing);
    }
}
