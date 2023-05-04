using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisions : MonoBehaviour
{

    public LayerMask layer;
    public float radius = 1f;
    public float damage = 10f;
    public float knockDownDamage = 15f;

    public bool isPlayer, isEnemy;
    public GameObject hitSfx;
    public PlayerAttackCombo playerAttack;
    // Start is called before the first frame update
    void Awake()
    {
        playerAttack = GetComponentInParent<PlayerAttackCombo>();
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
                Vector3 hitSfxPos = transform.position;                
                GameObject hitSfxPrefab = Instantiate(hitSfx, hitSfxPos, Quaternion.identity);
                hitSfxPrefab.transform.SetParent(hit[0].GetComponent<Transform>());
                Destroy(hitSfxPrefab, 1f);

                if (playerAttack.comboCount >=3 && playerAttack.comboCount <= 4)
                {
                    if(Random.Range(0,2) >= 1)
                    {
                        hit[0].GetComponent<HealthScript>().ApplyDamage(knockDownDamage, true);
                    }             
                }
                if(playerAttack.comboCount >= 0 && playerAttack.comboCount <= 2)
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
                }
            }
            if(isEnemy)
            {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);              
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
