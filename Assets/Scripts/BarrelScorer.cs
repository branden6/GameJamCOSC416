using UnityEngine;

public class BarrelScorer : MonoBehaviour
{
    private bool hasScored = false;

    void Update()
    {
        if (hasScored) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float playerY = player.transform.position.y;
        float barrelY = transform.position.y;
        float verticalDifference = playerY - barrelY;
        float horizontalDistance = Mathf.Abs(player.transform.position.x - transform.position.x);

        
        if (verticalDifference > 0.3f && verticalDifference <= 1.75f && horizontalDistance < 1.5f)
        {
            HUDManager hud = FindObjectOfType<HUDManager>();
            if (hud != null)
            {
                hud.AddScore(50);
                hasScored = true;
            }
        }
    }
}
