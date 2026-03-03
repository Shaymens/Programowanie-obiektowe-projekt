using UnityEngine;
using TMPro;

public class DamageNumberController : MonoBehaviour
{
    public DamageNumber prefab;
    public static DamageNumberController Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }   

    public void CreateNumber(float damage, Vector3 position)
    {
        Instantiate(prefab, position, transform.rotation, transform);
    }
}

