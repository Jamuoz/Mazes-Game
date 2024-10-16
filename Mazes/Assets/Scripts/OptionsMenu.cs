
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class PostProcessingSettings
{
    
    public bool AmbientOC;
    public bool Bloom;
    public bool ColorGrading;
    public bool Grain;
    public bool MotionBlur;
    public bool Vignette;
    
}

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mySounds;
    public Slider sliderMusica;
    public Slider sliderSFX;
    public Slider sliderAmbient;

    //valores post procesado
    public PostProcessingSettings postpro;

    
}
