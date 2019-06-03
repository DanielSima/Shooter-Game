using System.Collections;
using UnityEngine;

public class FireBullet : Bullet
{
    //!!!!reset the component in inspector when changing those values!!!!
    public void Reset()
    {
        damage = 20f;
    }

    protected override void DoDamage(Character character, float damage)
    {
        StartCoroutine(ContinuousDamage(character, damage));
    }

    /// <summary>
    /// Does continous damage to a character.
    /// </summary>
    public IEnumerator ContinuousDamage(Character character, float damage)
    {
        //move the bullet outside playable area
        this.transform.position = new Vector3(100, 100, 100);
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //gradually change health
        for(int i = 0; i < 10; i++)
        {
            character.ChangeHealth(-damage/10);
            yield return new WaitForSeconds(1f);
        }

        //destroy the bullet
        Destroy(this.gameObject);
    }
}