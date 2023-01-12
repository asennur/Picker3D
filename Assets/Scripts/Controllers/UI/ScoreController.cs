using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Sirenix;
using Sirenix.OdinInspector;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private List<TextMeshPro> scores = new List<TextMeshPro>();
    [Button]
    private void ScoreValues()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            scores[i].text = ((i+1) * 10).ToString();
        }
    }
}
