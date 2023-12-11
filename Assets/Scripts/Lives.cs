using System.Collections;
using System.Collections.Generic;
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
        // Проверяем, существует ли экземпляр GameManager
        if (GameManager.Instance != null)
        {
            // Получаем количество жизней из GameManager и устанавливаем текст
            _livesText.text = "Lives: " + GameManager.Instance.lives.ToString();
        }
        else
        {
            Debug.LogError("GameManager instance is missing!");
        }
    }
}
