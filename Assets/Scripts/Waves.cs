using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public GameObject prefabs;
    public int frames = 0;
    public static int enemies = 0;

    // Start is called before the first frame update
    void Start()
    {
         enemies = 0;
         spawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Inimigos presentes: " + Waves.enemies);
        spawnEnemies();
    }


    public void spawnEnemies()
    {
        if (Waves.enemies <= 0)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(prefabs, new Vector3(23.126f, 2.3221f, 8.86f), Quaternion.identity);
                enemies = enemies + 1;
            }
        }
        

    }
}
