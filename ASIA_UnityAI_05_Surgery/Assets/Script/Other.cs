using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{
    public static bool Oth_complate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "knife")
        {
            print("碰到其他");
            Oth_complate = true;
        }
    }
}
