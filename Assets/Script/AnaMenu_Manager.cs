using Kubra;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AnaMenu_Manager : MonoBehaviour
{

    BellekY�netimi _BellekY�netimi = new BellekY�netimi();
    
    public GameObject CikisPaneli;
    
    public AudioSource ButonSes;

    // Start is called before the first frame update
    
    public Text[] TextObjeleri;
    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;


    void Start()
    {
        _BellekY�netimi.KontrolEtVeTan�mla();
        
        ButonSes.volume = _BellekY�netimi.VeriOku_f("MenuFx");

  
    }

    public void SahneYukle(int Index)
    {
        ButonSes.Play();
        SceneManager.LoadScene(Index);
    }

    public void Oyna()
    {
        ButonSes.Play();
        StartCoroutine(LoadAsync(_BellekY�netimi.VeriOku_i("SonLevel")));
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

    public void CikisButonIslem(string Durum)
    {
        ButonSes.Play();
        if (Durum == "Evet")
            Application.Quit();
        else if (Durum == "cikis")
            CikisPaneli.SetActive(true);
        else
            CikisPaneli.SetActive(false);
    }

}
