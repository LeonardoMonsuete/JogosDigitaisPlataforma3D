using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textos : MonoBehaviour
{

    public Text texto;
    [Range(1, 10)] public float distancia = 3;
    private GameObject _player;
    private bool txt_exibido;

    // Start is called before the first frame update
    void Start()
    {
        texto.enabled = false;
        txt_exibido = false;
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && !txt_exibido)
        {
            texto.enabled = true;
            yield return new WaitForSecondsRealtime(5.5f);
            texto.enabled = false;
            txt_exibido = true;
        }
    }

}
