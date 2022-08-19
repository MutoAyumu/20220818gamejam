using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UniRx;

public class GameManager
{
    static GameManager instance = new GameManager();

    /// <summary>
    /// SingletonGameManager�̓ǂݎ��v���p�e�B
    /// </summary>
    public static GameManager Instance => instance;

    /// <summary>
    /// Pause�̃A�N�V����
    /// </summary>
    public event Action OnPause;
    /// <summary>
    /// Reseme�̃A�N�V����
    /// </summary>
    public event Action OnResume;
    /// <summary>
    /// �Q�[���I�[�o�[���ɂ�������������ǉ�
    /// </summary>
    public event Action OnGameOver;

    bool _isPause;
    public bool _isGameStart = true;

    /// <summary>
    /// �Q�[���̃X�R�A
    /// </summary>
    IntReactiveProperty _gameScore;
    IntReactiveProperty _missCount;

    FloatReactiveProperty _timer;

    public IReactiveProperty<float> GameTimer => _timer;

    public IReactiveProperty<int> Score => _gameScore;
    public IReactiveProperty<int> MissCount => _missCount;

    public void Setup(GameManagerAttachment attachment)
    {
        //�A�^�b�`�����g����Callback�ɓo�^
        attachment.SetupCallback(OnUpdate);

        //Pause������o�^
        OnPause += Pause;
        OnResume += Resume;

        _timer = new FloatReactiveProperty(0);
        _timer.Value = attachment.GameTime;

        Reset();
    }
    void OnUpdate()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(_isPause)    //Pause����
            {
                OnResume.Invoke();
            }
            else �@�@//Pause
            {
                OnPause.Invoke();
            }
        }

        if(!_isGameStart)
        {
            _timer.Value -= Time.deltaTime;

            if(_timer.Value < 0)
            {
                OnGameOver.Invoke();
                _isGameStart = true;
                Debug.Log("�Q�[���I��");
            }
        }
    }

    /// <summary>
    /// �X�R�A���Z�̊֐�
    /// </summary>
    /// <param name="i"></param>
    public void IncreaseScore(int i)
    {
        if(i <= 0)
        {
            return;
        }

        _gameScore.Value += i;

        Debug.Log($"�X�R�A�����Z���܂��� : Score [{_gameScore.Value}]");
    }

    /// <summary>
    /// �X�R�A���Z�̊֐�m
    /// </summary>
    /// <param name="i"></param>
    public void DecreaseScore(int i)
    {
        if(i >= 0)
        {
            return;
        }

        if (_gameScore.Value > 0)
        {
            _gameScore.Value += i;

            if(_gameScore.Value < 0)
            {
                _gameScore.Value = 0;
            }
        }
        _missCount.Value += 1;

        Debug.Log($"�X�R�A�����Z���܂��� : Score [{_gameScore.Value}] Miss [{_missCount.Value}]");
    }

    void Pause()
    {
        _isPause = true;
        Debug.Log("Pause");
    }
    void Resume()
    {
        _isPause = false;
        Debug.Log("Resume");
    }

    /// <summary>
    /// �Q�[���f�[�^�̏�����
    /// </summary>
    public void Reset()
    {
        //�X�R�A��������
        _missCount = null;
        _gameScore = null;

        _missCount = new IntReactiveProperty(0);
        _gameScore = new IntReactiveProperty(0);
    }
}
