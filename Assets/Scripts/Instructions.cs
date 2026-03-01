using TMPro;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public TextMeshProUGUI textField;

    public void SetInstructions(string instructions)
    {
        textField.text = instructions;
    }
}
