using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.AddCoin();
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Vector3 restingPosition = transform.localPosition; // localPosition - это позиция относительно родителя
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f; // смещение вверх на 2ед относительно родителя 

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        Destroy(gameObject);

    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f; // сколько времени прошло с начала движения
        float duration = 0.25f; // установка длительности перемещения в 0.25 секунды

        while (elapsed < duration)
        {
            float t = elapsed / duration; // интерполяционный параметр - который показывает, насколько далеко мы продвинулись в анимации от начала до конца.
            transform.localPosition = Vector3.Lerp(from, to, t); // Vector3.Lerp - чтобы интерполировать позицию объекта между точкой from и точкой to в соответствии с параметром t.
            elapsed += Time.deltaTime; // увеличение переменной elapsed на количество прошедшего времени с момента последнего кадра.
            yield return null; // выполнение корутины будет приостановлено на один кадр, и затем продолжится в следующем кадре
        }

        transform.localPosition = to; //  установка окончательной позиции объекта, чтобы избежать ошибок округления и гарантировать, что объект находится в нужной позиции в конце анимации.

    }
}
