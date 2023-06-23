using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DusmanController : MonoBehaviour
{
    public GameObject Saldiri_Hedefi;
    public NavMeshAgent _NavMesh;
    public Animator _Animator;
    public GameManager _Gamemager;
    bool Saldiri_BasladiMi;

    public void AnimasyonTetikle()
    {
        _Animator.SetBool("Saldir", true);
        Saldiri_BasladiMi = true;

    }

    // Update is called once per frame
    void LateUpdate ()
    {
        if(Saldiri_BasladiMi)
        _NavMesh.SetDestination(Saldiri_Hedefi.transform.position);
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AltKarakter"))
        {
            Vector3 yeniPoz = new Vector3(transform.position.x, .23f, transform.position.z);
            _Gamemager.YokOlmaEfektiOlustur(yeniPoz, false, true);
            gameObject.SetActive(false);
        }
    }
}
