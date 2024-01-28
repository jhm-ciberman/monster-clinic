using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;
using System;

public interface ICameraEffect
{
    public float Time { get; set; }
    float Weight { get; set; }
    Vector2 CameraPosition { get; }
    float CameraRotation { get; }
    float CameraSizeScale { get; }
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
        public float CameraSizeScale { get; } = 1f;
        public float Time { get; set; } = 0f;
    }

    public Camera mainCamera;

    // Only as reference for position
    public Camera introCamera;
    public Camera gameplayCamera;

    public float transitionTime = 4f;

    public float cameraEffectTransitionTime = 1f;


    public PostProcessVolume volume;

    [SerializeField]
    private float _cameraMovementVerticalSpeed = 2f;

    private bool _isCameraMoving = false;


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

    private Vector3 _baseCameraPosition;

    private float _baseCameraOrthoSize;

    public void Start()
    {
        this._currentCameraEffect = this._dummyCameraEffect;
        this._previousCameraEffect = this._dummyCameraEffect;

        this._cameraContainer = this.mainCamera.transform.parent;

        this._dummyCameraEffect.Weight = 1f;
        this._cameraEffectDrunk.Weight = 0f;
        this._cameraEffectIntoxicated.Weight = 0f;
        this._cameraEffectMushrooms.Weight = 0f;

        this._baseCameraOrthoSize = this.mainCamera.orthographicSize;
        this._baseCameraPosition = this.mainCamera.transform.localPosition;

        this.TeleportToIntro();

        this.introCamera.gameObject.SetActive(false);
        this.gameplayCamera.gameObject.SetActive(false);
    }

    public void TeleportToIntro()
    {
        this._baseCameraPosition = this.introCamera.transform.position;
        this._baseCameraOrthoSize = this.introCamera.orthographicSize;
    }

    public void StartMovement()
    {
        this._isCameraMoving = true;
    }

    private void GoToCamera(Camera camera, Action onComplete = null)
    {
        var targetPos = camera.transform.position;
        var targetSize = camera.orthographicSize;

        DOTween.To(
            () => this._baseCameraPosition,
            (x) => this._baseCameraPosition = x,
            targetPos,
            this.transitionTime
        ).SetEase(Ease.InOutSine);
            
        DOTween.To(
            () => this._baseCameraOrthoSize,
            (x) => this._baseCameraOrthoSize = x,
            targetSize,
            this.transitionTime
        ).SetEase(Ease.InOutSine)
        .OnComplete(() => onComplete?.Invoke());
    }

    public void GoToIntro()
    {
        this.GoToCamera(this.introCamera);
    }

    public void GoToGameplay(Action onComplete = null)
    {
        this.GoToCamera(this.gameplayCamera, () => onComplete?.Invoke());
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

            this._currentCameraEffect.Time = 0f;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.CameraEffect = CameraEffect.None;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.CameraEffect = CameraEffect.Drunk;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.CameraEffect = CameraEffect.Intoxicated;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.CameraEffect = CameraEffect.Mushrooms;
        }

        this._currentCameraEffect.Time += Time.deltaTime;

        if (this._isCameraMoving)
        {
            this._baseCameraPosition += new Vector3(0, this._cameraMovementVerticalSpeed * Time.deltaTime, 0);
        }


        var currentPos = this._currentCameraEffect.CameraPosition;
        var previousPos = this._previousCameraEffect.CameraPosition;
        var pos = Vector2.Lerp(previousPos, currentPos, this._currentCameraEffect.Weight);

        var currentRot = this._currentCameraEffect.CameraRotation;
        var previousRot = this._previousCameraEffect.CameraRotation;
        var rot = Mathf.Lerp(previousRot, currentRot, this._currentCameraEffect.Weight);

        var currentScale = this._currentCameraEffect.CameraSizeScale;
        var previousScale = this._previousCameraEffect.CameraSizeScale;
        var scale = Mathf.Lerp(previousScale, currentScale, this._currentCameraEffect.Weight);

        var cameraTransform = this.mainCamera.transform;
        cameraTransform.localPosition = this._baseCameraPosition + new Vector3(pos.x, pos.y, this._cameraContainer.localPosition.z);
        cameraTransform.localRotation = Quaternion.Euler(0, 0, rot);
        this.mainCamera.orthographicSize = this._baseCameraOrthoSize * scale;
    }

    internal void StopMovement()
    {
        this._isCameraMoving = false;
    }
}
