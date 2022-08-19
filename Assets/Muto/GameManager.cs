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
    /// SingletonGameManagerの読み取りプロパティ
    /// </summary>
    public static GameManager Instance => instance;

    /// <summary>
    /// Pauseのアクション
    /// </summary>
    public event Action OnPause;
    /// <summary>
    /// Resemeのアクション
    /// </summary>
    public event Action OnResume;
    /// <summary>
    /// ゲームオーバー時にさせたい処理を追加
    /// </summary>
    public event Action OnGameOver;

    bool _isPause;
    public bool _isGameStart = true;

    /// <summary>
    /// ゲームのスコア
    /// </summary>
    IntReactiveProperty _gameScore;
    IntReactiveProperty _missCount;

    FloatReactiveProperty _timer;

    public IReactiveProperty<float> GameTimer => _timer;

    public IReactiveProperty<int> Score => _gameScore;
    public IReactiveProperty<int> MissCount => _missCount;

    public void Setup(GameManagerAttachment attachment)
    {
        //アタッチメント側のCallbackに登録
        attachment.SetupCallback(OnUpdate);

        //Pause処理を登録
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
            if(_isPause)    //Pause解除
            {
                OnResume.Invoke();
            }
            else 　　//Pause
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
                Debug.Log("ゲーム終了");
            }
        }
    }

    /// <summary>
    /// スコア加算の関数
    /// </summary>
    /// <param name="i"></param>
    public void IncreaseScore(int i)
    {
        if(i <= 0)
        {
            return;
        }

        _gameScore.Value += i;

        Debug.Log($"スコアを加算しました : Score [{_gameScore.Value}]");
    }

    /// <summary>
    /// スコア減算の関数m
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

        Debug.Log($"スコアを減算しました : Score [{_gameScore.Value}] Miss [{_missCount.Value}]");
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
    /// ゲームデータの初期化
    /// </summary>
    public void Reset()
    {
        //スコアを初期化
        _missCount = null;
        _gameScore = null;

        _missCount = new IntReactiveProperty(0);
        _gameScore = new IntReactiveProperty(0);
    }
}
