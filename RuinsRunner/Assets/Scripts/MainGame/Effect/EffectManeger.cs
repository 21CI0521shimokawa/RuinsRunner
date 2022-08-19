using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManeger
    : MonoBehaviour
    ,IGetMeet
    ,ICreateStormEffect
    ,IStopStormEffect
{
    [Header("Effect")]
    [SerializeField] ParticleSystem EnemyStorm;
    [SerializeField] ParticleSystem ConcentrationLine;
    [Header("StormEffect再生位置除法")]
    [SerializeField] Transform EnemyTransform;
    void Update()
    {
    }
    public void PlayConcentrationLine()
    {
        ConcentrationLine.Play();
    }

    public void PlayStormEffect()
    {
        EnemyStorm.Play();
    }

    public void StopStormEffect()
    {
        EnemyStorm.Stop();
    }
}
