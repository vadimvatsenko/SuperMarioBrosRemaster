using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform _connection; // место куда будет телепортироватся Марио
    [SerializeField] private KeyCode _enterKeyCode = KeyCode.S;
    [SerializeField] private Vector3 _enterDirection = Vector3.down;
    [SerializeField] private Vector3 _exitDerection = Vector3.zero;
    private void OnTriggerStay2D(Collider2D other) // OntriggerStay2D Будет ждать всегда нажатие на кнопку входа а трубу
    {
        if (_connection != null && other.CompareTag("Player"))
        {

            if (Input.GetKeyDown(_enterKeyCode))
            {
                StartCoroutine(Enter(other.transform));
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        Vector3 enteredPosition = transform.position + _enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f; // уменьшаем Марио при захождение трубу

        yield return Move(player, enteredPosition, enteredScale);
        yield return new WaitForSeconds(1f); //Это выражение означает паузу в выполнении корутины на определенное количество секунд

        bool underground = _connection.position.y < 0f;
        Camera.main.GetComponent<SideScrolling>().SetUnderground(underground); // получаем доступ к методу скрипта  SetUnderground передаем значение underground

        if (_exitDerection != Vector3.zero)
        { // если координаты выхода не равны нулю, то...
            player.position = _connection.position - _exitDerection;
            yield return Move(player, _connection.position + _exitDerection, Vector3.one); // Vector3.one, чтобы обеспечить ему нормальный, стандартный размер
        }
        else
        {
            player.position = _connection.position;
            player.localScale = Vector3.one;
        }
        player.GetComponent<PlayerMovement>().enabled = true;


    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;

    }
}
