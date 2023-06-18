using System.Collections;
using UnityEngine;

public class DamageAura : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float delay;
    [SerializeField] int radius;

    void Awake()
    {
        StartCoroutine(Cr_Period());
    }
    IEnumerator Cr_Period()
    {
        DamageArea();

        yield return new WaitForSeconds(delay);

        yield return Cr_Period();

    }
    private void DamageArea()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position.To2D(), radius);
        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out IDamageable damageable))
                continue;

            damageable.Damage(damage);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
