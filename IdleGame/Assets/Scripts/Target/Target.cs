using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    //[SerializeField] private int maxHealth;  
    [SerializeField] private int currentHealth;
    [SerializeField] private AudioClip[] damageSoundClips;


    void Start()
    {
        currentHealth = GameManager.Instance.maxHealth;
    }

    public void DamageTarget(int _damage)
    {
        currentHealth -= _damage;

        //SoundManager.Instance.PlaySoundFxClip(damageSoundClip, transform, 1f);
        SoundManager.Instance.PlayRandomSoundFxClip(damageSoundClips, transform, 1f);

        if(currentHealth <= 0)
        {
            Die();

            int maxHealth = GameManager.Instance.maxHealth;
            int crystalsToAdd = 0;


            switch (maxHealth)
            {
                case <= 100:
                    crystalsToAdd = 1;
                    break;

                case > 100 and <= 200:
                    crystalsToAdd = 5;
                    break;

                case > 200 and <= 300:
                    crystalsToAdd = 10;
                    break;

                case > 300 and <= 400:
                    crystalsToAdd = 20;
                    break;

                case > 400 and <= 500:
                    crystalsToAdd = 30;
                    break;
                
                case > 500 and <= 600:
                    crystalsToAdd = 60;
                    break;
                
                case > 600 and <= 700:
                    crystalsToAdd = 120;
                    break;

                case > 700 and <= 800:
                    crystalsToAdd = 240;
                    break;
                
                case > 800 and <= 900:
                    crystalsToAdd = 480;
                    break;

                case > 900 and <= 1000:
                    crystalsToAdd = 1000;
                    break;

                default:
                    crystalsToAdd = 1500;
                    break;
            }
            if (GameManager.Instance.crystal == 100000000000 )//== long.MaxValue)
            {
                return;
            }
            else
            {
                GameManager.Instance.crystal += crystalsToAdd;
            }
        }
    }

    private void Die()
    {
        gameObject.SetActive(false); 

    }

    void OnEnable()
    {
        if(GameManager.Instance != null)
        {
            currentHealth = GameManager.Instance.maxHealth;
        }
        else
        {
            //Debug.Log("No GameManager.");
            return;
        }
        
    }
}
