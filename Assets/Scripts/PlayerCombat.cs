using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer; 
    private Animator m_animator;

    public float attackRange = 0.5f;
    public int attackDamage = 10;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    public Button attackButton;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        attackButton.onClick.AddListener(() => buttonCallBack(attackButton));
    }

    // Update is called once per frame

    void buttonCallBack(Button buttonPressed)
    {
        if (Time.time >= nextAttackTime)
        {
            if (buttonPressed == attackButton)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }
    }

    void Attack()
    {

        m_animator.SetTrigger("Attack");
             
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer); 
        
        // Creates a circle with given radius in that object and collects all objects that hits 
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hitted Enemy " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            enemy.GetComponent<Enemy>().EnemyDeath();
        }

} 
    // To define best attack range with Gizmos
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
