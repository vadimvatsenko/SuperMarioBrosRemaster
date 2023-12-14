using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    [SerializeField] private Transform flag;
    [SerializeField] private Transform poleBotton;
    [SerializeField] private Transform castle;
    [SerializeField] private float speed = 6f;
    [SerializeField] private int nextWorld = 1;
    [SerializeField] private int nextStage = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, poleBotton.position));
            StartCoroutine(LevelComplete(other.transform));
        }
    }

    private IEnumerator LevelComplete(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;

        yield return MoveTo(player, poleBotton.position);
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, castle.position);

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        GameManager.Instance.LoadLevel(nextWorld, nextStage);
    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while (Vector3.Distance(subject.position, destination) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = destination;
    }
}
