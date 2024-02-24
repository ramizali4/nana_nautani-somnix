using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class backgroundHandler : MonoBehaviour
{
    public VolumeProfile profile; // Public reference to the Post Processing Profile
    public float duration = 0.3f; // Animation duration in seconds
    public Volume volume;
    private ChromaticAberration aberration;
    private DepthOfField depthOfField;

    private bool isetro = false;
    public SpriteRenderer overlay;
    private AudioSource music;
    public AudioSource sfx;
    void Start()
    {
        music = GetComponent<AudioSource>();
        // aberration = volume.profile.GetSetting<ChromaticAberration>();
        volume.profile.TryGet(out aberration);
        volume.profile.TryGet(out depthOfField);

        if (aberration == null)
        {
            Debug.LogError("Chromatic Aberration effect not found in the Post Processing Volume!");
            return;
        } 
    }
  /*  IEnumerator AnimateIntensity()
    {
        float startTime = Time.time;
        float intensity = 0f;

        while (intensity < 1f)
        {
            intensity = Mathf.Lerp(0f, 1f, (Time.time - startTime) / duration);
            aberration.intensity.Override(intensity);
            yield return new WaitForEndOfFrame();
        }
    }*/
    private IEnumerator AnimateChromaticAberration()
    {
        float elapsedTime = 0f;
        float startValue = 0f;
        float targetValue = 1f;

        // Animation from 0 to 1
        while (elapsedTime < duration)
        {
            aberration.intensity.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Animation from 1 back to 0
        elapsedTime = 0f;
        startValue = 1f;
        targetValue = 0f;

        while (elapsedTime < duration)
        {
            aberration.intensity.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        aberration.intensity.value = targetValue;
    }
    private IEnumerator AnimateDepthOfField(float startValue, float targetValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration/2)
        {
            depthOfField.focusDistance.value = Mathf.Lerp(startValue, targetValue, elapsedTime / (duration/2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        depthOfField.focusDistance.value = targetValue;
    }
    public void Shift(Component sender, object data)
    {
        //if (volume.profile.TryGet(out ChromaticAberration aberration))
            StartCoroutine(AnimateChromaticAberration());
           // StartCoroutine(AnimateDepthOfField(0,5f));
        sfx.Play(); 
        // else Debug.Log("error");
        if (data is string)
        { 
            if (isetro)
            {
                // backgroundAnimator.runtimeAnimatorController = futureAnim;
                overlay.color = new Color(255, 175, 0, 2);
                music.pitch = 0.95f;
            }
            else
            {
                //   backgroundAnimator.runtimeAnimatorController = retroAnim;
               overlay.color = new Color(255, 0, 217, 3);
                music.pitch = 1.05f;
                // overlay.color = new Color(255, 175, 0, 2);
            }
            isetro = !isetro;
        }
    }

}
