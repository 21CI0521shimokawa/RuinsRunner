using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Meet : ItemBase
{
    [SerializeField] AudioClip audioClip_;
    
    // Update is called once per frame
    void Update()
    {
    }

    protected override void TakenItem(GameObject _player)
    {
        //_player.GetComponent<PlayerController>().Recovery();
        StaticInterfaceManager.MovePlayerZ(-1, _player.GetComponent<PlayerController>());
        StaticInterfaceManager.UpdateScore(500);
        MoveLooksLikeRunning.moveMagnification = 1.6f;  //�ړ����x�{��
        StaticInterfaceManager.PlayConcentrationLineEffect();
        ScoreManager scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        scoreManager.scoreUpTime = 10.0f;    //���_�A�b�v����

        PlayAudio.PlaySE(audioClip_);
    }

    protected override void ReleaseProcess_ManagedResource()
    {

    }

    protected override void ReleaseProcess_UnManagedResource()
    {

    }
}
