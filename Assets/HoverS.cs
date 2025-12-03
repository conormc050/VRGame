using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverS : MonoBehaviour
 {
    public XRRayInteractor rayInteractor;
    public GameObject reticle;

    private ReticleR spinner;

    void Start()
    {
        if(reticle != null)
        {
        spinner = reticle.GetComponent<ReticleR>();
        }
    }

    void Update()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.transform.GetComponent<  TeleportationAnchor>()|| hit.transform.GetComponent<TeleportationArea>())
            {
                spinner?.StartSpinning();
            }
            else
            {
                spinner?.StopSpinning();
            }
        }
        else
        {
            spinner?.StopSpinning();
        }
    }
}
