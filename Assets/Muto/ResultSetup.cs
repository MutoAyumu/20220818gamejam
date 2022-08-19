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

    [SerializeField] Image _fadePanel;
    [SerializeField]float _fadeSpeed = 1f; 

    private void Awake()
    {
        if(GameManager.Instance.GameData == null)
        {
            GameManager.Instance.Reset();
        }

        //ここテスト
        GameManager.Instance.IncreaseScore(100);
        GameManager.Instance.DecreaseScore(10);
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

        var data = GameManager.Instance.GameData;

        var c1 = _scoreText.color;
        c1.a = 0;
        _scoreText.color = c1;

        var c2 = _missText.color;
        c2.a = 0;
        _missText.color = c2;

        _fadePanel.fillAmount = 1;

        DOVirtual.Float(1f, 0f, _fadeSpeed, value => _fadePanel.fillAmount = value)
            .SetDelay(1)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                SetText(_scoreText, data._score);
                SetText(_missText, data._miss);
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
                SetButton();
            });
    }
    void SetButton()
    {
        _nextButton.gameObject.SetActive(true);

        var image = _nextButton.gameObject.GetComponent<Image>();
        var text = _nextButton.transform.GetChild(0).GetComponent<Text>();

        var c1 = text.color;
        c1.a = 0;
        text.color = c1;
        
        var c2 = image.color;
        c2.a = 0;
        image.color = c2;

        text.DOFade(1, 1f).SetEase(Ease.Linear);
        image.DOFade(1, 1f).SetEase(Ease.Linear);

        var rect = _nextButton.GetComponent<RectTransform>();
        rect.DOAnchorPos(new Vector2(0, -50f), 1f)
            .SetEase(Ease.InQuart)
            .SetRelative(true);
    }
}
