using UnityEngine;
using DG.Tweening;
using System;

public class ItemPreview : MonoBehaviour
{

    public event EventHandler AnimationCompleted;

    public void Start()
    {
        // Go from botton to the center of the screen, shake a little (angle only), and then go back to the bottom

        float initialY = -12;

        var target = this.transform.position;
        target.y = initialY;

        this.transform.position = target;

        this.transform
            .DOMoveY(0, 1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => this.transform
                .DOShakeRotation(1, new Vector3(0, 0, 10), 10, 90, false)
                .OnComplete(() => this.transform
                    .DOMoveY(initialY, 1f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => this.AnimationCompleted?.Invoke(this, EventArgs.Empty))
                )
            );
    }
}