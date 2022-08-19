using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManagerAttachment : MonoBehaviour
{
    public delegate void MonoEvent();
    MonoEvent _onUpdateCallback;

    [SerializeField] Image _fadePanel;
    [SerializeField]float _fadeSpeed = 1;

    [SerializeField] Text _countDownText;

    [SerializeField] float _gameTimeLimit = 60;

    [SerializeField] string _resultSceneName = "Result";

    public float GameTime => _gameTimeLimit;

    private void Awake()
    {
        GameManager.Instance.Setup(this);

        var fade = new Fade();

        _fadePanel.transform.localScale = new Vector3(-1f, 1f, 1f);

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

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += SceneLoad;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= SceneLoad;
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

    void SceneLoad()
    {
        var fade = new Fade();

        _fadePanel.transform.localScale = new Vector3(1f, 1f, 1f);
        _fadePanel.raycastTarget = true;

        var tween = fade.FadeOut(_fadePanel, _fadeSpeed, 1, Ease.Linear);

        tween.OnComplete(() =>
        {
            SceneManager.LoadScene(_resultSceneName);
        });
    }
    private void OnDestroy()
    {
        //ここ一旦仮で
        //GameManager.Instance.Destroy();
    }
}
