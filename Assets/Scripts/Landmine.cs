using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public Transform basePos;
    public float ExplodePower;

    public void Explode()
    {
        print("BOOM!");
    }
}
