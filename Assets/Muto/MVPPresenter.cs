using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MVPPresenter : MonoBehaviour
{
    [SerializeField] MVPText _timerText;
    GameManager _gameManager;

    private void Start()
    {
        if (_timerText)
        {
            _gameManager = GameManager.Instance;

            _gameManager.GameTimer.Subscribe(x =>
            {
                _timerText.SetText(((int)(x / 60)).ToString() + ":" + ((int)(x % 60)).ToString("00"));
            }).AddTo(this);
        }
    }
}
