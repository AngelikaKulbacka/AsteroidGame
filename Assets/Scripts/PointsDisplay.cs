using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PointsDisplay : MonoBehaviour
{
    TextMeshProUGUI pointsText;
    public static int currentPoints = 0;

    public int pointsToWin = 15;

    void Start()
    {
        pointsText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        pointsText.text = "Punkty: " + currentPoints;

        if (currentPoints >= pointsToWin)
        {
            SceneManager.LoadScene(2);
            currentPoints = 0;
        }
    }
}
