using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    /// <summary>
    /// ゲームのスコア
    /// </summary>
    Score _gameScore;

    public void Setup(GameManagerAttachment attachment)
    {
        //アタッチメント側のCallbackに登録
        attachment.SetupCallback(OnUpdate);

        //Pause処理を登録
        OnPause += Pause;
        OnResume += Resume;

        _gameScore = new Score();
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
    }

    /// <summary>
    /// スコア加算の関数
    /// </summary>
    /// <param name="i"></param>
    public void AddScore(int i)
    {
        if(i <= 0)
        {
            return;
        }

        _gameScore._score += i;

        Debug.Log($"スコアを加算しました : Score [{_gameScore._score}]");
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

        if (_gameScore._score > 0)
        {
            _gameScore._score += i;

            if(_gameScore._score < 0)
            {
                _gameScore._score = 0;
            }
        }
        _gameScore._miss += 1;

        Debug.Log($"スコアを減算しました : Score [{_gameScore._score}] Miss [{_gameScore._miss}]");
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
    public void Destroy()
    {
        //スコアを初期化
        _gameScore = null;
    }
}

[System.Serializable]
public class Score
{
    //ここにスコアで使いたい変数を定義
    public int _score;
    public int _miss;

    //Scoreクラスのコンストラクタ
    public Score()
    {
        _score = 0;
        _miss = 0;
    }
}
