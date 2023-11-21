using UnityEngine;

public static class Extensions
{

    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }
        float radius = 0.25f; // это размеры коллайдера при падении
        float distance = 0.375f; // коллайдер будет внизу нашего игрока с радиусом 0,375

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
        // private static LayerMask layerMask = LayerMask.GetMask("Default");: 
        // Как я уже объяснил ранее, это создает маску слоя, включающую только слой с именем "Default".

        // public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction): 
        // Это определение статического метода Raycast, который является расширением для класса Rigidbody2D. 
        // Теперь каждый объект Rigidbody2D может вызывать этот метод, как если бы он был его собственным.

        // if (rigidbody.isKinematic) { return false; }: 
        // Проверяет, является ли rigidbody кинематическим (не реагирующим на физику), и если это так, возвращает false. 
        // Это делается, чтобы избежать выполнения лучевого лучения для кинематических объектов, так как они обычно не взаимодействуют с физикой мира.

        // float radius = 0.25f;: Устанавливает радиус для лучевого лучения.

        // float distance = 0.75f;: Устанавливает расстояние для лучевого лучения.

        // RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction, distance, layerMask);: 
        // Выполняет лучевое лучение в форме круга от текущей позиции rigidbody в указанном направлении direction с использованием заданного радиуса, 
        // максимального расстояния и маски слоя layerMask.

        // return hit.collider != null && hit.rigidbody != rigidbody;: 
        // Возвращает true, если лучевое лучение столкнулось с коллайдером (hit.collider != null) и если этот коллайдер не принадлежит тому же rigidbody, 
        // для которого выполняется лучевое лучение (hit.rigidbody != rigidbody). 
        // Это используется для определения, попал ли луч в другой объект (и не в текущий объект rigidbody).
    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {

        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
        // public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection): 
        // Это определение статического метода DotTest, который является расширением для класса Transform. 
        // Теперь каждый объект Transform может вызывать этот метод, как если бы он был его собственным.

        // Vector2 direction = other.position - transform.position;: 
        // Создает вектор direction, указывающий от текущего transform к позиции other.

        // return Vector2.Dot(direction.normalized, testDirection) > 0.25f;:
        // Вычисляет скалярное произведение нормализованных векторов direction и testDirection, а затем сравнивает результат с 0.25.

        // direction.normalized: Нормализует вектор direction, что означает, что его длина становится равной 1, сохраняя его направление.

        // Vector2.Dot(...): Скалярное произведение двух векторов. В данном случае, скалярное произведение нормализованных векторов.

        // > 0.25f: Возвращает true, если скалярное произведение больше 0.25, иначе возвращает false.

        // Этот метод, по сути, проверяет, находится ли точка other в определенном направлении(testDirection) относительно текущего transform.
        // Это может быть полезным, например, для определения, с какой стороны объекта находится другой объект относительно некоторого точечного точки в пространстве.
    }
}


