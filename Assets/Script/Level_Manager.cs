using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Kubra;
using System.Reflection;

public class Level_Manager : MonoBehaviour
{
    int levelIsUnlocked;

    public Button[] buttons;
    public AudioSource ButonSes;

    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;

    void Start()
    {
        ButonSes.volume = PlayerPrefs.GetFloat("MenuFx");

        ButonSes.Play();

        levelIsUnlocked = PlayerPrefs.GetInt("levelIsUnlocked", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < levelIsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }

    }

    public void LoadLevel(int levelIndex)
    {
        ButonSes.Play();
        StartCoroutine(LoadAsync(levelIndex));
    }

    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation Operation = SceneManager.LoadSceneAsync(SceneIndex);
        YuklemeEkrani.SetActive(true);

        while (!Operation.isDone)
        {
            float progress = Mathf.Clamp01(Operation.progress / .9f);
            YuklemeSlider.value = Operation.progress;
            yield return null;

        }
    }

}
