using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player)
        {
            // Y값은 고정하고 X값만 플레이어를 따라감
            transform.position = new Vector3(player.position.x, transform.position.y, 0);
        }
    }
}
