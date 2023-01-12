using Signals;
using TMPro;
using UnityEngine;

public class ScoreWriting : MonoBehaviour
{
    private float _score;
    private void OnEnable()
    {
        _score = (float) UISignals.Instance.onGetScore?.Invoke();
        WriteScore();
    }

    private void WriteScore()
    {
        transform.GetComponent<TextMeshProUGUI>().text = "Score: " + (Mathf.Ceil(_score * 50) * 10);
    }
}
