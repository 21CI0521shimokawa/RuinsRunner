using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;

public static class StaticInterfaceManager
{
    #region ���S��
    /// <summary>
    /// ����|���v�����󂯖��߂���
    /// �v���C���[���ڐG����gameObject���Q�Ɠn�����Ďg��
    /// </summary>
    /// <param name="_pillar"></param>
    static public void ToFallOverPillar(ref GameObject _pillar)
    {
        IToFallenOver obj = _pillar.GetComponent(typeof(IToFallenOver)) as IToFallenOver;
        if (obj == null) return;
        obj.CallToFallOver();
    }

    /// <summary>
    /// �U���v�����󂯖��߂���
    /// �U�����鑤���U���Ώۂ�gameObject��n���Ďg��
    /// </summary>
    static public void CauseDamage(GameObject _object)
    {
        IDamaged obj = _object.GetComponent(typeof(IDamaged)) as IDamaged;
        if (obj == null) return;
        obj.CallReceiveDamage();
    }

    /// <summary>
    /// �J�����̈ړ��𖽗߂���
    /// �ړI�n��n���Ďg��
    /// </summary>
    /// <param name="_destination"></param>
    /// <param name="_newTarget"></param>
    static public void MoveCamera(Vector3 _destination, GameObject _newTarget = null)
    {
        ICameraMoveTest obj = Camera.main.GetComponent(typeof(ICameraMoveTest)) as ICameraMoveTest;
        if (obj == null) return;
        obj.CallCameraMove(_destination, _newTarget);
    }

    /// <summary>
    /// �v���C���[��Z���W�e�[�u�����ړ�������
    /// </summary>
    /// <param name="_moveAmount"></param>
    /// <param name="_player"></param>
    static public void MovePlayerZ(int _moveAmount, PlayerController _player)
    {
        IMovePlayer obj = _player.GetComponent(typeof(IMovePlayer)) as IMovePlayer;
        if (obj == null) return;
        obj.MovePlayer(_moveAmount);
    }

    /// <summary>
    /// �Q�[���V�[�����~�j�Q�[���ɐ؂�ւ���
    /// </summary>
    /// <param name="_gameState"></param>
    /// <param name="_sceneManagerMain"></param>
    static public void SwitchRunToMG(GameState _gameState, SceneManagerMain _sceneManagerMain)
    {
        ISwitchRunToMG obj = _sceneManagerMain.GetComponent(typeof(ISwitchRunToMG)) as ISwitchRunToMG;
        if (obj == null) return;
        obj.SwitchMiniGame(_gameState);
    }

    /// <summary>
    /// �X�R�A�̉��Z
    /// </summary>
    /// <param name="_addScore"></param>
    /// <param name="_sceneManagerMain"></param>
    static public void UpdateScore(int _addScore)
    {
        GameObject scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        IUpdateScore obj = scoreManager.GetComponent(typeof(IUpdateScore)) as IUpdateScore;
        if (obj == null) return;
        obj.UpdateScore(_addScore);
    }

    static public void UpdateCoinCount(int _addValue = 1)
    {
        GameObject scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        IUpdateCoinCount obj = scoreManager.GetComponent(typeof(IUpdateCoinCount)) as IUpdateCoinCount;
        if (obj == null) return;
        obj.UpdateCoinCount(_addValue);
    }

    static public void ExitGame()
    {
        GameObject sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        IExitGame obj = sceneManager.GetComponent(typeof(IExitGame)) as IExitGame;
        if (obj == null) return;
        obj.ExitToResult();
    }
    /// <summary>
    /// Player�������痎�������ɃJ������Z���𒲐�
    /// </summary>
    /// <param name="CameraObject"></param>
    static public void EditingCameraPositon(GameObject CameraObject)
    {
        IEditingCameraPositon Object = CameraObject.GetComponent(typeof(IEditingCameraPositon)) as IEditingCameraPositon;
        if (Object == null) return;
        Object.DoEditingCameraPositon(CameraObject);
    }
    #endregion
    #region �S���ӏ�
    /// <summary>
    /// EnemyAttack�Q�[�����n�܂������̃J�����̓���
    /// </summary>
    /// <param name="CameraObject">�J�����I�u�W�F�N�g�擾</param>
    static public void DoEnemyAttackMove(GameObject CameraObject)
    {
        ICameraEnemyFromBackMove Objct = CameraObject.GetComponent(typeof(ICameraEnemyFromBackMove)) as ICameraEnemyFromBackMove;
        if (Objct == null) return;
        Objct.DoEnemyFromBackMove(CameraObject);
    }
    /// <summary>
    /// Default�̈ʒu�ɖ߂�J�������[�u
    /// </summary>
    /// <param name="CameraObject">�J�����I�u�W�F�N�g�擾</param>
    static public void DoReturnCameraMove(GameObject CameraObject)
    {
        IReturnDefaultCameraPositonMove Object = CameraObject.GetComponent(typeof(IReturnDefaultCameraPositonMove)) as IReturnDefaultCameraPositonMove;
        if (Object == null) return;
        Object.DoDefaultCameraPositonMove(CameraObject);
    }
    /// <summary>
    /// �O��������ł���I�u�W�F�N�g�������Q�[���J�n
    /// </summary>
    static public void AvoidGameStart()
    {
        GameObject AvoidGameManeger = GameObject.FindGameObjectWithTag("AvoidManeger");
        IAvoidGame Object=AvoidGameManeger.GetComponent(typeof(IAvoidGame))as IAvoidGame;
        if (Object == null) return;
        Object.DoAvoidGame();
    }
    /// <summary>
    /// ��������������̏W�����G�t�F�N�g�Đ�
    /// </summary>
    static public void PlayConcentrationLineEffect()
    {
        GameObject EffectManeger = GameObject.FindGameObjectWithTag("EffectManeger");
        IGetMeet Object = EffectManeger.GetComponent(typeof(IGetMeet)) as IGetMeet;
        if (Object == null) return;
        Object.PlayConcentrationLine();
    }
    /// <summary>
    /// �W�����G�t�F�N�g��~
    /// </summary>
    static public void StopConcentrationLineEffect()
    {
        GameObject EffectManeger = GameObject.FindGameObjectWithTag("EffectManeger");
        IStopConcentrationLineEffect Object = EffectManeger.GetComponent(typeof(IStopConcentrationLineEffect)) as IStopConcentrationLineEffect;
        if (Object == null) return;
        Object.StopConcentrationLine();
    }
    /// <summary>
    /// EnemyAttack���̃G�t�F�N�g�Đ�
    /// </summary>
    static public void PlayEnemyStormEffect()
    {
        GameObject EffectManeger = GameObject.FindGameObjectWithTag("EffectManeger");
        ICreateStormEffect Object = EffectManeger.GetComponent(typeof(ICreateStormEffect)) as ICreateStormEffect;
        if (Object == null) return;
        Object.PlayStormEffect();
    }
    /// <summary>
    /// EnemyAttack���̃G�t�F�N�g��~
    /// </summary>
    static public void StopEnemyStormEffect()
    {
        GameObject EffectManeger = GameObject.FindGameObjectWithTag("EffectManeger");
        IStopStormEffect Object = EffectManeger.GetComponent(typeof(IStopStormEffect)) as IStopStormEffect;
        if (Object == null) return;
        Object.StopStormEffect();
    }
    #endregion
}