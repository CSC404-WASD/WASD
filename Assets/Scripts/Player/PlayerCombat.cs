using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerStats stats;
    ControllerLayouts cLayout;

    public Rigidbody rigidbody;
    private PlayerAudio _playerAudio;
    
    public Transform attackPoint;
    public Vector3 attackRange = new Vector3(0.5f, 0.5f, 0.25f);
    //cube is just a visual for now, once animation is added can be removed
    public GameObject attackIndicator;
    public float attackDuration = 0.25f;
    public LayerMask enemyLayers;

    public float upCooldown = 0.25f;
    float nextUpAttackTime = 0f;

    public float downCooldown = 2.0f;
    float nextDownAttackTime = 0f;
    //object to spawn
    public GameObject downMine;

    public float leftCooldown = 0.25f;
    public float dashLength = 0.5f;
    float nextLeftTime = 0f;

    public float rightCooldown = 0.25f;

    public float knockbackRadius = 1000f;
    public float knockbackPower = 10f;
    public float knockbackStun = 1f; // how long to stun enemy on knockback
    private float nextRightTime = 0f;

    void Start()
    {
        stats = PlayerStats.instance;
        cLayout = ControllerLayouts.instance;
        if (cLayout == null) // If you are opening scenes from outside the menu. Debug.
        {
            cLayout = this.gameObject.AddComponent(typeof(ControllerLayouts)) as ControllerLayouts;
            cLayout.setLayout(ControllerType.XBOX360);
        }
        rigidbody = GetComponent<Rigidbody>();
        _playerAudio = GetComponent<PlayerAudio>();
    }

    void FixedUpdate()
    {
        // If attack in progress, poll for that.
        if(stats.isAttacking)
        {
            Collider[] hitColliders = Physics.OverlapBox(attackPoint.position, attackRange, Quaternion.identity, enemyLayers);
            foreach(Collider enemy in hitColliders) {
                //might want to make an Enemy file for this
                var enemyAI = enemy.GetComponent<BaseEnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.Die();
                }
            }
        }
    }
    void Update()
    {


        // Else, check for inputs.
        if(!stats.isAttacking) {
            //testing with ps4 controller on mac, button 6 = press left trigger button 7 = press right trigger
            // can also use axis 5 for a val between -1 and 1 (left trigger), or axis 6 for rt
            if (Input.GetKeyDown(KeyCode.U) && !stats.isAttacking && !stats.isDashing) {
                PerformUpAttack();
            // button 3 is triangle on ps (up) and y on xbox360 (up)
            } else if (Input.GetKeyDown(cLayout.upButton()) && !stats.isAttacking && !stats.isDashing) {
                PerformUpAttack();
            }

            if (Input.GetKeyDown(KeyCode.J) && !stats.isDashing)
            {
                PerformDownAttack();
            }
            // button 3 is triangle on ps (up) and y on xbox360 (up)
            //joystick button 1 = x (down) for ps4 controller, b (right) for xbox360 thanks devs
            else if (Input.GetKeyDown(cLayout.downButton()) && !stats.isDashing) {
                PerformDownAttack();
            }

            //button 2 is right (circle) on ps4 and left (x) on xbox360
            if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(cLayout.rightButton())) && !stats.isAttacking && !stats.isDashing)
            {
                PerformDKnockback();
            }

            if (Input.GetKeyDown(KeyCode.H) && !stats.isDashing && !stats.isAttacking) {
                PerformADash();
            //button 0 is left on ps4(square) and down (a) on xbox360
            } else if (Input.GetKeyDown(cLayout.leftButton()) && !stats.isDashing && !stats.isAttacking) {
                PerformADash();
            }
        }
    }

    private void PerformUpAttack() {

        // check vertical charge
        float vCharge = stats.getVerticalCharge();

        if (vCharge <= stats.upChargeConsumption)
        {
            if (vCharge > 0)
            {
                _playerAudio.PlayFartSound();
                stats.setVerticalDiff(-1 * vCharge);
            }
            return;
        }
        
        if (nextUpAttackTime > Time.time)
        {
            return;
        }
        
        stats.setVerticalDiff(-1f * stats.upChargeConsumption);

        // execute attack
        stats.isAttacking = true;
        attackIndicator.SetActive(true);

        // actual collider check occurs in FixedUpdate.

        //delay next attack
        _playerAudio.PlayUpSound();
        nextUpAttackTime = Time.time + upCooldown;
        StartCoroutine(HideCube(attackDuration));
    }

    private void PerformDownAttack() {
        // check vertical charge and convert to positive (if in down) for easy use
        float vCharge = stats.getVerticalCharge() * -1;
        
        if (vCharge < stats.downChargeConsumption)
        {
            if (vCharge > 0)
            {
                _playerAudio.PlayFartSound();
                stats.setVerticalDiff(vCharge);
            }
            return;
        }
        
        if (nextDownAttackTime > Time.time)
        {
            return;
        }
        
        stats.setVerticalDiff(stats.downChargeConsumption);

        Instantiate(downMine, this.transform.position + new Vector3(1,0,1), Quaternion.identity);

        _playerAudio.PlayDownSound();
        //delay next attack
        nextDownAttackTime = Time.time + downCooldown;
    }

    private void PerformADash() {
        // check horizontal charge
        var hCharge = -1 * stats.getHorizontalCharge();

        if (hCharge < stats.leftChargeConsumption)
        {
            if (hCharge > 0)
            {
                _playerAudio.PlayFartSound();
                stats.setHorizontalDiff(hCharge);
            }

            return;
        }
        
        if (nextLeftTime > Time.time)
        {
            return;
        }
        stats.setHorizontalDiff(stats.leftChargeConsumption);

        // execute dash
        stats.isDashing = true;

        _playerAudio.PlayLeftSound();
        //delay next dash
        nextLeftTime = Time.time + leftCooldown;
        StartCoroutine(FinishDash(dashLength));
    }
    
    private void PerformDKnockback()
    {
        var charge = stats.getHorizontalCharge();

        if (charge < stats.rightChargeConsumption)
        {
            if (charge > 0)
            {
                _playerAudio.PlayFartSound();
                stats.setHorizontalDiff(-1 * charge);
            }
            return;
        }
        
        if (nextRightTime > Time.time)
        {
            return;
        }
        
        stats.setHorizontalDiff(-1f * stats.rightChargeConsumption); // reversed
        
        // find entities to knock back
        var origin = rigidbody.position;
        var colliders = Physics.OverlapSphere(origin, knockbackRadius);
        stats.isAttacking = true;
        foreach (var other in colliders)
        {
            var collidedBody = other.GetComponent<Rigidbody>();
            if (collidedBody == null)
            {
                continue;
            }
            var direction = origin - other.transform.position;
            other.attachedRigidbody.velocity = -direction.normalized * knockbackPower;
            var enemyAI = other.GetComponent<BaseEnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.stunForDuration(knockbackStun);
            }
        }
        
        _playerAudio.PlayRightSound();
        // delay next knockback
        nextRightTime = Time.time + rightCooldown;
        stats.isAttacking = false;
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }

    IEnumerator HideCube(float time) {
        yield return new WaitForSeconds(time);
        attackIndicator.SetActive(false);
        stats.isAttacking = false;
    }

    IEnumerator FinishDash(float time) {
        yield return new WaitForSeconds(time);
        stats.isDashing = false;
    }
}
