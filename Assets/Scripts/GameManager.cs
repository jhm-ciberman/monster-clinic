using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CameraController cameraController;

    public ItemPreview itemPreview;

    private int _livesCount = 3;

    private int _currentLevel = 0;

    public void Start()
    {
        Instance = this;

        itemPreview.AnimationCompleted += this.ItemPreview_AnimationCompleted;

        DOVirtual.DelayedCall(2f, () =>
        {
            this.cameraController.GoToGameplay(1,
                () => itemPreview.StartAnimation()
            );
        });

    }

    public void OnLevelComplete(int level)
    {
        if (level <= this._currentLevel) return;
        this._currentLevel = level;

        if (level == 4)
        {
            Debug.Log("You win!!");
            this.cameraController.GoToIntro();
            return;
        }

        this.cameraController.GoToGameplay(level);
    }

    private void ItemPreview_AnimationCompleted(object sender, EventArgs e)
    {
        
    }

    public void NotifyPlayerDied()
    {
        this._livesCount--;
        Debug.Log($"Lives: {this._livesCount}");

        if (this._livesCount <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}