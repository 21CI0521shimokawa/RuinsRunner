using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameQTE_ArrowSymbolController : ObjectSuperClass
{
    Image image_;
    Vector2 moveVec_;

    Vector3 startPos_;
    Vector3 turnPos_;

    // Start is called before the first frame update
    void Start()
    {
        image_ = gameObject.GetComponent<Image>();
        moveVec_ = new Vector2(0, 10);
        startPos_ = image_.rectTransform.anchoredPosition;
        turnPos_ = new Vector3(0, -190, 0);
    }

    // Update is called once per frame
    void Update()
    {
        image_.rectTransform.anchoredPosition += moveVec_ * Time.unscaledDeltaTime / 0.5f;

        if (moveVec_.y > 0)
        {
            if(image_.rectTransform.anchoredPosition.y >= turnPos_.y)
            {
                moveVec_.y *= -1;
            }
        }
        else
        {
            if (image_.rectTransform.anchoredPosition.y <= startPos_.y)
            {
                moveVec_.y *= -1;
            }
        }
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
            image_ = null;
        }

        // �A���}�l�[�W���\�[�X�̉���������L�q


        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(_disposing);
    }
}
