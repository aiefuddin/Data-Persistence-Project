using UnityEngine;

public class LeaderboardPage : MonoBehaviour
{
    public Transform contentParent;
    public GameObject leaderboardEntryPrefab;

    private void OnEnable()
    {
        PopulateLeaderboard();
    }

    void PopulateLeaderboard()
    {
        // Clear existing entries
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Sort players by score in descending order
        DataManager.Instance.players.Sort((a, b) => b.score.CompareTo(a.score));

        // Create leaderboard entries

        foreach (var player in DataManager.Instance.players)
        {
            GameObject entry = Instantiate(leaderboardEntryPrefab, contentParent);

            LeaderboardItem ui = entry.GetComponent<LeaderboardItem>();
            ui.Setup(player);

        }
    }

}
