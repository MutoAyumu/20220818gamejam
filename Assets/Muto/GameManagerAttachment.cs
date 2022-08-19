using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManagerAttachment : MonoBehaviour
{
    public delegate void MonoEvent();
    MonoEvent _onUpdateCallback;

    [SerializeField] Image _fadePanel;
    [SerializeField]float _fadeSpeed = 1;

    [SerializeField] Text _countDownText;

    [SerializeField] float _gameTimeLimit = 60;
    public float GameTime => _gameTimeLimit;

    private void Awake()
    {
        GameManager.Instance.Setup(this);

        var fade = new Fade();

        DOTween.Sequence()
            .Append(fade.FadeIn(_fadePanel, _fadeSpeed, 1, Ease.Linear))
            .Append(_countDownText.DOCounter(3, 0, 3f)
            .SetDelay(0.5f))
            .OnComplete(() =>
            {
                GameManager.Instance._isGameStart = false;
                _fadePanel.raycastTarget = false;
                _countDownText.enabled = false;
            });
    }
    /// <summary>
    /// GameManagerのUpdate処理をコールバックに追加m
    /// </summary>
    /// <param name="e"></param>
    public void SetupCallback(MonoEvent e)
    {
        _onUpdateCallback = e;
    }

    private void Update()
    {
        _onUpdateCallback.Invoke();
    }
    private void OnDestroy()
    {
        //ここ一旦仮で
        //GameManager.Instance.Destroy();
    }
}
