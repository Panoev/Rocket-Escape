using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    
    float YValue = 1f;
    
    void Update()
    {
    transform.Rotate(0,YValue,0);  
    }
}
