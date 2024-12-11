/// <summary>
/// Health.cs
/// Author: MutantGopher
/// This is a sample health script.  If you use a different script for health,
/// make sure that it is called "Health".  If it is not, you may need to edit code
/// referencing the Health component from other scripts
/// </summary>

using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

public class Health : MonoBehaviour
{
	public bool canDie = true;					// Whether or not this health can die
	
	public float startingHealth = 100.0f;		// The amount of health to start with
	public float maxHealth = 100.0f;            // The maximum amount of health
	[HideInInspector]
	public float currentHealth;					// The current ammount of health

	public bool replaceWhenDead = false;		// Whether or not a dead replacement should be instantiated.  (Useful for breaking/shattering/exploding effects)
	public GameObject deadReplacement;			// The prefab to instantiate when this GameObject dies
	public bool makeExplosion = false;			// Whether or not an explosion prefab should be instantiated
	public GameObject explosion;				// The explosion prefab to be instantiated

	public bool isPlayer = false;				// Whether or not this health is the player
	public GameObject deathCam;					// The camera to activate when the player dies

	public bool dead = false;                  // Used to make sure the Die() function isn't called twice

	private Camera deathCamera;
	private bool gameStarts;
	public UIHealthManager UIHM;                // controls the script for managing the health bar

	public bool hasRevive;                      // works for the Revive ability

	public int pointValue = 10;
    public FirstPersonController playerPoints;
    public GameObject player;
	public WeaponSwap ws;

    public GameObject reviveSprite;
	private MaterialController materialController;
	private SkinnedMeshRenderer meshRenderer;



    // Use this for initialization
    void Start()
	{
		if (isPlayer)
		{
            reviveSprite.SetActive(false);
        }
        player = GameObject.Find("Player");
		ws = player.GetComponent<WeaponSwap>();
        playerPoints = player.gameObject.GetComponent<FirstPersonController>();
		if (this.tag == "Enemy")
		{
            materialController = GameObject.Find("GlobalController").GetComponent<MaterialController>();
            meshRenderer = this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        }
        // Initialize the currentHealth variable to the value specified by the user in startingHealth		
        try
		{
			if(deathCam != null)
				deathCamera = deathCam.GetComponent<Camera>();
		}
		finally
		{
			currentHealth = startingHealth;
			gameStarts = false;
		}
	}

	void Update()
	{
		if (currentHealth <= 0 && !dead && canDie)
		{
			Die();
		}

		/*if (Input.GetKeyDown("return") && !gameStarts)
		{
			gameStarts = true;
		}*/
	}

	public void ChangeHealth(float amount)
	{
		// Change the health by the amount specified in the amount variable
		currentHealth += amount;
		if (isPlayer)
		{
			UIHM.UpdateVals();
		}
		else
		{
			meshRenderer.material = materialController.damagedEnemy;
			StartCoroutine(EndFlash());
		}

		// If the health runs out, then Die.
		if (currentHealth <= 0 && !dead && canDie)
		{
			Die();
		}

		// Make sure that the health never exceeds the maximum health
		else if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
	}

	public void Die()
	{
        // This GameObject is officially dead.  This is used to make sure the Die() function isn't called again
        dead = true;

        // check if the player has a revive
        if (isPlayer && hasRevive)
		{
			currentHealth = maxHealth;
			hasRevive = false;
			dead = false;
            reviveSprite.SetActive(false);
            return;
		}

		// Make death effects
		if (replaceWhenDead)
			Instantiate(deadReplacement, transform.position, transform.rotation);
		if (makeExplosion)
			Instantiate(explosion, transform.position, transform.rotation);

		if (isPlayer && deathCam != null && dead)
		{
			PauseHandler.PS.canPause = false;
			deathCamera.enabled = true;
			StatisticManager.UpdateStat("Deaths", 1);
			StatisticLogHandler.SL.WriteGameStats();
		}

		// Loot drops
		if (this.tag == "Enemy")
		{
			float randomDrop = Random.Range(0, 10);
            playerPoints.UpdatePoints(pointValue);

			// update statistics for kills and points earned
			StatisticManager.UpdateStat("Kills", 1);
			StatisticManager.UpdateStat("Points", pointValue);


            if (randomDrop <= 2)
			{
				GameObject ammoDrop = ObjectPool.SharedInstance.GetPooledObject();
				if (ammoDrop != null)
				{
					ammoDrop.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
					ammoDrop.SetActive(true);
					StartCoroutine(DespawnAmmo(ammoDrop));
				}
			}
			else if (randomDrop >= 8)
			{
                GameObject healthDrop = ObjectPool2.SharedInstance.GetPooledObject();
				if (healthDrop != null)
				{
                    healthDrop.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
                    healthDrop.SetActive(true);
                    StartCoroutine(DespawnAmmo(healthDrop));
                }
            }
        }

		if (dead)
		{
            // Remove this GameObject from the scene
            Destroy(gameObject);
			
        }
	}

	void OnGUI()
	{
		if (this.tag == "Player" && gameStarts)
		{
            GUI.Label(new Rect(10, Screen.height - 70, 300, 100), "Health: " + currentHealth + "/" + maxHealth);
        }
    }

	private IEnumerator DespawnAmmo(GameObject ammo)
	{
		yield return new WaitForSeconds(20.0f);
		ammo.SetActive(false);
	}

	private IEnumerator EndFlash()
	{
		yield return new WaitForSeconds(0.2f);
        meshRenderer.material = materialController.normalEnemy;
    }
}
