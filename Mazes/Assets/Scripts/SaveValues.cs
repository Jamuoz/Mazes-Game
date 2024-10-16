using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SaveValues : MonoBehaviour
{

    public AudioMixer audiomix;
    [SerializeField] private Slider generalAudioSlider;
    [SerializeField] private Slider ambientSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Slider musicSlider;

    private void Start() {
        // Inicializar los valores de sonido de los Sliders
        InitializeVolume("AmbientSound", ambientSlider, "Ambient");
        InitializeVolume("GeneralSound", generalAudioSlider, "General");
        InitializeVolume("MusicSound", musicSlider, "Music");
        InitializeVolume("EffectsSound", effectsSlider, "Effects");
    }

    private void InitializeVolume(string key, Slider slider, string parameterName) 
    {
        if (PlayerPrefs.HasKey(key)) {
            slider.value = PlayerPrefs.GetFloat(key);
        } else {
            PlayerPrefs.SetFloat(key, slider.value);
        }
        SetVolume(slider, parameterName, key);
    }

    public void SetVolume(Slider slider, string parameterName, string key) {
        float volume = slider.value;
        audiomix.SetFloat(parameterName, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(key, volume);
    }

    // Métodos específicos de cada tipo de volumen (enlazados a los sliders)
    public void SetAmbientVolume() {
        SetVolume(ambientSlider, "Ambient", "AmbientSound");
    }
    public void SetGeneralVolume() {
        SetVolume(generalAudioSlider, "General", "GeneralSound");
    }
    public void SetMusicVolume() {
        SetVolume(musicSlider, "Music", "MusicSound");
    }
    public void SetEffectsVolume() {
        SetVolume(effectsSlider, "Effects", "EffectsSound");
    }  
}
