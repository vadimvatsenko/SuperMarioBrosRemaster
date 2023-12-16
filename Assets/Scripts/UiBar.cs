using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiBar : MonoBehaviour
{
    private TextMeshProUGUI _liveText;

    private void Start()
    {
        _liveText = GetComponent<TextMeshProUGUI>();
        _liveText.text = "x" + " " + GameManager.Instance.lives.ToString();
    }
}
