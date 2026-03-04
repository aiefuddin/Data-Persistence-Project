using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public TMP_InputField usernameInputField;

    public void StartNewGame()
    {
        string playerName = usernameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name cannot be empty!");
            return;
        }

        DataManager.Instance.playerName = playerName;

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit(); // original code to quit Unity player
    #endif
    }

    public void GoToLeaderboard()
    {
        SceneManager.LoadScene(2);
    }
}
