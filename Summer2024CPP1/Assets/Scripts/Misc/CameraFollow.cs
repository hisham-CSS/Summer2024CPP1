using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minXClamp = -0.95f;
    public float maxXClamp = 236.9f;

    public float minYClamp = -0.95f;
    public float maxYClamp = 236.9f;

    //this function always runs after fixed update - unity specifies this is where camera movement should happen
    private void LateUpdate()
    {
        PlayerController pc = GameManager.Instance.PlayerInstance;
        Vector3 cameraPos = transform.position;

        cameraPos.x = Mathf.Clamp(pc.transform.position.x, minXClamp, maxXClamp);
        cameraPos.y = Mathf.Clamp(pc.transform.position.y, minYClamp, maxYClamp);

        transform.position = cameraPos;
    }
}
