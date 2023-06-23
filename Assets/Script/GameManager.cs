using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kubra;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int AnlikKarakterSayisi = 1;
    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesiEfektler;


    [Header("LEVEL VERÝLERÝ")]
    public List<GameObject> Dusmanlar;
    public int KacDusmanOlsun;
    public GameObject _AnaKarakter;
    public bool OyunBittiMi;
    bool SonaGeldikMi;


    Matematiksel_islemler _Matematiksel_islemler = new Matematiksel_islemler();
    BellekYönetimi _BellekYönetimi = new BellekYönetimi();

    Scene _Scene;
    [Header("GENEL VERÝLERÝ")]
    public AudioSource[] Sesler;
    public GameObject[] IslemPanelleri;
    public Slider OyunSesiAyar;

    [Header("LOADING VERÝLERÝ")]
    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;

    private void Awake()
    {
        Sesler[0].volume = _BellekYönetimi.VeriOku_f("OyunSes");
        OyunSesiAyar.value = _BellekYönetimi.VeriOku_f("OyunSes");
        Sesler[1].volume = _BellekYönetimi.VeriOku_f("MenuFX");
        Destroy(GameObject.FindWithTag("MenuSes"));
    }

    void Start()
    {
        DusmanlariOlustur();

        _Scene = SceneManager.GetActiveScene();
    }

    public void DusmanlariOlustur()
    {
        for(int i = 0; i < KacDusmanOlsun; i++)
        {
            Dusmanlar[i].SetActive(true);

        }
    }

    public void DusmanlariTetikle()
    {
        foreach(var item in Dusmanlar)
        {
            if(item.activeInHierarchy)
            {
                item.GetComponent<DusmanController>().AnimasyonTetikle();
            }
        }
        SonaGeldikMi = true;
        SavasDurumu();
    }

    void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.A))
            foreach (var item in Karakterler)
            {
                if(!item.activeInHierarchy)
                {
                    item.transform.position = DogmaNoktasi.transform.position;
                    item.SetActive(true);
                    AnlikKarakterSayisi++;
                    break;
                }


            }*/
    }

    void SavasDurumu()
    {

        if(SonaGeldikMi)
        {
            if (AnlikKarakterSayisi == 1 || KacDusmanOlsun == 0)
            {
                OyunBittiMi = true;
                foreach (var item in Dusmanlar)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }

                foreach (var item in Karakterler)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }

                _AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);

                if (AnlikKarakterSayisi < KacDusmanOlsun || AnlikKarakterSayisi == KacDusmanOlsun)
                {
                    IslemPanelleri[2].SetActive(true);
                }
                else
                {
                    if(AnlikKarakterSayisi > 5)
                    {
                        if(_Scene.buildIndex == _BellekYönetimi.VeriOku_i("SonLevel"))
                        {
                            _BellekYönetimi.VeriKaydet_int("Puan", _BellekYönetimi.VeriOku_i("Puan") + 600);
                            _BellekYönetimi.VeriKaydet_int("SonLevel", _BellekYönetimi.VeriOku_i("SonLevel") + 1);

                        }
                    }

                    else
                    {
                        if (_Scene.buildIndex == _BellekYönetimi.VeriOku_i("SonLevel"))
                        {
                            _BellekYönetimi.VeriKaydet_int("Puan", _BellekYönetimi.VeriOku_i("Puan") + 200);
                            _BellekYönetimi.VeriKaydet_int("SonLevel", _BellekYönetimi.VeriOku_i("SonLevel") + 1);
                        }

                    }
                    IslemPanelleri[3].SetActive(true);
                }


            }
        }
       
    }

    public void AdamYonetimi(string islemTuru, int GelenSayi,  Transform Pozisyon)
    {
        switch(islemTuru)
        {
            case "Carpma":
                _Matematiksel_islemler.Carpma(GelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;

            case "Toplama":
                _Matematiksel_islemler.Toplama(GelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;

            case "Cikartma":
                _Matematiksel_islemler.Cikartma(GelenSayi, Karakterler, YokOlmaEfektleri);
                break;

            case "Bolme":
                _Matematiksel_islemler.Bolme(GelenSayi, Karakterler, YokOlmaEfektleri);
                break;
        }

    }

    public void YokOlmaEfektiOlustur(Vector3 Pozisyon, bool Balyoz = false, bool Durum=false)
    {
        foreach(var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                if (!Durum)
                    AnlikKarakterSayisi--;
                else
                    KacDusmanOlsun--;
                break;
            }
        }

        

        if (Balyoz)
        {
            Vector3 yeniPoz = new Vector3(Pozisyon.x, .005f, Pozisyon.z);
            foreach (var item in AdamLekesiEfektler)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = yeniPoz;
                    break;
                }
            }

        }

        if(!OyunBittiMi)
            SavasDurumu();

    }

    public void CikisButonIslem(string Durum)
    {
        Sesler[1].Play();
        Time.timeScale = 0;
        if (Durum == "Durdur")
        {
            IslemPanelleri[0].SetActive(true);
        }  
        else if (Durum == "devamet") 
        {
            IslemPanelleri[0].SetActive(false);
            Time.timeScale = 1;
        }
            
        else if (Durum == "tekrar")
        {
            SceneManager.LoadScene(_Scene.buildIndex);
            Time.timeScale = 1;

        }

        else if (Durum == "anasayfa")
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    public void Ayarlar(string Durum)
    {
        if (Durum == "ayarla")
        {
            IslemPanelleri[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            IslemPanelleri[1].SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void SesiAyarla()
    {
        _BellekYönetimi.VeriKaydet_float("OyunSes", OyunSesiAyar.value);
        Sesler[0].volume = OyunSesiAyar.value;
    }

    public void SonrakiLevel()
    {
        StartCoroutine(LoadAsync(_Scene.buildIndex + 1));
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
