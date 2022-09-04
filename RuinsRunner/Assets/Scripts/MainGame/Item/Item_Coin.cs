using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Coin : ItemBase
{
    [SerializeField] GameObject child_;

    [SerializeField] AudioClip audioClip_;

    [SerializeField] Material material_;

    [SerializeField] float intensity_;

    ScoreManager scoreManager_;

    [SerializeField] Color normalColor_;
    [SerializeField] Color scoreUpColor_;

    private void Start()
    {
        scoreManager_ = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        child_.transform.Rotate(new Vector3(0, 120 * Time.deltaTime, 0));

        //intensity分の計算
        float factor = Mathf.Pow(2, intensity_);

        if (scoreManager_.scoreUpTime > 0.0f)
        {
            // マテリアルの変更するパラメータを事前に知らせる
            material_.EnableKeyword("_EMISSION");

            //特殊
            //material_.color = new Color(0.8039216f, 0.01568628f, 0.03210715f);
            material_.SetColor("_EmissionColor", scoreUpColor_ * factor);
        }
        else
        {
            // マテリアルの変更するパラメータを事前に知らせる
            material_.EnableKeyword("_EMISSION");

            //通常
            //material_.color = new Color(0.6037736f, 0.4345016f, 0.01423994f);
            material_.SetColor("_EmissionColor", normalColor_ * factor);
        }

    }

    protected override void TakenItem(GameObject _player)
    {
        StaticInterfaceManager.UpdateScore(100);
        StaticInterfaceManager.UpdateCoinCount();
        PlayAudio.PlaySE(audioClip_);
    }

    protected override void ReleaseProcess_ManagedResource()
    {
        audioClip_ = null;
    }

    protected override void ReleaseProcess_UnManagedResource()
    {

    }
}
