using UnityEngine;
using System;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
public class CameraEffectDrunk : MonoBehaviour, ICameraEffect
{
    public float Weight { get; set; } = 0f;

    public Vector2 CameraPosition { get; private set; } = Vector2.zero;
    public float CameraRotation { get; private set; } = 0f;
    public float CameraSizeScale { get; } = 1f;
    public float Time { get; set; } = 0f;

    public Vector2 positionDelta = new Vector2(0.2f, 0.2f);
    public float positionSpeed = 1f;


    public float rotationDelta = 1f;

    public float rotationSpeed = 1f;

    public float distortionBase = -10f;

    public float distortionDelta = 5f;

    public float distortionSpeed = 1f;

    private PostProcessVolume _volume;

    private LensDistortion _lensDistortion;

    public void Start()
    {
        this._volume = this.GetComponent<PostProcessVolume>();
        this._volume.weight = this.Weight;

        this._lensDistortion = this._volume.profile.GetSetting<LensDistortion>();
    }

    public void Update()
    {
        this._volume.weight = this.Weight;

        if (this.Weight == 0) return;

        this.CameraPosition = new Vector2(
            Mathf.Sin(this.Time * this.positionSpeed) * this.positionDelta.x,
            Mathf.Sin(this.Time * this.positionSpeed) * this.positionDelta.y
        );

        this.CameraRotation = Mathf.Sin(this.Time * this.rotationSpeed) * this.rotationDelta;

        var distortion = this.distortionBase + Mathf.Sin(this.Time * this.distortionSpeed) * this.distortionDelta;

        this._lensDistortion.intensity.value = distortion;
    }
}
