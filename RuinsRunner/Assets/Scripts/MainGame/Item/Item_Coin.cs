using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Coin : ItemBase
{
    [SerializeField] AudioClip audioClip_;

    [SerializeField] Material material_;

    ScoreManager scoreManager_;

    private void Start()
    {
        scoreManager_ = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 120 * Time.deltaTime, 0));

        if(scoreManager_.scoreUpTime > 0.0f)
        {
            material_.color = new Color(0.6039216f, 0.01568628f, 0.03210715f);
        }
        else
        {
            material_.color = new Color(0.6037736f, 0.4345016f, 0.01423994f);
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
