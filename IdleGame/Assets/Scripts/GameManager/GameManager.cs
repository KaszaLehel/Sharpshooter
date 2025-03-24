using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public long crystal = 0;

    [Header("Target Settings")]
    public float respawnTime = 0.2f;
    public int maxHealth = 100;
    public int currentTargetindex = 0; 

    [Header("Simple Arrow")]
    public float shotTime = 3f;
    public float hitChance = 0f;

    [Header("Green Arrow")]
    public bool isGreen = false;
    public float greenShotTime = 3f;
    public float greenHitChance = 0f;

    [Header("Red Arrow")]
    public bool isRed = false;
    public float redShotTime = 3f;
    public float redHitChance = 0f;


    [Header("Bool variables")]
    public bool shootBoth = false;
    public bool autoTarget = false;
    public bool isSecundTarget = false;

    [SerializeField] private GameObject leftTarget;
    [SerializeField] private GameObject rightTarget;
    [SerializeField] private GameObject leftTargetFoot;
    [SerializeField] private GameObject rightTargetFoot;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI crystalText;
    [SerializeField] private GameObject secundTargetText; 

    [Space(5)]
    [Header("GamePlayBools")]
    private bool _hasFirstShotChanged = false;
    private bool _hasTargetOpenChanged = false;
    public bool isFirstShot = false;
    public bool isTargetOpen = false;

    [Space(5)]
    public bool isGameFinished = false;
    public event Action OnFirstShotChanged;
    public event Action OnTargetOpenChanged;

    private bool isLeftTargetRespawning = false;
    private bool isRightTargetRespawning = false;


    public static GameManager Instance { get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        //DontDestroyOnLoad(gameObject);
        
    }

    void Start()
    {
        UpdateCrystalText();
        leftTargetFoot.SetActive(true);
        rightTargetFoot.SetActive(false);
        rightTarget.SetActive(false);
    }

    void Update()
    {
        if(shootBoth)
        {
            //respawnTime = 0.5f;
        }

        TargetRespawn();
        UpdateCrystalText();

        if(isSecundTarget)
        {
            ActivateSecundTarget();

            if (secundTargetText != null) 
            {
                secundTargetText.SetActive(true);
            }

            SetTargetOpen(true);
        }
    }

    void FixedUpdate()
    {
        CheckTheGameFinished();
    }

    #region Respawn Target
    void TargetRespawn()
    {
        float currentRespawnTime = respawnTime;

        if(autoTarget)
        {
            if(!leftTarget.activeSelf && !isLeftTargetRespawning)
            {
                
                StartCoroutine(LeftActivateTargetWithDelay(leftTarget, currentRespawnTime, true));
                
            }

            if(!rightTarget.activeSelf && !isRightTargetRespawning)
            {
                
                StartCoroutine(RightActivateTargetWithDelay(rightTarget, currentRespawnTime, true));
                
            }
        }
        else
        {
            if(!leftTarget.activeSelf)
            {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
                {
                    StartCoroutine(LeftActivateTargetWithDelay(leftTarget, currentRespawnTime, true));
                }
            }
            if(isSecundTarget)
            {
                if(!rightTarget.activeSelf)
                {
                    if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject()) 
                    {
                        StartCoroutine(RightActivateTargetWithDelay(rightTarget, currentRespawnTime, true));
                    }
                } 
            }
                   
        }
    }
#endregion

#region Crystal text
    void UpdateCrystalText()
    {
        if (crystalText != null)
        {
            crystalText.text = FormatNumber(crystal);//crystal.ToString();
        }
    }

    string FormatNumber(long number)
    {
        if(number < 1000)
        {
            return number.ToString();
        }
        float absNumber = Mathf.Abs(number);
        if (absNumber >= 1000000000) 
        {
            return (number / 1000000000f).ToString("0") + "B";
        }
        else if (absNumber >= 1000000) 
        {
            return (number / 1000000f).ToString("0") + "M";
        }
        else if (absNumber >= 1000) 
        {
            return (number / 1000f).ToString("0") + "K";
        }
        return number.ToString();
    }
#endregion 
    IEnumerator RightActivateTargetWithDelay(GameObject target, float _respawnTime, bool isRespawn)
    {
        if(isRespawn)
        {
            isRightTargetRespawning = true;
        }

        if(!target.activeSelf)
        {
            yield return new WaitForSeconds(_respawnTime);
            target.SetActive(true);
        }
        if(isRespawn)
        {
            isRightTargetRespawning = false;
        }
        /*else
        {
            target.SetActive(false);
            yield return new WaitForSeconds(_respawnTime);
            target.SetActive(true);
        }  */
    }

    IEnumerator LeftActivateTargetWithDelay(GameObject target, float _respawnTime, bool isRespawn)
    {
        if(isRespawn)
        {
            isLeftTargetRespawning = true;
        }

        if(!target.activeSelf)
        {
            yield return new WaitForSeconds(_respawnTime);
            target.SetActive(true);
        }
        if(isRespawn)
        {
            isLeftTargetRespawning = false;
        }
    }

    void ActivateSecundTarget()
    {
        rightTargetFoot.SetActive(true);
    }

#region Active Update Buttons
    public void SetFirstShot(bool value)
    {
        if (isFirstShot != value && !_hasFirstShotChanged)
        {
            isFirstShot = value;
            _hasFirstShotChanged = true;
            OnFirstShotChanged?.Invoke(); 
        }
    }

    public void SetTargetOpen(bool value)
    {
        if (isTargetOpen != value && !_hasTargetOpenChanged)
        {
            isTargetOpen = value;
            _hasTargetOpenChanged = true;
            OnTargetOpenChanged?.Invoke(); 
        }
    }
#endregion
#region Game End
    void CheckTheGameFinished()
    {
        if(maxHealth > 1000 &&
            shotTime < 0.15f &&
            hitChance == 100 &&
            greenShotTime < 0.15f &&
            respawnTime < 0.55 &&
            greenHitChance == 100 &&
            redShotTime < 0.15 &&
            redHitChance == 100 &&
            shootBoth == true &&
            autoTarget == true &&
            isSecundTarget == true &&
            isGameFinished == false)
            {
                isGameFinished = true;
                //Debug.Log("THE GAME FINISHED");
            }
    }
#endregion
}
