using UnityEngine;

public class LootDropper : MonoBehaviour
{
    public int potions = 5;

    private int potentialDrops = 0;

    public GameObject PotionHealth;
    public GameObject PotionMagic;
    public GameObject PotionAttack;
    public GameObject PotionDefense;

    public GameObject RingDexterity;
    public GameObject RingSpeed;
    public GameObject RingVitality;
    public GameObject RingWisdom;

    private int itemNumber = 0;
    public GameObject[] equipables;

    public void dropItem(Vector3 location){
        if(potentialDrops%10 == 0 && potentialDrops != 0 && itemNumber <= 11){
            GameObject item = Instantiate<GameObject>(equipables[itemNumber]);
            location.y = 0.5f;
            item.transform.position = location;
            Destroy(item, 30f);
            itemNumber = itemNumber + 1;
        }
        else{
            // drop other items based on luck 5% chance for each potion and ring
            float rng = Random.Range(0f,99f);
            if(rng < 5f){
                GameObject item = Instantiate<GameObject>(PotionHealth);
                location.y = 0.5f;
                item.transform.position = location;
                Destroy(item, 10f);
            }
            else if(rng < 10f){
                GameObject item = Instantiate<GameObject>(PotionMagic);
                location.y = 0.5f;
                item.transform.position = location;
                Destroy(item, 10f);
            }
            else if(rng < 15f){
                GameObject item = Instantiate<GameObject>(PotionAttack);
                location.y = 0.5f;
                item.transform.position = location;
                Destroy(item, 10f);
            }
            else if (rng < 20f){
                GameObject item = Instantiate<GameObject>(PotionDefense);
                location.y = 0.5f;
                item.transform.position = location;
                Destroy(item, 10f);
            }
            else if (rng > 95f){
                GameObject item = Instantiate<GameObject>(RingDexterity);
                location.y = 0.5f;
                item.transform.position = location;
                Destroy(item, 10f);
            }
            else if (rng > 90f){
                GameObject item = Instantiate<GameObject>(RingSpeed);
                location.y = 0.5f;
                item.transform.position = location;
                Destroy(item, 10f);
            }
            else if (rng > 85f){
                GameObject item = Instantiate<GameObject>(RingVitality);
                location.y = 0.5f;
                item.transform.position = location;
                Destroy(item, 10f);
            }
            else if (rng > 80f){
                GameObject item = Instantiate<GameObject>(RingWisdom);
                location.y = 0.5f;
                item.transform.position = location;
                Destroy(item, 10f);
            }
        }
        potentialDrops = potentialDrops + 1;
    }
}
