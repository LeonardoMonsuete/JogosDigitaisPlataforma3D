using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    int _currScene;
        

    private void OnTriggerEnter(Collider gameObject)
    {
        _currScene =  SceneManager.GetActiveScene().buildIndex;
        Debug.Log(gameObject.name);
        if(gameObject.name == "Player")
        {
            SceneManager.LoadScene(++_currScene);
        }
    }
}
