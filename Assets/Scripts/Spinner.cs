﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float spinSpeed = 1f;

   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,spinSpeed*Time.deltaTime);
    }
}
