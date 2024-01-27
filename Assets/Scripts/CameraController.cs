using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;
using System;

public interface ICameraEffect
{
    float Weight { get; set; }
    Vector2 CameraPosition { get; }
    float CameraRotation { get; }
}

public enum CameraEffect
{
    None,
    Drunk,
    Intoxicated,
    Mushrooms
}

public class CameraController : MonoBehaviour
{
    private class DummyCameraEffect : ICameraEffect
    {
        public float Weight { get; set; } = 0f;
        public Vector2 CameraPosition { get; } = Vector2.zero;
        public float CameraRotation { get;} = 0f;
    }

    public Camera mainCamera;

    // Only as reference for position
    public Camera introCamera;
    public Camera gameplayCamera;
    public float transitionTime = 2.5f;

    public float cameraEffectTransitionTime = 1f;

    public PostProcessVolume volume;


    private ICameraEffect _currentCameraEffect;

    private ICameraEffect _previousCameraEffect;

    private DummyCameraEffect _dummyCameraEffect = new DummyCameraEffect();
    
    [SerializeField]
    public CameraEffectDrunk _cameraEffectDrunk;

    [SerializeField]
    public CameraEffectIntoxicated _cameraEffectIntoxicated;

    [SerializeField]
    public CameraEffectMushrooms _cameraEffectMushrooms;

    private Transform _cameraContainer;

    public void Start()
    {
        this._currentCameraEffect = this._dummyCameraEffect;
        this._previousCameraEffect = this._dummyCameraEffect;

        this._cameraContainer = this.mainCamera.transform.parent;

        this._dummyCameraEffect.Weight = 1f;
        this._cameraEffectDrunk.Weight = 0f;
        this._cameraEffectIntoxicated.Weight = 0f;
        this._cameraEffectMushrooms.Weight = 0f;

        this.introCamera.gameObject.SetActive(false);
        this.gameplayCamera.gameObject.SetActive(false);

        this.TeleportToIntro();
    }

    public void TeleportToIntro()
    {
        this.mainCamera.transform.position = this.introCamera.transform.position;
    }

    public void GoToIntro()
    {
        var target = this.introCamera.transform.position;
        this.mainCamera.transform.DOMove(target, this.transitionTime).SetEase(Ease.InOutSine);
    }

    public void GoToGameplay()
    {
        var target = this.gameplayCamera.transform.position;
        this.mainCamera.transform.DOMove(target, this.transitionTime).SetEase(Ease.InOutSine);
    }

    
    private CameraEffect _cameraEffect = CameraEffect.None;

    public CameraEffect CameraEffect
    {
        get => this._cameraEffect;
        set
        {
            if (this._cameraEffect == value) return;

            this._cameraEffect = value;

            this._previousCameraEffect = this._currentCameraEffect;
            this._currentCameraEffect = value switch
            {
                CameraEffect.None => this._dummyCameraEffect,
                CameraEffect.Drunk => this._cameraEffectDrunk,
                CameraEffect.Intoxicated => this._cameraEffectIntoxicated,
                CameraEffect.Mushrooms => this._cameraEffectMushrooms,
                _ => throw new NotImplementedException()
            };

            Debug.Log($"Switching to {this._currentCameraEffect.GetType().Name}");

            DOTween.To(
                () => this._previousCameraEffect.Weight,
                (x) => this._previousCameraEffect.Weight = x,
                0,
                this.cameraEffectTransitionTime
            ).SetEase(Ease.InOutSine);
            
            DOTween.To(
                () => this._currentCameraEffect.Weight,
                (x) => this._currentCameraEffect.Weight = x,
                1,
                this.cameraEffectTransitionTime
            ).SetEase(Ease.InOutSine);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.CameraEffect = CameraEffect.None;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            this.CameraEffect = CameraEffect.Drunk;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            this.CameraEffect = CameraEffect.Intoxicated;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            this.CameraEffect = CameraEffect.Mushrooms;
        }

        var currentPos = this._currentCameraEffect.CameraPosition;
        var previousPos = this._previousCameraEffect.CameraPosition;
        var pos = Vector2.Lerp(previousPos, currentPos, this._currentCameraEffect.Weight);

        var currentRot = this._currentCameraEffect.CameraRotation;
        var previousRot = this._previousCameraEffect.CameraRotation;
        var rot = Mathf.Lerp(previousRot, currentRot, this._currentCameraEffect.Weight);

        this._cameraContainer.localPosition = new Vector3(pos.x, pos.y, this._cameraContainer.localPosition.z);
        this._cameraContainer.localRotation = Quaternion.Euler(0, 0, rot);
    }
}
