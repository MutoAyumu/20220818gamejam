using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fade
{
    public Tween FadeIn(Image image, float speed, float delay, Ease ease)
    {
        return DOVirtual.Float(1f, 0f, speed, value => image.fillAmount = value)
                    .SetDelay(delay)
                    .SetEase(ease);
    }
    public Tween FadeOut(Image image, float speed, float delay, Ease ease)
    {
        return DOVirtual.Float(0f, 1f, speed, value => image.fillAmount = value)
            .SetDelay(delay)
            .SetEase(ease);
    }
}
