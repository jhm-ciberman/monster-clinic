using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Gameplay,
    Outro
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CameraController cameraController;

    public ItemPreview itemPreview;

    private GameState _gameState = GameState.Intro;

    private CameraEffect[] _cameraEffects = new CameraEffect[]
    {
        CameraEffect.Intoxicated,
        CameraEffect.Intoxicated,
        CameraEffect.Intoxicated,
        CameraEffect.Drunk,
        CameraEffect.Drunk,
        CameraEffect.Drunk,
        CameraEffect.Drunk,
        CameraEffect.Drunk,
        CameraEffect.Mushrooms,
        CameraEffect.Mushrooms,
        CameraEffect.Mushrooms,
        CameraEffect.Mushrooms,
        CameraEffect.Mushrooms,
    };

    private int _cameraEffectIndex = 0;

    private void ChangeCameraEffect()
    {
        this._cameraEffectIndex = (this._cameraEffectIndex + 1) % this._cameraEffects.Length;
        this.cameraController.CameraEffect = this._cameraEffects[this._cameraEffectIndex];
    }

    private float _cameraEffectChangeTimeout = 0f;
    private float _cameraEffectChangeInterval = 1f;

    public void Start()
    {
        Instance = this;

        itemPreview.AnimationCompleted += this.ItemPreview_AnimationCompleted;

        DOVirtual.DelayedCall(2f, () =>
        {
            this.cameraController.GoToGameplay(
                () => itemPreview.StartAnimation()
            );
        });
    }

    public void OnWin()
    {
        if (_gameState == GameState.Outro) return;
        this._gameState = GameState.Outro;

        this.cameraController.GoToIntro();
        this.cameraController.StopMovement();

        this.cameraController.CameraEffect = CameraEffect.None;
    }

    private void ItemPreview_AnimationCompleted(object sender, EventArgs e)
    {
        // Start game
        this._gameState = GameState.Gameplay;
        this.cameraController.StartMovement();
    }

    public void NotifyPlayerDied()
    {
        Debug.Log("Game Over!");

        DOVirtual.DelayedCall(2f, () =>
        {
            this.Restart();
        });
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.Restart();
        }

        if (this._gameState == GameState.Gameplay)
        {
            this._cameraEffectChangeTimeout -= Time.deltaTime;
            if (this._cameraEffectChangeTimeout <= 0f)
            {
                this._cameraEffectChangeTimeout = this._cameraEffectChangeInterval;
                this.ChangeCameraEffect();
            }
        }
    }
}