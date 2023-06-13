using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsDisplay : MonoBehaviour
{
    TextMeshProUGUI pointsText;
    public static int currentPoints = 0;

    void Start()
    {
        pointsText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        pointsText.text = "Punkty: " + currentPoints;
    }
}
