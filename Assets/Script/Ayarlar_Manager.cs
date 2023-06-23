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
    BellekYönetimi _BellekYönetimi = new BellekYönetimi();

    // Start is called before the first frame update
    void Start()
    {

        ButonSes.volume = _BellekYönetimi.VeriOku_f("MenuFx");
        MenuSes.value = _BellekYönetimi.VeriOku_f("MenuSes");
        MenuFx.value = _BellekYönetimi.VeriOku_f("MenuFx");
        OyunSes.value = _BellekYönetimi.VeriOku_f("OyunSes");

    }

    public void SesAyarla(string HangiAyar)
    {
        switch (HangiAyar)
        {
            case "MenuSes":
                _BellekYönetimi.VeriKaydet_float("MenuSes", MenuSes.value);
                break;

            case "MenuFx":
                _BellekYönetimi.VeriKaydet_float("MenuFx", MenuFx.value);
                break;

            case "OyunSes":
                _BellekYönetimi.VeriKaydet_float("OyunSes", OyunSes.value);
                break;
        }

    }

    public void GeriDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
    }

}
