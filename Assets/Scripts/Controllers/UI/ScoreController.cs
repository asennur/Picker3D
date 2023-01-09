using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < 50; i++)
        {
            transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = ((i+1) * 10).ToString();
        }
    }
}
