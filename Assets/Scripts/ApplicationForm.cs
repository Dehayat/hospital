using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ApplicationForm : MonoBehaviour
{
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI dobLabel;
    public TextMeshProUGUI diagnosisLabel;
    public TextMeshProUGUI insuranceLabel;

    public GameObject treat;
    public GameObject deny;
    public GameObject pres;

    public Transform startPos;
    public Transform hiddenPos;
    public float moveDuration;
    public float hideSpeed;

    private RectTransform rect;
    private DraggableItem draggable;

    private bool patientReady;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        draggable = GetComponent<DraggableItem>();
        patientReady = false;
    }

    private void Start()
    {
        DisableDrag();
        treat.SetActive(false); deny.SetActive(false); pres.SetActive(false);
        rect.position = hiddenPos.position;
    }

    public void SetPatient(Patient patient)
    {
        DisableDrag();
        treat.SetActive(false); deny.SetActive(false); pres.SetActive(false);
        nameLabel.text = patient.patientName;
        dobLabel.text = patient.doB;
        diagnosisLabel.text = patient.diagnosis;
        insuranceLabel.text = patient.insuranceType == EInsurance.None ? patient.insuranceLabel : patient.insuranceType.ToString();
        StartCoroutine(GoToStartPos());
    }

    private IEnumerator GoToStartPos()
    {
        rect.position = hiddenPos.position;
        var initialPos = rect.position;
        float t = 0;

        while (Vector3.Distance(rect.position, startPos.position) > 0.2f)
        {
            t += Time.deltaTime;
            rect.position = Vector3.Lerp(initialPos, startPos.position, t / moveDuration);
            yield return new WaitForEndOfFrame();
        }
        EnableDrag();
        patientReady = true;
    }

    private void DisableDrag()
    {
        draggable.enabled = false;
    }
    private void EnableDrag()
    {
        draggable.enabled = true;
    }

    private IEnumerator Hide()
    {
        DisableDrag();
        yield return new WaitForSeconds(moveDuration);
        float t = 0;
        while (t < moveDuration)
        {
            t += Time.deltaTime;
            rect.position += Vector3.up * hideSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public bool Treat()
    {
        return HandlePatient(treat);
    }

    private bool HandlePatient(GameObject treatment)
    {
        if (!patientReady)
        {
            return false;
        }
        patientReady = false;
        treatment.SetActive(true);
        StartCoroutine(Hide());
        return true;
    }

    public bool Prescribe()
    {
        return HandlePatient(pres);
    }

    public bool Deny()
    {
        return HandlePatient(deny);
    }
}
