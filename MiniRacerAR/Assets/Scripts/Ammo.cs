using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    Rigidbody rb;
    public float ammoVelocity = 10;
    public AudioClip fire;
    public AudioClip hit;

    private void Awake() {
        AudioSource.PlayClipAtPoint(fire, transform.position);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * ammoVelocity * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other) {
        AudioSource.PlayClipAtPoint(hit, transform.position);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
