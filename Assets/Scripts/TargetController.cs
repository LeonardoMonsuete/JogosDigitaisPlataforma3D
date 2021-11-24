using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float maxHealth = 50f;
    public float health;
    public Slider healthSlider;
    public float healthPercentage;
    public GameObject player;
 
    void Start()
    {
        player = GameObject.Find("Player");
        health = maxHealth;
        healthPercentage = health / maxHealth;
        healthSlider.value = healthPercentage;
    }

    void Update()
    {
        if(player != null && healthSlider != null)
        {
            Vector3 directionPlayer = player.transform.position - healthSlider.transform.position;
            Quaternion rotationSlider = Quaternion.LookRotation(directionPlayer);
            healthSlider.transform.rotation = rotationSlider;
        }

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthPercentage = health / maxHealth;
        healthSlider.value = healthPercentage;
        if (health < 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Waves.enemies--;
        Debug.Log(Waves.enemies);
        Destroy(gameObject);
    }
}
