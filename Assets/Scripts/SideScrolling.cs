
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float cameraHeight = 5.5f;
    [SerializeField] private float undergroundCameraHeight = -9.5f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }

    public void SetUnderground(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundCameraHeight : cameraHeight;
        transform.position = cameraPosition;
    }
}
