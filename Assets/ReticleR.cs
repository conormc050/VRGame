using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleR : MonoBehaviour
{
    float spinSpeed = 100f;
    bool isspinning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isspinning)
        {
            transform.Rotate(Vector3.up, spinSpeed*Time.deltaTime);
        }
    }
    public void StartSpinning()
    {
        isspinning = true;
    }
    public void StopSpinning()
    {
        isspinning = false;
    }
}
