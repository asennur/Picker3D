using TMPro;
using UnityEngine;

public class ScoreWriting : MonoBehaviour
{
    private float _score;
    private void OnEnable()
    {
        _score = SliderController.Instance.finalScore;
        Debug.Log( $"Score: {_score}");
        WriteScore();
    }

    private void WriteScore()
    {
        transform.GetComponent<TextMeshProUGUI>().text = "Score: " + (Mathf.Ceil(_score * 50) * 10);
    }
}
