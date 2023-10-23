
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    private void Awake()
    {
        // GameObject.FindWithTag("Player"): Эта часть кода выполняет поиск игрового объекта, 
        //который имеет указанный тег "Player".В Unity вы можете присваивать теги игровым объектам для легкой классификации и идентификации. 
        //В данном случае, мы ищем объект с тегом "Player".

        //.transform: Как только найден объект с тегом "Player", мы получаем компонент Transform этого объекта. 
        //Компонент Transform отвечает за хранение и управление позицией, вращением и масштабом объекта в 3D или 2D пространстве в Unity.
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }
}
