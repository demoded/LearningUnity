using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class GamePlayUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;

    private void Awake()
    {
        Assert.IsNotNull(scoreLabel);
    }

    void Update()
    {
        var scoreAsInt = (int)GameManager.instance.Score;
        scoreLabel.text = scoreAsInt.ToString();
    }
}
