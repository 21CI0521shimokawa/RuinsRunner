using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameQTE_ImageRender : ObjectSuperClass
{
    MiniGameQTEManager manager_;
    GameObject canbas_;
    [SerializeField] GameObject[] imagePrefabs_;
    [SerializeField] GameObject arrowSymbolPrefab_;

    List<Image> buttonImages_;      //������摜�̃��X�g
    Image arrowSymbol_;             //���
    [SerializeField] Slider timeGauge_;              //�^�C���Q�[�W

    const int imageSize_ = 60;      //�摜�T�C�Y

    bool isInitializeProcessing_;   //�������������I���������ǂ���

    // Start is called before the first frame update
    void Start()
    {
        isInitializeProcessing_ = false;

        manager_ = gameObject.GetComponent<MiniGameQTEManager>();
        canbas_ = GameObject.FindGameObjectWithTag("QTECanbas");
        buttonImages_ = new List<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //�������������s���Ă��Ȃ����ǂ���
        if(!this.isInitializeProcessing_)
        {
            //Manager�̏��������I����Ă��邩�ǂ���
            if (manager_.isInitializeProcessing)
            {
                //����������
                ImagesInitialization();
                isInitializeProcessing_ = true;
            }
            else
            {
                return;
            }
        }

        //�X�V
        timeGauge_.value = Mathf.InverseLerp(manager_.timeLinitMax, 0, manager_.unscaledTimeCount);
    }

    //����������
    void ImagesInitialization()
    {
        Vector3 instanceatePos = new Vector3(0, -80, 0);
        for(int i = 0; i < manager_.buttons.Count; ++i)
        {
            //������S�Ẳ摜�����ɂ��炷
            foreach(Image image in buttonImages_)
            {
                image.rectTransform.anchoredPosition -= new Vector2(imageSize_, 0);
            }

            //����
            {
                Image newImage = Instantiate(imagePrefabs_[(int)manager_.buttons[i]], Vector3.zero, Quaternion.identity, canbas_.transform).GetComponent<Image>();
                newImage.rectTransform.anchoredPosition = instanceatePos;
                buttonImages_.Add(newImage);
            }

            instanceatePos.x += 60;
        }

        //���̐���
        {
            Image newImage = Instantiate(arrowSymbolPrefab_, Vector3.zero, Quaternion.identity, canbas_.transform).GetComponent<Image>();
            arrowSymbol_ = newImage;
            newImage.rectTransform.Rotate(new Vector3(0, 0, 90));
            ArrowSymbolPotisionChange();
        }

        //�^�C���Q�[�W�̐���
        //{
        //    Slider slider = Instantiate(timeGaugePrefab_, Vector3.zero, Quaternion.identity, canbas_.transform).GetComponent<Slider>();
        //    timeGauge_ = slider;
        //    //slider.gameObject.rectTransform.anchoredPosition = new Vector2(0, 100);
        //}
    }

    public void ArrowSymbolPotisionChange()
    {
        arrowSymbol_.rectTransform.anchoredPosition = new Vector2(buttonImages_[manager_.buttonNumber].rectTransform.anchoredPosition.x, -200);
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
            manager_ = null;
            canbas_ = null;
            imagePrefabs_ = null;
            arrowSymbolPrefab_ = null;
            //timeGaugePrefab_ = null;
            buttonImages_ = null;
            arrowSymbol_ = null;
            timeGauge_ = null;
        }

        // �A���}�l�[�W���\�[�X�̉���������L�q
        isInitializeProcessing_ = false;

        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(_disposing);
    }
}
