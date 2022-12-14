using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultSetup : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _missText;

    [SerializeField] Button _nextButton;
    [SerializeField] Button _titleButton;

    [SerializeField] Image _fadePanel;
    [SerializeField]float _fadeSpeed = 1f; 

    private void Awake()
    {
        if(GameManager.Instance.Score == null || GameManager.Instance.MissCount == null)
        {
            GameManager.Instance.Reset();
        }

        Setup();
    }

    void Setup()
    {
        if (!_scoreText || !_missText)
        {
            Debug.LogError("Textがセットされていません");
        }
        if(!_nextButton)
        {
            Debug.LogError("Buttonがセットされていません");
        }
        if (!_fadePanel)
        {
            Debug.Log("FadePanelがセットされていません");
        }

        _nextButton.gameObject.SetActive(false);
        _titleButton.gameObject.SetActive(false);

        var c1 = _scoreText.color;
        c1.a = 0;
        _scoreText.color = c1;

        var c2 = _missText.color;
        c2.a = 0;
        _missText.color = c2;

        _fadePanel.fillAmount = 1;

        var fade = new Fade();

        var tween = fade.FadeIn(_fadePanel, _fadeSpeed, 1, Ease.Linear);

            tween
            .OnComplete(() =>
            {
                _fadePanel.raycastTarget = false;
                SetText(_scoreText, GameManager.Instance.Score.Value);
                SetText(_missText, GameManager.Instance.MissCount.Value);
            });
    }
    void SetText(Text text, int i)
    {
        text.rectTransform.DOAnchorPos(new Vector2(0, 50f), 0)
            .SetRelative(true);

        text.text = 0.ToString("00000");
        var c = text.color;
        c.a = 0;
        text.color = c;

        text.rectTransform.DOAnchorPos(new Vector2(0, -50f), 1f)
            .SetEase(Ease.InQuart)
            .SetRelative(true);

        DOTween.Sequence()
            .Append(text.DOFade(1, 1f).SetEase(Ease.Linear))
            .Append(DOVirtual.Int(0, i, 0.5f, value => text.text = value.ToString("00000"))
                .SetDelay(0.5f))
            .OnComplete(() =>
            {
                SetButton(_nextButton);
                SetButton(_titleButton);
            });
    }
    void SetButton(Button button)
    {
        button.gameObject.SetActive(true);

        var image = button.gameObject.GetComponent<Image>();
        var text = button.transform.GetChild(0).GetComponent<Text>();

        var c1 = text.color;
        c1.a = 0;
        text.color = c1;
        
        var c2 = image.color;
        c2.a = 0;
        image.color = c2;

        text.DOFade(1, 1f).SetEase(Ease.Linear);
        image.DOFade(1, 1f).SetEase(Ease.Linear);

        var rect = button.GetComponent<RectTransform>();
        rect.DOAnchorPos(new Vector2(0, -50f), 1f)
            .SetEase(Ease.InQuart)
            .SetRelative(true);
    }
}
