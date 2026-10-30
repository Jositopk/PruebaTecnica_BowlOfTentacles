using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class TensionEvent : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource footstepsSource;
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private LightController hallwayLight;
    [SerializeField] private LightController KitchenLight;

    [Header("Configuracion")]
    [SerializeField, Min(0f)] private float silenceDuration = 2f;
    [SerializeField, Min(0f)] private float fadeInDuration = 2f;

    [Header("Eventos")]
    [SerializeField] private UnityEvent onTriggered;

    private bool hasTriggered;

    public void Trigger()
    {
        if (hasTriggered) return;

        hasTriggered = true;

        // Ejecuta los efectos configurados en el Inspector.
        onTriggered?.Invoke();

        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        // Pausar el ambiente, si existe.
        bool resumeAmbience =ambienceSource != null && ambienceSource.isPlaying;
        float originalVolume = resumeAmbience ? ambienceSource.volume : 0f;
        if (resumeAmbience)
            ambienceSource.Pause();
        //apagamos Luces
        yield return new WaitForSeconds(0.1f);
        hallwayLight.Toggle();
        KitchenLight.Toggle();
        // Dos segundos de silencio.
        yield return new WaitForSeconds(silenceDuration);

        // Reproducir los tres pasos.
        if (footstepsSource != null && footstepsSource.clip != null)
        {
            footstepsSource.Play();

            yield return new WaitForSeconds(footstepsSource.clip.length);
        }
        //Encendemos Luces
        hallwayLight.Toggle();
        KitchenLight.Toggle();
        // Recuperar el ambiente con Fade In.
        if (resumeAmbience)
        {
            ambienceSource.volume = 0f;
            ambienceSource.UnPause();
            float elapsed = 0f;
            while (elapsed < fadeInDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / fadeInDuration);
                ambienceSource.volume = Mathf.Lerp(0f, originalVolume, t);
                yield return null;
            }
            ambienceSource.volume = originalVolume;
        }
    }
}