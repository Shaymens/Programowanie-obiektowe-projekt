using UnityEngine;
using System.Collections.Generic;

public class Area : MonoBehaviour
{
    public AreaWeapon weapon;

    private Vector3 targetSize;
    private float timer;
    private bool shrinking;

    private float counter;
    private List<LookAtPlayer> enemiesInRange = new List<LookAtPlayer>();

    void Start()
    {
        transform.localScale = Vector3.zero;
        targetSize = Vector3.one * weapon.stats[weapon.weaponLevel].range;

        timer = weapon.stats[weapon.weaponLevel].duration;
        shrinking = false;

        counter = weapon.stats[weapon.weaponLevel].speed;
    }

    void Update()
    {
        if (!shrinking)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * 5);

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                shrinking = true;
                targetSize = Vector3.zero;
            }
        }
        else
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime);

            if (transform.localScale.x <= 0f)
            {
                Destroy(gameObject);
            }
        }

        // Zadawanie obra¿eñ co X sekund
        counter -= Time.deltaTime;
        if (counter <= 0f)
        {
            counter = weapon.stats[weapon.weaponLevel].speed;

            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                if (enemiesInRange[i] != null)
                {
                    enemiesInRange[i].TakeDamage(weapon.stats[weapon.weaponLevel].damage);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            LookAtPlayer enemy = collision.GetComponent<LookAtPlayer>();
            if (enemy != null)
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            LookAtPlayer enemy = collision.GetComponent<LookAtPlayer>();
            if (enemy != null && enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }
}

