using UnityEngine;
using System.Collections;
public class WallController : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private float delay;
    [SerializeField] private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject particles = Instantiate(particlePrefab, collision.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(DestroyParticlesAfterDelay(particles, delay));
        }
    }
    private IEnumerator DestroyParticlesAfterDelay(GameObject particles, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(particles);
    }
}
