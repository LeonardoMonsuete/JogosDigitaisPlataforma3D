using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider gameObject)
    {
        Debug.Log(gameObject.name);
        if(gameObject.name == "Player")
        {
            SceneManager.LoadScene("Game2");
        }
    }
}
