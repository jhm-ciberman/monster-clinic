using UnityEngine;
using DG.Tweening;
using System;

public class ItemPreview : MonoBehaviour
{

    public event EventHandler AnimationCompleted;
    
    public void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void StartAnimation()
    {
        
        this.gameObject.SetActive(true);

        // Go from botton to the center of the screen, shake a little (angle only), and then go back to the bottom
        var position = this.transform.position;

        this.transform
            .DOMoveY(position.y + 7f, 1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => this.transform
                .DOShakeRotation(1, new Vector3(0, 0, 10), 10, 90, false)
                .OnComplete(() => this.transform
                    .DOMoveY(position.y, 1f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => this.AnimationCompleted?.Invoke(this, EventArgs.Empty))
                )
            );
    }
}