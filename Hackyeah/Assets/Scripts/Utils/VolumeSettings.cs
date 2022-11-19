using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] string modifiedParameter;
    
    [Header("Mixers:")]
    [SerializeField] AudioMixer audioMixer; 

    [Header("Button reference:")]
    [SerializeField] Image musicMuteButton;

    [Header("Volume:")]
    [SerializeField] SO_Float audioVolume;

    [Header("Sprites:")]
    [SerializeField] Sprite audioMutedSprite;
    [SerializeField] Sprite audioUnmutedSprite;

    [SerializeField] Slider slider;

    bool isMuted = false;
    float soundValue = 0f;

    void Start()
    {
        //if(slider != null)
        //{
        //    slider.value = audioVolume.Float;
        //}
        audioMixer.GetFloat(modifiedParameter, out soundValue);

        if(soundValue <= -60f)
        {
            musicMuteButton.sprite = audioMutedSprite;
            isMuted = true;
        }
        else
        {
            musicMuteButton.sprite = audioUnmutedSprite;
            isMuted = false;
        }
        //isMuted = false;
        //audioMixer.SetFloat(modifiedParameter, audioVolume.Float);
    }
    
    void SetVolume(float MasterVolume)
    {
        Debug.Log(MasterVolume);
        audioMixer.SetFloat(modifiedParameter, audioVolume.Float);
    }

    public void SetAudioVolume()
    {
        audioVolume.Float = slider.value;
        audioMixer.SetFloat(modifiedParameter, audioVolume.Float);
        Debug.Log(audioVolume.Float);
    }

    void Mute()
    {
        Debug.Log("Muted");
        audioMixer.SetFloat(modifiedParameter, -80f);
        musicMuteButton.sprite = audioMutedSprite;
        isMuted = true;
    }

    void Unmute()
    {
        Debug.Log("Unmuted");
        audioMixer.SetFloat(modifiedParameter, audioVolume.Float);
        musicMuteButton.sprite = audioUnmutedSprite;
        isMuted = false;
    }

    public void MuteUnmute()
    {
        if(isMuted)
        {
            Unmute();
        }
        else
        {
            Mute();
        }
    }
}
