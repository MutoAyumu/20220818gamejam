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

    /// <summary>
    /// �Q�[���̃X�R�A
    /// </summary>
    Score _gameScore;

    public void Setup(GameManagerAttachment attachment)
    {
        //�A�^�b�`�����g����Callback�ɓo�^
        attachment.SetupCallback(OnUpdate);

        //Pause������o�^
        OnPause += Pause;
        OnResume += Resume;

        _gameScore = new Score();
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

    public void Destroy()
    {
        //�X�R�A��������
        _gameScore = null;
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
