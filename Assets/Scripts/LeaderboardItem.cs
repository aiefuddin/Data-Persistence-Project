using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text scoreText;

    public void Setup(DataManager.PlayerRecord record)
    {
        nameText.text = record.name;
        scoreText.text = record.score.ToString();
    }
}

