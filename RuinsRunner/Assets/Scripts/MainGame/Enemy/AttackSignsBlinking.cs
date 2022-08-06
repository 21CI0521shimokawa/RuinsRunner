using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// �U���\��
/// </summary>
public class AttackSignsBlinking : MonoBehaviour
{
    [Header("�ݒ�")]
    [SerializeField] float BlinkingTime;
    [SerializeField] Renderer Renderer;
    void Start()
    {
        this.Renderer.material.DOColor(Color.red, BlinkingTime).SetLoops(-1, LoopType.Yoyo);
    }
}
