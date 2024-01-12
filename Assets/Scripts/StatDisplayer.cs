using UnityEngine;
using UnityEngine.UI;

public class StatDisplayer : MonoBehaviour
{
    public Text wandText;
    public Text spellText;
    public Text robeText;
    public Text ringText;
    public Text attackText;
    public Text defenseText;
    public Text dexterityText;
    public Text speedText;
    public Text vitalityText;
    public Text wisdomText;

    public float originalProjectileEnemyTakeDamage = 50.0f;
    public float projectileEnemyTakeDamage; // make sure this is the same as originalPlayerTakeDamage during initialisation

    public float originalPlayerTakeDamage = 30.0f;
    public float playerTakeDamage;
    public float incrementalPlayerTakeDamage;

    public float originalSpellEnemyTakeDamage = 150.0f;
    public float spellEnemyTakeDamage;

    public float playerDefense = 0;

    public PlayerController player;

    private void Start() {
        projectileEnemyTakeDamage = originalProjectileEnemyTakeDamage;
        playerTakeDamage = originalPlayerTakeDamage;
        spellEnemyTakeDamage = originalSpellEnemyTakeDamage;
        incrementalPlayerTakeDamage = originalPlayerTakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        incrementalPlayerTakeDamage = incrementalPlayerTakeDamage + Time.deltaTime * originalPlayerTakeDamage / 200f; 
        
        this.wandText.text = "Wand: " + player.GetWand(); 

        this.spellText.text = "Spell: " + player.GetSpell();

        this.robeText.text = "Robe: " + player.GetRobe();

        this.ringText.text = "Ring: " + player.GetRing();

        this.attackText.text = "Bonus Attack: " + player.GetBonusAttack();
        this.defenseText.text = "Bonus Defense: " + player.GetBonusDefense();
        this.dexterityText.text = "Dexterity: " + player.GetDexterityMultiplier();
        this.speedText.text = "Speed: " + player.GetSpeedMultiplier();
        this.vitalityText.text = "Vitality: " + player.GetVitalityMultiplier();
        this.wisdomText.text = "Wisdom : " + player.GetWisdomMultiplier();
    }

    public float GetProjectileEnemyTakeDamage(){ // how much damage enemy takes from player projectiles
        projectileEnemyTakeDamage = player.GetBonusAttack() + originalProjectileEnemyTakeDamage;
        return projectileEnemyTakeDamage;
    }
    public float GetSpellEnemyTakeDamage(){ // how much damage enemy takes from player spell
        spellEnemyTakeDamage = player.GetBonusSpellDamage() + originalSpellEnemyTakeDamage;
        return spellEnemyTakeDamage;
    }

    public float GetPlayerTakeDamage(){ // how much damage the player takes
        playerTakeDamage = Mathf.Max(incrementalPlayerTakeDamage - player.GetBonusDefense(), 10);
        return playerTakeDamage;
    }
}
