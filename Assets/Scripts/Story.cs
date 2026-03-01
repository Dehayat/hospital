using System.Collections;
using TMPro;
using UnityEngine;

public class Story : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI storyText;

    public string dayPrefix = "Day ";

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ShowStory(int day, string text)
    {
        storyText.text = text;
        dayText.text = dayPrefix + (day+1);
        anim.SetTrigger("Story");
    }
}
