using TMPro;
using UnityEngine;

public class MarioHubCanvas : MonoBehaviour
{
    [SerializeField] GameObject _marioLives;
    [SerializeField] GameObject _levelTime;
    [SerializeField] GameObject _stageAndLevel;
    [SerializeField] GameObject _coins;

    private void Start()
    {

        _stageAndLevel.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.world.ToString() + "-" + GameManager.Instance.stage.ToString();
    }

    private void Update()
    {
        _levelTime.GetComponent<TextMeshProUGUI>().text = GameManager.Instance._levelTime.ToString();
        _coins.GetComponent<TextMeshProUGUI>().text = " " + "x" + " " + GameManager.Instance.coins.ToString();
        _marioLives.GetComponent<TextMeshProUGUI>().text = " " + "x" + " " + GameManager.Instance.lives.ToString();
    }
}
