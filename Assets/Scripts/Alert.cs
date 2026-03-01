using System.Collections;
using TMPro;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public Transform startPos;
    public TextMeshProUGUI nameLabel;

    public float moveDuration;

    private RectTransform rect;
    private DraggableItem draggable;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        draggable = GetComponent<DraggableItem>();
    }

    private void Start()
    {
        DisableDrag();
        StartCoroutine(GoToStartPos());
    }

    private void DisableDrag()
    {
        draggable.enabled = false;
    }
    private void EnableDrag()
    {
        draggable.enabled = true;
    }

    private IEnumerator GoToStartPos()
    {
        var initialPos = rect.position;
        float t = 0;

        while (Vector3.Distance(rect.position, startPos.position) > 0.2f)
        {
            t += Time.deltaTime;
            rect.position = Vector3.Lerp(initialPos, startPos.position, t / moveDuration);
            yield return new WaitForEndOfFrame();
        }
        EnableDrag();
    }
}
