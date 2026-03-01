using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int currentDay;
    private int currentPatient;

    public Day[] days;
    public GameObject alertPrefab;
    public Transform alertSpawnPos;
    public Transform alertStartPos;
    public Transform itemsParent;

    [Header("Time")]
    public float waitTimeAfterPatient = 2.5f;
    public float storyTime = 9f;
    public float alertDelay = 3f;

    [Header("Ballance")]
    [Range(0f, 1f)]
    public float alertChance = 0.3f;

    private ApplicationForm applicationForm;
    private Instructions instructions;
    private Story story;
    private EResponse lastResponse;

    public Patient CurrentPatient
    {
        get
        {
            return days[currentDay].patients[currentPatient];
        }
    }

    public string CurrentInstructions
    {
        get
        {
            return days[currentDay].rules;
        }
    }

    public Day CurrentDay
    {
        get
        {
            return days[currentDay];
        }
    }

    private void Start()
    {
        applicationForm = FindAnyObjectByType<ApplicationForm>();
        instructions = FindAnyObjectByType<Instructions>();
        story = FindAnyObjectByType<Story>();

        StartDay(0);
    }

    private void StartDay(int dayId)
    {
        currentDay = dayId;
        StartCoroutine(DaySequence());
    }

    private IEnumerator DaySequence()
    {
        ShowInstructions();
        story.ShowStory(currentDay, CurrentDay.beforeStory);
        yield return new WaitForSeconds(storyTime);
        ShowPatient(0);
    }

    private void ShowInstructions()
    {
        instructions.SetInstructions(CurrentInstructions);
    }

    private void ShowPatient(int patientID)
    {
        currentPatient = patientID;
        applicationForm.SetPatient(CurrentPatient);
    }


    public void TreatPatient()
    {
        lastResponse = EResponse.Treat;
        if (applicationForm.Treat())
        {
            NextPatient();
        }
    }

    private void NextPatient()
    {
        StartCoroutine(NextPatientSequence());
    }

    private IEnumerator NextPatientSequence()
    {
        yield return new WaitForSeconds(waitTimeAfterPatient);
        if (CurrentDay.IsLastPatient(currentPatient))
        {
            NextDay();
        }
        else
        {
            SendRandomAlert();
            currentPatient++;
            ShowPatient(currentPatient);
        }
    }

    private void SendRandomAlert()
    {
        if (CurrentPatient.correctResponse != lastResponse)
        {
            if (UnityEngine.Random.Range(0f, 1f) < alertChance)
            {
                StartCoroutine(ShowAlertSequence());
            }
        }
    }

    private IEnumerator ShowAlertSequence()
    {
        yield return new WaitForSeconds(alertDelay);
        var alertGO = Instantiate(alertPrefab, alertSpawnPos.position, Quaternion.identity,itemsParent);
        alertGO.GetComponent<Alert>().startPos = alertStartPos;
    }

    private void NextDay()
    {
        if (currentDay < days.Length - 1)
        {
            StartDay(currentDay + 1);
        }
    }

    public void PrescribePatient()
    {
        lastResponse = EResponse.Prescribe;
        if (applicationForm.Prescribe())
        {
            NextPatient();
        }
    }

    public void DenyPatient()
    {
        lastResponse = EResponse.Deny;
        if (applicationForm.Deny())
        {
            NextPatient();
        }
    }
}
