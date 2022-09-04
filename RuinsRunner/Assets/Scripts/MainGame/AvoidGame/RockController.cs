using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using System.ComponentModel;
using UnityEngine.Rendering;
using TMPro;
using System.Runtime.CompilerServices;

public class RockController : ObjectSuperClass
{
    [Header("�ړI�n�ݒ�")]
    [SerializeField] float RockDestinationPositonZ;
    [Header("��̐ݒ�")]
    [SerializeField] float RockSpeed;
    [Header("�A�j���[�^�[")] //�H���ǋL 2202/9/5
    [SerializeField] Animator animator;
    [SerializeField] Ease RockEaseType;
    [SerializeField] SkinnedMeshRenderer mesh;
    [SerializeField] BoxCollider boxCollider;
    //���������ł��邩�ǂ���
    bool isAnimEnd = false;
    public bool isAttack = false;
    void Start()
    {
        HitProcessingWithPlayer();
        RockMove();
    }
    private void RockMove()
    {
        this.transform.DOMoveZ(RockDestinationPositonZ, RockSpeed)
            .SetDelay(3)
            .OnStart(() =>
            {
                if (!isAttack)
                {
                    boxCollider.enabled = false;
                    StartCoroutine(DestroyAfterSecond(5.0f));
                }
            })
            .OnUpdate(() =>
            {
                //���ł��Ȃ��^�C�v�Ȃ痣���
                if (!this.isAttack) 
                {
                    Leave();
                    //AdjustAlpha();
                    //InvisibleDestroy();
                }
                else
                {
                    Attack();
                }
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

                //�v���C��������
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Stumble(3);

                Destroy(gameObject);
            });
    }

    private void Leave()
    {
        if (isAnimEnd) return;
        animator.SetBool("Leave", true);
        isAnimEnd = true;
    }
    
    private void Attack()
    {
        if(this.transform.position.z < 15 && !isAnimEnd)
        {
            animator.SetBool("Attack", true);
            isAnimEnd=true;
        }
    }

    private void AdjustAlpha()
    {
        mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    IEnumerator DestroyAfterSecond(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
        yield return null;
    }
}
