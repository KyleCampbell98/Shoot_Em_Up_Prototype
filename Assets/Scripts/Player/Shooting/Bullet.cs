using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Configuration")]
    [SerializeField] private Rigidbody2D bulletRb;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float activeBulletLifespan = 2f; // Time bullet is active before disabling.
    [SerializeField] private Vector2 positionalOrigin; // Used for resetting position upon disabling. 


    // Internal Script Logic
     private const string deactivateBulletMethod = "DeactivateBullet";


    // Start is called before the first frame update
    void Awake()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        positionalOrigin = transform.position;
    }
   

    private void OnEnable()
    {
        Invoke(deactivateBulletMethod, activeBulletLifespan);
        bulletRb.velocity = bulletSpeed * transform.up;
    }

    private void OnDisable()
    {
        transform.position = positionalOrigin;
    }
    
    

    private void DeactivateBullet()
    {
     //   Debug.Log("Deactivate Bullet called");
        gameObject.SetActive(false);
    }

    
}
