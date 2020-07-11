using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownBomb : MonoBehaviour
{
    public GameObject landminePrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(landminePrefab, collision.contacts[0].point, Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal));
        Destroy(gameObject);
    }
}
