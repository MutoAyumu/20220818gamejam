using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultSetup : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _missText;

    private void Awake()
    {
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

        var data = GameManager.Instance.GameData;
        SetText(_scoreText, data._score);
        SetText(_missText, data._miss);
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
                .SetDelay(0.5f));
    }
}
