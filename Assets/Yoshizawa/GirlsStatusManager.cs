using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlsStatusManager : MonoBehaviour
{
    [SerializeField]JudgeType _state;
    [SerializeField] int _score = 0;
    EnemGenerator _eg;
    bool _isJudge;

    public void Judge()
    {
        if(_isJudge)
        {
            return;
        }
        if(_state == JudgeType.good)
        {
            GameManager.Instance.AddScore(_score);
        }
        else if(_state == JudgeType.bad)
        {
            GameManager.Instance.DecreaseScore(_score);
        }

        _isJudge = true;
    }

    public void Set(EnemGenerator eg)
    {
        _eg = eg;
    }

    /// <summary>animation triger �ŌĂ�</summary>
    public void Destroy()
    {
        _eg.Test2(transform.position);
        Destroy(gameObject);
    }

    enum JudgeType
    {
        /// <summary>�i���p�����ꂵ���Ƃ�</summary>
        good,
        /// <summary>�i���p������ȂƂ� </summary>
        bad,
    }
}
