using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlsStatusManager : MonoBehaviour
{
    [SerializeField]JudgeType _state;
    [SerializeField] int _score = 0;
    EnemGenerator _eg;
    Animator _anim;
    bool _isJudge;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Judge()
    {
        if(_isJudge)
        {
            return;
        }
        if(_state == JudgeType.good)
        {
            GameManager.Instance.IncreaseScore(_score);
        }
        else if(_state == JudgeType.bad)
        {
            GameManager.Instance.DecreaseScore(_score);
        }

        _isJudge = true;

        _anim.SetTrigger("Judge");
    }

    public void Set(EnemGenerator eg)
    {
        _eg = eg;
    }

    /// <summary>animation triger で呼ぶ</summary>
    public void Destroy()
    {
        _eg.Test2(transform.position);
        Destroy(gameObject);
    }

    enum JudgeType
    {
        /// <summary>ナンパがうれしいとき</summary>
        good,
        /// <summary>ナンパがいやなとき </summary>
        bad,
    }
}
