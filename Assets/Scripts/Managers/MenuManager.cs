using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;

public class MenuManager : MonoBehaviour
{
    public Animator canvasAnimator;

    public GameObject[] menuPanels = new GameObject[4];

    public SoundManager soundManager;

    public Text[] textButtons;
    public GameObject iselect;

    public Toggle seeMines;
    public Toggle mutedMusic;
    public Toggle mutedSFX;
    public Slider volumeMusic;
    public Slider volumeSFX;

    void Start()
    {
        soundManager = SoundManager.Instance;

        SetMusicClipName("menu-music");

        GetPrayerPrefs();

        ChangeVolumeMusic();
        ChangeVolumeSFX();
        ChangeMuteMusic();
        ChangeMuteSFX();

        //SelectPanel("MenuPanel");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PlaySFXClipName("back-menu");
            ExitPanel("MenuPanel");
        }
    }

    public void StartGame()
    {
        PlaySFXClipName("click-text");

        SetMusicClipName("gamming-music");
        if (mutedMusic.isOn)
            PlayMusicClipName();

        SceneManager.LoadScene("Minefield");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #region UI Events

    public void PointerEnterText(string pNameText)
    {
        soundManager.PlaySFXClipName("PointerEnterText");

        Text auxText = textButtons.FirstOrDefault(t => t.name == pNameText);
        iselect.SetActive(true);

        iselect.transform.position =
            new Vector3(auxText.transform.position.x + auxText.transform.position.x * 0.2f,
            auxText.transform.position.y,
            auxText.transform.position.z);
    }

    public void PointerExitText(string pNameText)
    {
        iselect.SetActive(false);
    }

    #endregion

    #region UI Sounds Events

    public void ExitPanel(string pStatePanel)
    {
        canvasAnimator.SetBool("Options", false);
        canvasAnimator.SetBool("HowToPlay", false);
        canvasAnimator.SetBool("Credits", false);

        soundManager.PlaySFXClipName("click-text");
    }

    public void SelectPanel(string pStatePanel)
    {
        canvasAnimator.SetBool(pStatePanel, true);
        soundManager.PlaySFXClipName("click-text");
    }

    public void PlaySFXClipName(string pName)
    {
        soundManager.PlaySFXClipName(pName);
    }

    public void SetMusicClipName(string pName)
    {
        soundManager.SetMusicClipName(pName);
    }

    public void PlayMusicClipName()
    {
        soundManager.PlayMusicClipName();
    }


    public void ChangeMuteMusic()
    {
        //mutedSFX.isOn --> true: apagar la musica
        PlayerPrefs.SetInt("mutedMusic", Convert.ToInt32(mutedMusic.isOn));
        volumeMusic.interactable = mutedMusic.isOn;
        soundManager.ChangeMuteMusic(!mutedMusic.isOn);
    }

    public void ChangeMuteSFX()
    {
        //mutedSFX.isOn --> true: apagar la musica
        PlayerPrefs.SetInt("mutedSFX", Convert.ToInt32(mutedSFX.isOn));
        volumeSFX.interactable = mutedSFX.isOn;
        soundManager.ChangeMuteSFX(!mutedSFX.isOn);
    }

    public void ChangeVolumeMusic()
    {
        PlayerPrefs.SetFloat("volumeMusic", volumeMusic.value);
        soundManager.ChangeVolumeMusic(volumeMusic.value);
    }

    public void ChangeVolumeSFX()
    {
        PlayerPrefs.SetFloat("volumeSFX", volumeSFX.value);
        soundManager.ChangeVolumeSFX(volumeSFX.value);
    }

    public void ChangeSeeMines()
    {
        PlayerPrefs.SetInt("seeMines", Convert.ToInt32(seeMines.isOn));
    }

    public void GetPrayerPrefs()
    {
        if (PlayerPrefs.HasKey("seeMines"))
            seeMines.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("seeMines"));
        else
        {
            seeMines.isOn = GeneralGameValues.SeeMines;
            PlayerPrefs.SetInt("seeMines", Convert.ToInt32(seeMines.isOn));
        }

        if (PlayerPrefs.HasKey("mutedMusic"))
            mutedMusic.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("mutedMusic"));
        else
        {
            mutedMusic.isOn = GeneralGameValues.MutedMusic;
            PlayerPrefs.SetInt("mutedMusic", Convert.ToInt32(mutedMusic.isOn));
        }

        if (PlayerPrefs.HasKey("mutedSFX"))
            mutedSFX.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("mutedSFX"));
        else
        {
            mutedSFX.isOn = GeneralGameValues.MutedSFX;
            PlayerPrefs.SetInt("mutedSFX", Convert.ToInt32(mutedSFX.isOn));
        }

        if (PlayerPrefs.HasKey("volumeMusic"))
            volumeMusic.value = PlayerPrefs.GetFloat("volumeMusic");
        else
        {
            volumeMusic.value = GeneralGameValues.VolumneMusic; //ACA TENER VALOR POR DEFECTO
            PlayerPrefs.SetFloat("volumeMusic", volumeMusic.value);
        }

        if (PlayerPrefs.HasKey("volumeSFX"))
            volumeSFX.value = PlayerPrefs.GetFloat("volumeSFX");
        else
        {
            volumeSFX.value = GeneralGameValues.VolumeSFX; //ACA TENER VALOR POR DEFECTO
            PlayerPrefs.SetFloat("volumeSFX", volumeSFX.value);
        }
    }
    #endregion


}

