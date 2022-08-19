using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    Score _gameScore;

    public Score GameData => _gameScore;

    public void Setup(GameManagerAttachment attachment)
    {
        //�A�^�b�`�����g����Callback�ɓo�^
        attachment.SetupCallback(OnUpdate);

        //Pause������o�^
        OnPause += Pause;
        OnResume += Resume;

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

        _gameScore._score += i;

        Debug.Log($"�X�R�A�����Z���܂��� : Score [{_gameScore._score}]");
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

        if (_gameScore._score > 0)
        {
            _gameScore._score += i;

            if(_gameScore._score < 0)
            {
                _gameScore._score = 0;
            }
        }
        _gameScore._miss += 1;

        Debug.Log($"�X�R�A�����Z���܂��� : Score [{_gameScore._score}] Miss [{_gameScore._miss}]");
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
        _gameScore = null;
        _gameScore = new Score();
    }
}

[System.Serializable]
public class Score
{
    //�����ɃX�R�A�Ŏg�������ϐ����`
    public int _score;
    public int _miss;

    //Score�N���X�̃R���X�g���N�^
    public Score()
    {
        _score = 0;
        _miss = 0;
    }
}
