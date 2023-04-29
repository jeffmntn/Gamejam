using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisions : MonoBehaviour
{

    public LayerMask layer;
    public float radius = 1f;
    public float damage = 2f;

    public bool isPlayer, isEnemy;
    public GameObject hitSfx;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetectCollision();
    }

    void DetectCollision()
    {
       Collider[] hit = Physics.OverlapSphere(transform.position, radius, layer);

        if (hit.Length > 0)
        {
            if (isPlayer)
            {
                Vector3 hitSfxPos = hit[0].transform.position;
                hitSfxPos.y += 1.3f;

                if (hit[0].transform.forward.x > 0)
                {
                    hitSfxPos.y += 0.3f;
                }
                else if (hit[0].transform.forward.x < 0)
                {
                    hitSfxPos.y -= 0.3f;
                }

                GameObject hitSfxPrefab = Instantiate(hitSfx, hitSfxPos, Quaternion.identity);
                Destroy(hitSfxPrefab, 1f);

                if (gameObject.CompareTag("Head"))
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, true);
                }
                else
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
                }
            }

            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
