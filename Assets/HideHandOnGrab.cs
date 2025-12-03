using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideHandOnGrab : MonoBehaviour
{
    private XRRayInteractor rayInteractor;
    private XRBaseController controller;

    void Awake()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        controller = GetComponent<XRBaseController>();
    }

    void OnEnable()
    {
        rayInteractor.selectEntered.AddListener(OnGrab);
        rayInteractor.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        rayInteractor.selectEntered.RemoveListener(OnGrab);
        rayInteractor.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (controller != null)
            controller.hideControllerModel = true; 
    }

    void OnRelease(SelectExitEventArgs args)
    {
        if (controller != null)
            controller.hideControllerModel = false;
    }
}