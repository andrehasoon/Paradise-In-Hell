using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    private Animator anim;
    public bool gameOver = false;
    public float speed = 0.1f;
    private Rigidbody body;

    public PlayerProjectileController projectilePrefab;

    public PlayerProjectileController spellPrefab;
    public float spawnRadius = 1.0f;
    public float fireRate = 1.0f; // shots/second
    public float fireTimer = 0.0f;
    public float projectileSpeed = 15.0f;
    public float spellSpeed = 7.5f;




    public float maxHealth = 100;
    public float currHealth;
    public float vitality;
    public float maxMagic = 100;
    public float currMagic;
    public float wisdom;
    public float spellCost = 30;

    public string wand = "";
    public string spell = "";
    public string robe = "";
    public string ring = "";
    public int bonusAttack = 0;
    public int bonusDefense = 0;
    public float dexterityMultiplier = 1f;
    public float speedMultiplier = 1f;
    public float vitalityMultiplier = 1f;
    public float wisdomMultiplier = 1f;
    public int bonusSpellDamage = 0;

    private float mapSize = 50;
    public StatDisplayer stats;
    public bool dead = false;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        currHealth = maxHealth;
        currMagic = maxMagic;
        vitality = maxHealth/20;
        wisdom = maxMagic/20;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead){
            if((currHealth + Time.deltaTime * vitality) < maxHealth){
                currHealth = currHealth + Time.deltaTime * vitality * vitalityMultiplier;
            }
            else{
                currHealth = maxHealth;
            }

            if((currMagic + Time.deltaTime * wisdom) < maxMagic){
                currMagic = currMagic + Time.deltaTime * wisdom * wisdomMultiplier;
            }
            else{
                currMagic = maxMagic;
            }

            Vector3 mousePos  = Input.mousePosition;
            Vector3 playerPos  = Camera.main.WorldToScreenPoint(transform.position);

            Vector3 dir  = mousePos - playerPos;

            float angle  = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(-(angle-90), Vector3.up);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
                anim.SetBool("Walk_b", true);
            } else {
                anim.SetBool("Walk_b", false);
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) {
                transform.LookAt(transform.position + new Vector3(-1, 0, 1));
            } 

            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) {
                transform.LookAt(transform.position + new Vector3(-1, 0, -1));
            } 

            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
                transform.LookAt(transform.position + new Vector3(1, 0, -1));
            }

            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)) {
                transform.LookAt(transform.position + new Vector3(1, 0, 1));
            }

            else if (Input.GetKey(KeyCode.A)) {
                transform.LookAt(transform.position + Vector3.left);
            }

            else if (Input.GetKey(KeyCode.W)) {
                transform.LookAt(transform.position + Vector3.forward);
            }

            else if (Input.GetKey(KeyCode.S)) {
                transform.LookAt(transform.position + Vector3.back);
            }

            else if (Input.GetKey(KeyCode.D)) {
                transform.LookAt(transform.position + Vector3.right);
            } 

            if (Input.GetKeyDown(KeyCode.Space)) {
                if(currMagic >= spellCost){
                    currMagic = currMagic - spellCost;
                    transform.rotation = Quaternion.AngleAxis(-(angle-90), Vector3.up);
                    
                    // taken from workshop 9 and adapted
                    Vector2 mouseScreenPos = Input.mousePosition;
                    float distanceFromCameraToXZPlane = Camera.main.transform.position.y;
                    Vector3 screenPosWithZDistance = (Vector3)mouseScreenPos + (Vector3.forward * distanceFromCameraToXZPlane);
                    Vector3 fireToWorldPos = Camera.main.ScreenToWorldPoint(screenPosWithZDistance);
                    fireToWorldPos.y = this.transform.position.y;
                    Vector3 spawnLocation = fireToWorldPos + (new Vector3(0,2.851f,0));
                    Vector3 velocity = (fireToWorldPos - this.transform.position).normalized * spellSpeed;

                    if(Vector3.Distance(spawnLocation, this.transform.position) > 2){
                        PlayerProjectileController p = Instantiate<PlayerProjectileController>(spellPrefab);
                        p.Setup(spawnLocation, velocity);
                    }
                }
            }
        
            float xDirection = Input.GetAxis("Horizontal");
            float zDirection = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection).normalized;
            
            Vector3 newPos = transform.position + moveDirection * speed * speedMultiplier * Time.deltaTime;
            newPos.y = 0.00001f;

            if(newPos.x > mapSize/2f){
                newPos.x = mapSize/2f;
            }
            else if(newPos.x < -mapSize/2f){
                newPos.x = -mapSize/2f;
            }
            else if(newPos.z > mapSize/2f){
                newPos.z = mapSize/2f;
            }
            else if(newPos.z < -mapSize/2f){
                newPos.z = -mapSize/2f;
            }

            transform.position = newPos;

            if (Input.GetMouseButton(0))
            {
                transform.rotation = Quaternion.AngleAxis(-(angle-90), Vector3.up);
                if (fireTimer <= 0.0f){
                    anim.Play("Attack", -1, 0f);
                    // anim.SetBool("Attack_b", true);
                    fireTimer = 1.0f/(fireRate * dexterityMultiplier);

                    // taken from workshop 9 and adapted
                    Vector2 mouseScreenPos = Input.mousePosition;

                    float distanceFromCameraToXZPlane = Camera.main.transform.position.y;

                    Vector3 screenPosWithZDistance = (Vector3)mouseScreenPos + (Vector3.forward * distanceFromCameraToXZPlane);
                    Vector3 fireToWorldPos = Camera.main.ScreenToWorldPoint(screenPosWithZDistance);
                    fireToWorldPos.y = this.transform.position.y; 

                    PlayerProjectileController p = Instantiate<PlayerProjectileController>(projectilePrefab);

                    Vector3 spawn = (fireToWorldPos - this.transform.position).normalized * spawnRadius;
                    Vector3 spawnLocation = this.transform.position + (new Vector3(0,2.851f,0)) + spawn;
                    Vector3 velocity = (fireToWorldPos - this.transform.position).normalized * projectileSpeed;

                    p.Setup(spawnLocation, velocity);
                }
            }
            fireTimer -= Time.deltaTime;
        }
        else {
            float deathRotation = transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(0,deathRotation,0);
        }
    }

    private void OnTriggerEnter(Collider collider){
        if(!dead){
            if(collider.gameObject.tag.Contains("Wand")){
                wand = collider.gameObject.tag.Substring(5);
                bonusAttack = bonusAttack + 20;
                // stats.updateProjectileEnemyTakeDamage();
                Destroy(collider.gameObject);
            }
            else if(collider.gameObject.tag.Contains("Spell")){
                spell = collider.gameObject.tag.Substring(5);
                bonusSpellDamage = bonusSpellDamage + 100;
                // stats.updateSpellEnemyTakeDamage();
                spellCost = spellCost + 5;
                Destroy(collider.gameObject);
            }
            else if(collider.gameObject.tag.Contains("Robe")){
                robe = collider.gameObject.tag.Substring(5);
                bonusDefense = bonusDefense + 25;
                Destroy(collider.gameObject);
            }
            else if(collider.gameObject.tag.Contains("Ring")){
                ring = collider.gameObject.tag.Substring(5);
                if(collider.gameObject.tag.Contains("Dexterity")){
                    dexterityMultiplier = 2f;
                    speedMultiplier = 1f;
                    vitalityMultiplier = 1f;
                    wisdomMultiplier = 1f;
                }
                else if(collider.gameObject.tag.Contains("Speed")){
                    dexterityMultiplier = 1f;
                    speedMultiplier = 1.3f;
                    vitalityMultiplier = 1f;
                    wisdomMultiplier = 1f;
                }
                else if(collider.gameObject.tag.Contains("Vitality")){
                    dexterityMultiplier = 1f;
                    speedMultiplier = 1f;
                    vitalityMultiplier = 2f;
                    wisdomMultiplier = 1f;
                }
                else{
                    dexterityMultiplier = 1f;
                    speedMultiplier = 1f;
                    vitalityMultiplier = 1f;
                    wisdomMultiplier = 2f;
                }
                Destroy(collider.gameObject);
            }
            else if(collider.gameObject.tag.Contains("Potion")){
                if(collider.gameObject.tag.Contains("Attack")){
                    bonusAttack = bonusAttack + 10;
                    // stats.updateProjectileEnemyTakeDamage();
                }
                else if(collider.gameObject.tag.Contains("Defense")){
                    bonusDefense = bonusDefense + 5;
                }
                else if(collider.gameObject.tag.Contains("Health")){
                    maxHealth = maxHealth + 10;
                    currHealth = currHealth + 10;
                    vitality = maxHealth/20;
                }
                else{
                    maxMagic = maxMagic + 10;
                    currMagic = currMagic + 10;
                    wisdom = maxMagic/20;
                }
                Destroy(collider.gameObject);
            }
            else if(collider.gameObject.tag == "EnemyProjectile"){
                currHealth = currHealth - stats.GetPlayerTakeDamage();
                if(currHealth < 0){
                    dead = true;
                    anim.Play("Die", -1, 0f);
                    GameOverScreen.Setup(Time.timeSinceLevelLoad);
                }
            }
        }
    }

    public float GetMaxHealth(){
        return maxHealth;
    }

    public float GetCurrHealth(){
        return currHealth;
    }

    public float GetMaxMagic(){
        return maxMagic;
    }

    public float GetCurrMagic(){
        return currMagic;
    }

    public float GetBonusAttack(){
        return bonusAttack;
    }

    public float GetBonusSpellDamage(){
        return bonusSpellDamage;
    }

    public float GetBonusDefense(){
        return bonusDefense;
    }

    public string GetWand(){
        if (wand.Length > 2)
        {
            return wand.Substring(0,6);
        }
        else
        {
            return wand;
        }
    }

    public int GetWandDamage(){
        return (wand[5] - '0') * 50;
    }

    public string GetSpell(){
        if (spell.Length > 2)
        {
            return spell.Substring(0,6);
        }
        else
        {
            return spell;
        }
    }

    public int GetSpellDamage(){
        return bonusSpellDamage + 150; // 150 is bcs thats the starting value
    }

    public string GetRobe(){
        if (robe.Length > 2)
        {
            return robe.Substring(0,6);
        }
        else
        {
            return robe;
        }
    }

    public int GetRobeDefense(){
        return (robe[5] - '0') * 25;
    }

    public string GetRing(){
        if (ring.Length > 2)
        {
            return ring.Substring(4);
        }
        else 
        {
            return ring;
        }
    }

    public string GetRingStat(){
        return ring.Substring(4);
    }

    public string GetDexterityMultiplier(){
        return dexterityMultiplier + "X";
    }

    public string GetSpeedMultiplier(){
        return speedMultiplier + "X";
    }

    public string GetVitalityMultiplier(){
        return vitalityMultiplier + "X";
    }

    public string GetWisdomMultiplier(){
        return wisdomMultiplier + "X";
    }

    public bool IsDead(){
        return dead;    
    }
}
