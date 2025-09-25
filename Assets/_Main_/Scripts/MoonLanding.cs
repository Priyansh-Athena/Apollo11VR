using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public TMP_Text rockMissionInstructionTxt;
    [SerializeField] Button roverMissionBtn, flagMissionBtn, RockMissionBtn;
    [SerializeField] TMP_Text roverMissionBtnTxt, flagMissionBtnText, RockMissionBtnText, miniGameWindowTitleTxt;
    [SerializeField] GameObject restartButton;
    

    public GameObject flag, ungrabbableFlag;

    [SerializeField] InputActionReference leftPrimary, rightPrimary;

    [Header("Events")]
    public UnityEvent OnRoverMissionStart, OnRoverMissionStop, OnMarkRoverMissionComplete;
    public UnityEvent OnFlagMissionStart, OnFlagMissionStop;
    public UnityEvent OnRockMissionStart, OnRockMissionStop;

    private bool isInMission = false;
    private bool roverMissionCompleted = false, flagMissionCompleted = false, rockMissionCompleted = false;

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
        if(RocketLanding.hasLanded && !isInMission)
        {
            miniGamesWindow.SetActive(!miniGamesWindow.activeInHierarchy);
        }
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
        if (roverMissionCompleted)
        {
            roverMissionBtn.interactable = false;
            roverMissionBtnTxt.text = $"Mission Completed: Drive rover";
            return;
        }

        isInMission = true;

        rover.transform.position = roverSpawnPosition.position;
        rover.SetActive(true);

        OnRoverMissionStart.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Rocket Launch");
    }

    public void StopRoverMission(Collider other)
    {
        if (other.gameObject.tag == "IceMountain")
        {
            roverDescriptionTxt.text = $"Mission Completed: Ice Found on the Moon...";

            OnRoverMissionStop.Invoke();
        }
    }

    public void MarkRoverMissionComplete()
    {
        OnMarkRoverMissionComplete.Invoke();
        isInMission = false;
        roverMissionCompleted = true;

        MiniGamesWindowTitle();
    }

    public void StartFlagMission()
    {
        if (flagMissionCompleted)
        {
            flagMissionBtn.interactable = false;
            flagMissionBtnText.text = $"Mission Completed: Host the flag";
            return;
        }

        isInMission = true;

        Vector3 flagPos = flag.transform.position;
        flagPos.x = roverSpawnPosition.position.x;
        flagPos.y = 2;
        flagPos.z = roverSpawnPosition.position.z;
        flag.transform.position = flagPos;

        flag.SetActive(true);

        OnFlagMissionStart.Invoke();
    }

    public void StopFlagMission(Collider other)
    {
        if(other.gameObject.tag == "FlagHosting")
        {
            Vector3 triggerPos = other.ClosestPoint(flag.transform.position);
            triggerPos.y = 1f;
            flag.SetActive(false);

            ungrabbableFlag.transform.position = triggerPos;
            ungrabbableFlag.SetActive(true);
        }


        OnFlagMissionStop.Invoke();
        isInMission = false;
        flagMissionCompleted = true;

        MiniGamesWindowTitle();
    }

    public void StartRockMission()
    {
        if(rockMissionCompleted)
        {
            RockMissionBtn.interactable = false;
            RockMissionBtnText.text = $"Mission Completed: Collect Rock Samples";
            return;
        }

        isInMission = true;
        OnRockMissionStart.Invoke();
    }

    int rocksCollected = 0;
    public void CollecRock()
    {
        rocksCollected++;
        rockMissionInstructionTxt.text = $"Rock sample collected: {rocksCollected}/5";

        if(rocksCollected >= 5)
        {
            rockMissionInstructionTxt.text = $"Mission Completed: Rock Samples Collected...";
            Invoke("StopRockMission", 3f);
        }
    }

    private void StopRockMission()
    {
        OnRockMissionStop.Invoke();
        isInMission = false;
        rockMissionCompleted = true;

        MiniGamesWindowTitle();
    }

    private void MiniGamesWindowTitle()
    {
        if(roverMissionCompleted && flagMissionCompleted && rockMissionCompleted)
        {
            restartButton.SetActive(true);
            miniGameWindowTitleTxt.text = "All Missions Completed! You can now explore the Moon!";
        }

        if (roverMissionCompleted)
        {
            roverMissionBtn.interactable = false;
            roverMissionBtnTxt.text = $"Mission Completed: Drive Rover";
        }

        if (flagMissionCompleted)
        {
            flagMissionBtn.interactable = false;
            flagMissionBtnText.text = $"Mission Completed: Host the flag";
        }

        if (rockMissionCompleted)
        {
            RockMissionBtn.interactable = false;
            RockMissionBtnText.text = $"Mission Completed: Collect Rock Samples";
        }
    }
}
