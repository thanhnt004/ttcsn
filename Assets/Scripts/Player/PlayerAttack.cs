using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float atackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] GameObject fireBall;
    private PlayerInputAction playerController;
    private Animator anim;
    private float cooldownTimer; 
    void Start()
    {
        playerController = new PlayerInputAction();
        playerController.Enable();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.Player.Attack.triggered && cooldownTimer>atackCooldown)
        {
            Attack();
            Debug.Log("Atk");
        }
        cooldownTimer += Time.deltaTime;
    }
    void Attack()
    {
        anim.SetTrigger("atk");
        cooldownTimer = 0;
        fireBall.transform.position = firePoint.position;
        fireBall.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
