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
    bool _isPause;

    float _timer;
    [SerializeField] float _timeLimit = 2f;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnResume += Resume;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnPause -= Pause;
        GameManager.Instance.OnResume -= Resume;
    }

    private void Update()
    {
        if (_isPause || _isJudge) return;

        _timer += Time.deltaTime;

        if(_timer >= _timeLimit)
        {
            _isJudge = true;
            _anim.SetTrigger("Judge");
        }    
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

    void Pause()
    {
        _isPause = true;
        //Animationがあれば止める
    }
    void Resume()
    {
        _isPause = false;
    }

    enum JudgeType
    {
        /// <summary>ナンパがうれしいとき</summary>
        good,
        /// <summary>ナンパがいやなとき </summary>
        bad,
    }
}
