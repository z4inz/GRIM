using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Destroys game object after collision or 5 seconds, this script makes sure bullet goes in right direction
public class Bullet: MonoBehaviour
{
    [SerializeField] float speed = 15f;

    public void Launch(Vector3 direction)
    {
        direction.Normalize();
        transform.up = direction;
        GetComponent<Rigidbody>().velocity = direction * speed;
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    void Start()
    {
        

    }
}
