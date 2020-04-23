using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kidney : MonoBehaviour
{
    public static bool complate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "knife")
        {
            print("碰到腰子");
            complate = true;
        }
    }
}
