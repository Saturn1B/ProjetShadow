using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volumes : MonoBehaviour
{
    public AudioMixer master, sound, voice;
    public Slider Master, Sound, Voice;

    private void Start()
    {
        Master.value = GetMasterValue(master);
        Sound.value = GetSoundValue(sound);
        Voice.value = GetVoiceValue(voice);
    }

    public void SetLevelMaster(float sliderValue)
    {
        master.SetFloat("MasterParam", sliderValue);
    }

    public void SetLevelSound(float sliderValue)
    {
        sound.SetFloat("SoundParam", sliderValue);
    }

    public void SetLevelVoice(float sliderValue)
    {
        voice.SetFloat("VoiceParam", sliderValue);
    }

    float GetMasterValue(AudioMixer mixer)
    {
        float value;
        bool result = mixer.GetFloat("MasterParam", out value);

        if (result)
        {
            return value;
        }
        else
        {
            return 0;
        }
    }

    float GetSoundValue(AudioMixer mixer)
    {
        float value;
        bool result = mixer.GetFloat("SoundParam", out value);

        if (result)
        {
            return value;
        }
        else
        {
            return 0;
        }
    }

    float GetVoiceValue(AudioMixer mixer)
    {
        float value;
        bool result = mixer.GetFloat("VoiceParam", out value);

        if (result)
        {
            return value;
        }
        else
        {
            return 0;
        }
    }
}
