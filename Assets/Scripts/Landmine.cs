using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public float ExplodePower;

    public void Explode()
    {
        Destroy(gameObject);
    }
}
