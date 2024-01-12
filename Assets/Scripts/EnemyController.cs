using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public EnemyProjectileController projectilePrefab;
    public float startingHealth = 100f;
    private float projectileTakeDamage;

    private float spellTakeDamage;
    public float lookRadius = 20.0f;
    private Transform target;

    public GameObject explosionPrefab;

    private LootDropper lootData;

    public float spawnRadius = 1.0f;
    public float fireRate = 2.0f; // shots/second
    public float fireTimer = 0.0f;
    public bool animationTimer = true; 
    public bool dead;
    public float deathtimer = 1.13f;
    public float projectileSpeed = 15.0f;
    NavMeshAgent agent;

    private Animator anim;

    public void Setup(LootDropper Data, Vector3 randomPos, Transform toFollow, float projectileTakeDamage, float spellTakeDamage){
        float timeNow = Time.timeSinceLevelLoad;
        startingHealth = startingHealth + Mathf.Pow(timeNow, 0.9f);
        lootData = Data;
        this.gameObject.transform.position = randomPos;
        agent = GetComponent<NavMeshAgent>();
        target = toFollow;
        this.projectileTakeDamage = projectileTakeDamage;
        this.spellTakeDamage = spellTakeDamage;
        anim = GetComponent<Animator>();
        dead = false;
    }

    void Update(){
        if(!dead){
            float distance = Vector3.Distance(target.position, transform.position);
            if(distance <= lookRadius){
                agent.SetDestination(target.position);
            }

            if(fireTimer <= 0.5f && animationTimer){
                anim.Play("Attack1", -1, 0f);
                animationTimer = false;
            }

            if(fireTimer <= 0.0f){
                fireTimer = 1.0f/fireRate;
                animationTimer = true;
                EnemyProjectileController p = Instantiate<EnemyProjectileController>(projectilePrefab);
                Vector3 spawn = (target.position - this.transform.position).normalized * spawnRadius;
                p.transform.position = this.transform.position + (new Vector3(0,1.5f,0)) + spawn;
                Vector3 vel = target.position - this.transform.position;
                vel.y = 0.0f;
                p.velocity = vel.normalized * projectileSpeed;
            }
            fireTimer -= Time.deltaTime;
        }
        else{
            deathtimer -= Time.deltaTime;
            if(deathtimer <= 0){
                GameObject explosion = Instantiate(this.explosionPrefab);
                explosion.transform.position = this.transform.position;
                Destroy(this.gameObject);
                lootData.dropItem(this.transform.position);
            }
        }
        
    }

    private void OnTriggerEnter(Collider collider){
        if(!dead){
            if(collider.gameObject.tag == "PlayerProjectile"){
                anim.Play("Hit", -1, 0f);
                startingHealth = startingHealth - projectileTakeDamage;
                if(startingHealth <= 0){
                    dead = true;
                    anim.Play("Death", -1, 0f);
                }
            }
            if(collider.gameObject.tag == "SpellProjectile"){
                anim.Play("Hit", -1, 0f);
                startingHealth = startingHealth - spellTakeDamage;
                if(startingHealth <= 0){
                    dead = true;
                    anim.Play("Death", -1, 0f);
                }
            }
        }
    }
}
