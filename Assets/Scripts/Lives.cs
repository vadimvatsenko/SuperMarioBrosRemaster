using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    private Text _livesText;

    void Awake()
    {
        _livesText = GetComponent<Text>();
    }

    void Update()
    {
        if (GameManager.Instance != null)
        {
            _livesText.text = "Lives: " + GameManager.Instance.lives.ToString();
        }
        else
        {
            Debug.LogError("GameManager instance is missing!");
        }
    }
}
