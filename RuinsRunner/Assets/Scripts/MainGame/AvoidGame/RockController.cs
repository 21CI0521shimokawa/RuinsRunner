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
            .OnUpdate(() =>
            {
                transform.DORotate(new Vector3(0, 0, -360), 1, RotateMode.LocalAxisAdd)
                         .SetEase(RockEaseType)
                         .SetLoops(-1);
            })
            .OnComplete(() =>
            {
                Destroy(this.gameObject);
            })
            .SetEase(RockEaseType);
    }
    private void HitProcessingWithPlayer()
    {
        this.OnTriggerEnterAsObservable()
            .Select(collison => collison.tag)
            .Where(tag => tag == "Player")
            .Subscribe(collision =>
            {
                StaticInterfaceManager.UpdateScore(-100);
                Destroy(gameObject);
            });
    }
}
