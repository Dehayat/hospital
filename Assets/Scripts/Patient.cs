using UnityEngine;

public enum EInsurance
{
    None,
    PremiumCare,
    BasicPlan,
    EmergencyOnly
}
public enum EResponse
{
    Deny,
    Prescribe,
    Treat
}

[CreateAssetMenu(fileName = "Patient", menuName = "Scriptable Objects/Patient")]
public class Patient : ScriptableObject
{
    public string patientName;
    public string doB;
    public string diagnosis;
    public EInsurance insuranceType;
    public EResponse correctResponse;
    public string insuranceLabel;
}
