using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MVPPresenter : MonoBehaviour
{
    [SerializeField] MVPText _timerText;
    [SerializeField] MVPText _scoreText;
    GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;

        if (_timerText)
        {
            _gameManager.GameTimer.Subscribe(x =>
            {
                _timerText.SetText(((int)(x / 60)).ToString() + ":" + ((int)(x % 60)).ToString("00"));
            }).AddTo(this);
        }

        if(_scoreText)
        {
            _gameManager.Score.Subscribe(x =>
            {
                _scoreText.SetText(x.ToString("00000"));
            }).AddTo(this);
        }
    }
}
