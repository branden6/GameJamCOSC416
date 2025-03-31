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
            visual = transform.Find("Visual");

        if (visual != null && walkDirection != Vector3.zero)
        {
            // Flip only X scale (facing direction), keep size
            Vector3 originalScale = visual.localScale;
            visual.localScale = new Vector3(
                Mathf.Abs(originalScale.x) * Mathf.Sign(walkDirection.x),
                originalScale.y,
                originalScale.z
            );
        }
    }

    public void SetVisualScale(float scale)
    {
        if (visual == null)
            visual = transform.Find("Visual");

        if (visual != null)
        {
            float dir = Mathf.Sign(visual.localScale.x);
            visual.localScale = new Vector3(dir * scale, scale, scale);
        }
    }

    private void Update()
    {
        if (Time.time - spawnTime < edgeCheckDelay) return;

        Collider col = GetComponent<Collider>();
        float heightOffset = col ? col.bounds.extents.y : 0.5f;

        Vector3 rayOrigin = transform.position + walkDirection * 0.2f + Vector3.up * heightOffset * 0.5f;
        Debug.DrawRay(rayOrigin, Vector3.down * groundCheckDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            // Get slope tangent
            Vector3 slopeNormal = hit.normal;
            Vector3 slopeTangent = Vector3.Cross(Vector3.forward, slopeNormal).normalized;

            if (Vector3.Dot(slopeTangent, walkDirection) < 0)
                slopeTangent = -slopeTangent;

            // Move along slope
            transform.position += slopeTangent * walkSpeed * Time.deltaTime;

            // Stick to ground
            Vector3 groundedPos = transform.position;
            groundedPos.y = hit.point.y + heightOffset * 0.9f;
            transform.position = groundedPos;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("barrel"))
        {
            Destroy(other.gameObject);
            StartCoroutine(ShakeEffect());
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * 0.05f, new Vector3(0.8f, 0.1f, 0.2f));
    }
}
