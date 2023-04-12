using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private const string Resolution_Pref_Key = "Resolution";

    [SerializeField] private Text ResolutionText;

    private Resolution[] Resolutions;

    private int CurrentResolutionIndex = 0;

    public AudioMixer AudioMixer;

    private void Start()
    {
        Resolutions = Screen.resolutions;

        CurrentResolutionIndex = PlayerPrefs.GetInt(Resolution_Pref_Key);

        SetResolutionText(Resolutions[CurrentResolutionIndex]);
    }

    #region Menu

    public void StartGame()
    {
        SceneManager.LoadScene("stage1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

    #region ResolutionSetting

    private int GetNextWrappedIndex<T>(IList<T> collection,int currentIndex)
    {
        if (collection.Count < 1) return 0;
        return (currentIndex + 1) % collection.Count;
    }

    private int GetPreviousWrappedIndex<T>(IList<T> collection,int currentIndex)
    {
        if (collection.Count < 1) return 0;
        if ((currentIndex - 1) < 0) return collection.Count - 1;    
        return (currentIndex - 1) % collection.Count;
    }

    private void SetResolutionText(Resolution resolution)
    {
        ResolutionText.text = resolution.width + "x" + resolution.height;
    }

    public void SetNextResolution()
    {
        CurrentResolutionIndex = GetNextWrappedIndex(Resolutions, CurrentResolutionIndex);
        SetResolutionText(Resolutions[CurrentResolutionIndex]);
    }

    public void SetPreviousResolution()
    {
        CurrentResolutionIndex = GetPreviousWrappedIndex(Resolutions, CurrentResolutionIndex);
        SetResolutionText(Resolutions[CurrentResolutionIndex]);
    }

    private void SetAndApplyResolution(int NewResolutionIndex)
    {
        CurrentResolutionIndex = NewResolutionIndex;
        ApplyCurrentResolution();
    }

    private void ApplyCurrentResolution()
    {
        ApplyResolution(Resolutions[CurrentResolutionIndex]);
    }

    private void ApplyResolution(Resolution resolution)
    {
        SetResolutionText(resolution);

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(Resolution_Pref_Key, CurrentResolutionIndex);
    }

    public void ApplyChanges()
    {
        SetAndApplyResolution(CurrentResolutionIndex);
    }

    #endregion

    #region Audio

    public void SetMainVolume(float Volume)
    {
        AudioMixer.SetFloat("MainVolume", Volume);
    }

    public void SetMusicVolume(float Volume)
    {
        AudioMixer.SetFloat("MusicVolume", Volume);
    }

    public void SetSoundVolume(float Volume)
    {
        AudioMixer.SetFloat("SoundVolume", Volume);
    }

    #endregion
}
