using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class RockController : ObjectSuperClass
{
    [Header("–Ú“I’nÝ’è")]
    [SerializeField] float RockDestinationPositonZ;
    [Header("Šâ‚ÌÝ’è")]
    [SerializeField] float RockSpeed;
    [SerializeField] Ease RockEaseType;
    void Start()
    {
        HitProcessingWithPlayer();
        RockMove();
    }
    private void RockMove()
    {
        this.transform.DOMoveZ(RockDestinationPositonZ, RockSpeed)
            .OnComplete(() =>
            {
                Destroy(this.gameObject);
            })
            .SetEase(RockEaseType);
    }
    private void HitProcessingWithPlayer()
    {
        this.OnTriggerEnter2DAsObservable()
            .Select(collison => collison.tag)
            .Where(tag => tag == "Player")
            .Subscribe(collision =>
            {
                StaticInterfaceManager.UpdateScore(-100);
                Destroy(gameObject);
            });
    }
}
