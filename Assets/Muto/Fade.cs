using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fade
{
    public Tween FadeIn(Image image, float speed, float delay, Ease ease)
    {
        var c = image.color;
        c.a = 1;
        image.color = c;

        return image.DOFade(0f, speed)
             .SetDelay(delay)
             .SetEase(ease);
    }
    public Tween FadeOut(Image image, float speed, float delay, Ease ease)
    {
        var c = image.color;
        c.a = 0;
        image.color = c;

        return image.DOFade(1f, speed)
            .SetDelay(delay)
            .SetEase(ease);
    }
}
