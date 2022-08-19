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
    [Header("StormEffectçƒê∂à íuèúñ@")]
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
