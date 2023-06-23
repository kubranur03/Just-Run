using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Kubra;

public class Ayarlar_Manager : MonoBehaviour
{

    public AudioSource ButonSes;
    public Slider MenuSes;
    public Slider MenuFx;
    public Slider OyunSes;
    BellekY�netimi _BellekY�netimi = new BellekY�netimi();

    // Start is called before the first frame update
    void Start()
    {

        ButonSes.volume = _BellekY�netimi.VeriOku_f("MenuFx");
        MenuSes.value = _BellekY�netimi.VeriOku_f("MenuSes");
        MenuFx.value = _BellekY�netimi.VeriOku_f("MenuFx");
        OyunSes.value = _BellekY�netimi.VeriOku_f("OyunSes");

    }

    public void SesAyarla(string HangiAyar)
    {
        switch (HangiAyar)
        {
            case "MenuSes":
                _BellekY�netimi.VeriKaydet_float("MenuSes", MenuSes.value);
                break;

            case "MenuFx":
                _BellekY�netimi.VeriKaydet_float("MenuFx", MenuFx.value);
                break;

            case "OyunSes":
                _BellekY�netimi.VeriKaydet_float("OyunSes", OyunSes.value);
                break;
        }

    }

    public void GeriDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
    }

}
