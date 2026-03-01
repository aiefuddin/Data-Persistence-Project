using TMPro;
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
}
