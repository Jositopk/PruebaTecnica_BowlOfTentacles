
using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Light targetLight;
    [SerializeField] private AudioSource audioSource;

    [Header("Sonidos")]
    [SerializeField] private AudioClip turnOnSound;
    [SerializeField] private AudioClip turnOffSound;

    private void Awake()
    {
        if (targetLight == null)
            targetLight = GetComponent<Light>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void TurnOn()
    {
        SetLightState(true);
    }

    public void TurnOff()
    {
        SetLightState(false);
    }

    public void Toggle()
    {
        if (targetLight != null)
            SetLightState(!targetLight.enabled);
    }

    public void SetLightState(bool isOn)
    {
        if (targetLight == null ||
            targetLight.enabled == isOn)
            return;

        // Cambiar estado de la luz.
        targetLight.enabled = isOn;

        // Reproducir sonido correspondiente.
        AudioClip clip = isOn ? turnOnSound : turnOffSound;

        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}
