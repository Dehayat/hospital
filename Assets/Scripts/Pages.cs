using UnityEngine;

public class Pages : MonoBehaviour
{
    public GameObject[] pages;

    private int currentPage = 0;

    private void Start()
    {
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
        pages[0].SetActive(true);
        currentPage = 0;
    }

    public void NextPage()
    {
        if (currentPage < pages.Length-1)
        {
            pages[currentPage].SetActive(false);
            currentPage++;
            pages[currentPage].SetActive(true);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            pages[currentPage].SetActive(false);
            currentPage--;
            pages[currentPage].SetActive(true);
        }
    }
}
