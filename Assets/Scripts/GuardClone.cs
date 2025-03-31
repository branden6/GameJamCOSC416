using UnityEngine;
using System.Collections;

public class GuardClone : MonoBehaviour
{
    public float walkSpeed = 1f;
    public float groundCheckDistance = 1.2f;
    public LayerMask groundLayer;

    private Vector3 walkDirection;
    private Transform visual;
    private float edgeCheckDelay = 0.15f;
    private float spawnTime;

    private void Start()
    {
        visual = transform.Find("Visual");
        spawnTime = Time.time;
    }

    public void Initialize(Vector3 direction)
    {
        walkDirection = direction.normalized;

        if (visual == null)
        {
            visual = transform.Find("Visual");
        }

        if (visual != null && walkDirection != Vector3.zero)
        {
            visual.localScale = new Vector3(Mathf.Sign(walkDirection.x), 1, 1);
        }
    }

    private void Update()
    {
        if (Time.time - spawnTime < edgeCheckDelay) return;

        Vector3 rayOrigin = transform.position + walkDirection * 0.3f + Vector3.up * 0.2f;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            // Move along slope
            Vector3 slopeNormal = hit.normal;
            Vector3 slopeTangent = Vector3.Cross(Vector3.forward, slopeNormal).normalized;

            // Ensure direction matches intended direction
            if (Vector3.Dot(slopeTangent, walkDirection) < 0)
                slopeTangent = -slopeTangent;

            // Move along slope
            transform.position += slopeTangent * walkSpeed * Time.deltaTime;

            // Stick to the ground based on height
            float heightOffset = 0.5f;

            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                heightOffset = col.bounds.extents.y;
            }
            else
            {
                Renderer rend = GetComponentInChildren<Renderer>();
                if (rend != null)
                {
                    heightOffset = rend.bounds.extents.y;
                }
            }

            Vector3 groundedPos = transform.position;
            groundedPos.y = hit.point.y + heightOffset;
            transform.position = groundedPos;

            }
            else
            {
                Destroy(gameObject);
            }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * 0.05f, new Vector3(0.8f, 0.1f, 0.2f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("barrel"))
        {
            Destroy(other.gameObject); // Remove the barrel
            StartCoroutine(ShakeEffect()); // Play hit shake
        }
    }

    private IEnumerator ShakeEffect()
    {
        Vector3 originalPos = transform.position;

        float shakeDuration = 0.1f;
        float shakeStrength = 0.05f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeStrength, shakeStrength);
            float offsetY = Random.Range(-shakeStrength, shakeStrength);
            transform.position = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }



}
