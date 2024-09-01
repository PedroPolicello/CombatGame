using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("---- Audio Sources ----")] 
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] private AudioSource battleSource;
    
    [Header("---- Audio Sliders ----")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sFXSlider;
    
    [Header("---- Audio Numbers ----")]
    [SerializeField] private TextMeshProUGUI musicNumber;
    [SerializeField] private TextMeshProUGUI sFXNumber;

    [Header("---- Audio Clips ----")]
    [SerializeField] private AudioClip backgroundMusic;
    public AudioClip battleMusic;
    public AudioClip click; //Ajuste
    public AudioClip win;
    public AudioClip lose;
    public AudioClip[] punches;

    private void Awake()
    {
        SetBackgroundMusic();
        SetSliders();
    }
    
    private void Update()
    {
       VolumeController();
    }

    void SetBackgroundMusic()
    {
        musicSource.loop = true;
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    void SetSliders()
    {
        musicSlider.maxValue = 10f;
        sFXSlider.maxValue = 10f;
        musicSlider.value = musicSlider.maxValue;
        sFXSlider.value = sFXSlider.maxValue;
    }

    void VolumeController()
    {
        musicSource.volume = musicSlider.value / 100;
        sFXSource.volume = sFXSlider.value / 10;
        musicNumber.text = musicSlider.value.ToString();
        sFXNumber.text = sFXSlider.value.ToString();
    }

    public void BattleMusic(bool inCombat)
    {
        if (inCombat)
        {
            battleSource.clip = battleMusic;
            battleSource.volume = .15f;
            battleSource.loop = true;
            musicSource.Stop();
            battleSource.Play();
        }
        else
        {
            battleSource.Stop();
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sFXSource.PlayOneShot(audioClip);
    }

    public void ChooseRandomPunchSFX()
    {
        int index = Random.Range(0, punches.Length);
        PlaySFX(punches[index]);
    }
}
