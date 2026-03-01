using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Day", menuName = "Scriptable Objects/Day")]
public class Day : ScriptableObject
{
    [TextArea]
    public string rules;
    public Patient[] patients;
    [TextArea]
    public string beforeStory;

    public bool IsLastPatient(int currentPatient)
    {
        return currentPatient == patients.Length - 1;
    }
}
