using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class MoonLanding : MonoBehaviour
{
    [SerializeField] Loading loading;
    [SerializeField] RocketLanding rocketLanding;
    [SerializeField] TMP_Text landingInstructionTxt;
    [SerializeField] GameObject miniGamesWindow;
    [SerializeField] Transform roverSpawnPosition;
    [SerializeField] GameObject rover;
    public TMP_Text roverDescriptionTxt;

    [SerializeField] InputActionReference leftPrimary, rightPrimary;

    [Header("Events")]
    public UnityEvent OnRoverMissionStart;

    private void Start()
    {
        loading.HideLoading();
        StartCoroutine(RocketLandingCoroutine());

    }

    private void OnEnable()
    {
        // Attach a callback when the action is performed (button pressed)
        leftPrimary.action.performed += OnPrimaryButtonPressed;
        rightPrimary.action.performed += OnPrimaryButtonPressed;

        // Enable the action (if not already enabled by XRI)
        leftPrimary.action.Enable();
        rightPrimary.action.Enable();
    }

    private void OnDisable()
    {
        // Detach the callback to avoid memory leaks
        leftPrimary.action.performed -= OnPrimaryButtonPressed;
        rightPrimary.action.performed -= OnPrimaryButtonPressed;

        // Optional: disable the action
        leftPrimary.action.Disable();
        rightPrimary.action.Disable();
    }

    // This function runs when the primary button is pressed
    private void OnPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        if(RocketLanding.hasLanded)
            miniGamesWindow.SetActive(!miniGamesWindow.activeInHierarchy);
    }

    void Update()
    {
        
    }

    private IEnumerator RocketLandingCoroutine()
    {
        for(int i=10; i>=0; i--)
        {
            landingInstructionTxt.text = $"Eagle Module will land here in {i} seconds";
            yield return new WaitForSeconds(1f);
        }

        landingInstructionTxt.text = "Eagle Module is landing... \n Look Up...";
        rocketLanding.StartLanding();
    }

    public void AfterLandingTasks()
    {
        landingInstructionTxt.text = "Eagle Module has landed successfully! Use Primary button to select to open Missions Screen";
    }

    public void StartRoverMission()
    {
        rover.transform.position = roverSpawnPosition.position;
        rover.SetActive(true);

        OnRoverMissionStart.Invoke();
    }

    public void StopRoverMission(Collider other)
    {
        if (other.gameObject.tag == "IceMountains")
        {
            roverDescriptionTxt.text = $"Mission Completed: Ice Found on the Moon...";
        }
    }
}
