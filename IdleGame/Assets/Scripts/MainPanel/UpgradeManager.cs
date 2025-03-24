using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("Interactable Buttons")]
    [SerializeField] private Button GreenArrowButonUpgrade;
    [SerializeField] private Button RedArrowButtonUpgrade;
    [SerializeField] private Button DoubleTargetButtonUpgrade;
    [SerializeField] private Button AutoTargetButtonUpgrade;
    [SerializeField] private Button SecondTargetButtonUpgrade;

    [Header("AutoRespawnTargetSpeed")]
    [SerializeField] private Button AutoTargetRespawnSpeed;
    [SerializeField] private TextMeshProUGUI autoTargetRespawnText;
    private long autoTargetRespawnPrice = 100;

    [Space(5)]
    [Header("Target Health")]
    [SerializeField] private Button TargetHealtButtonUpgrade;
    [SerializeField] private TextMeshProUGUI healthTargetPriceText;
    private long healthCurrentPrice = 2;

    [Header("Simple Speed")]
    [SerializeField] private Button ArrowSpeedUpgrade;
    [SerializeField] private TextMeshProUGUI arrowSpeedPriceText;
    private long arrowSpeedCurrentPrice = 1;

    [Header("Simple Accuracy")]
    [SerializeField] private Button ArrowaccuracyUpgrade;
    [SerializeField] private TextMeshProUGUI arrowAccuracyPriceText;
    private long arrowAccuracyCurrentPrice = 1;

    [Header("Green Speed")]
    [SerializeField] private Button GreenArrowSpeedUpgrade;
    [SerializeField] private TextMeshProUGUI greenArrowSpeedPriceText;
    private long greenArrowSpeedCurrentPrice = 3;

    [Header("Green Accuracy")]
    [SerializeField] private Button GreenArrowaccuracyUpgrade;
    [SerializeField] private TextMeshProUGUI greenArrowAccuracyPriceText;
    private long greenArrowAccuracyCurrentPrice = 20;

    [Header("Red Speed")]
    [SerializeField] private Button RedArrowSpeedUpgrade;
    [SerializeField] private TextMeshProUGUI redArrowSpeedPriceText;
    private long redArrowSpeedCurrentPrice = 3;

     [Header("Red Accuracy")]
    [SerializeField] private Button RedArrowaccuracyUpgrade;
    [SerializeField] private TextMeshProUGUI redArrowAccuracyPriceText;
    private long redArrowAccuracyCurrentPrice = 20;



    void Start()
    {
        GreenArrowButonUpgrade.gameObject.SetActive(false);
        RedArrowButtonUpgrade.gameObject.SetActive(false);
        AutoTargetButtonUpgrade.gameObject.SetActive(false);
        AutoTargetRespawnSpeed.gameObject.SetActive(false);

        GreenArrowSpeedUpgrade.gameObject.SetActive(false);
        GreenArrowaccuracyUpgrade.gameObject.SetActive(false);
        RedArrowSpeedUpgrade.gameObject.SetActive(false);
        RedArrowaccuracyUpgrade.gameObject.SetActive(false);

        UpdatePriceText(healthTargetPriceText, healthCurrentPrice);
        UpdatePriceText(arrowSpeedPriceText, arrowSpeedCurrentPrice);
        UpdatePriceText(arrowAccuracyPriceText, arrowAccuracyCurrentPrice);
        UpdatePriceText(greenArrowSpeedPriceText, greenArrowSpeedCurrentPrice);
        UpdatePriceText(greenArrowAccuracyPriceText, greenArrowAccuracyCurrentPrice);
        UpdatePriceText(redArrowSpeedPriceText, redArrowSpeedCurrentPrice);
        UpdatePriceText(redArrowAccuracyPriceText, redArrowAccuracyCurrentPrice);
    }   

#region Arrow Speed
    public void ArrowShotSpeed()
    {
        if(GameManager.Instance.crystal >= arrowSpeedCurrentPrice)
        {
            GameManager.Instance.crystal -= arrowSpeedCurrentPrice;
            GameManager.Instance.shotTime -= 0.1f;

            if(GameManager.Instance.shotTime <= 0.15f)
            {
                UpdatePriceText(arrowSpeedPriceText, 0);
                ArrowSpeedUpgrade.interactable = false;
                return;
            }

            //arrowSpeedCurrentPrice = NextFibNumber(arrowSpeedCurrentPrice);
            arrowSpeedCurrentPrice = NextPrice(arrowSpeedCurrentPrice);
            UpdatePriceText(arrowSpeedPriceText, arrowSpeedCurrentPrice);
        }
        else
        {
            Debug.Log("No enaught crystal.");
        }
    }
#endregion

#region Arrow Accuracy
    public void ArrowAccuracy()
    {
        if(GameManager.Instance.crystal >= arrowAccuracyCurrentPrice)
        {
            GameManager.Instance.crystal -= arrowAccuracyCurrentPrice;
            GameManager.Instance.hitChance += 5f;

            if(GameManager.Instance.hitChance >= 100)
            {
                UpdatePriceText(arrowAccuracyPriceText, 0);
                ArrowaccuracyUpgrade.interactable = false;
                return;
            }
            arrowAccuracyCurrentPrice = NextPrice(arrowAccuracyCurrentPrice);
            UpdatePriceText(arrowAccuracyPriceText, arrowAccuracyCurrentPrice);
        }
        else
        {
            Debug.Log("No enaught crystal.");
        }
    }
#endregion

#region Green Arrow Speed
    public void GreenArrowShotSpeed()
    {
        if(GameManager.Instance.crystal >= greenArrowSpeedCurrentPrice)
        {
            GameManager.Instance.crystal -= greenArrowSpeedCurrentPrice;
            GameManager.Instance.greenShotTime -= 0.1f;

            if(GameManager.Instance.greenShotTime <= 0.15f)
            {
                UpdatePriceText(greenArrowSpeedPriceText, 0);
                GreenArrowSpeedUpgrade.interactable = false;
                return;
            }

            greenArrowSpeedCurrentPrice = NextPrice(greenArrowSpeedCurrentPrice);
            UpdatePriceText(greenArrowSpeedPriceText, greenArrowSpeedCurrentPrice);
        }
        else
        {
            Debug.Log("No enaught crystal.");
        }
    }
#endregion

#region Green Arrow Accuracy
    public void GreenArrowAccuracy()
    {
        if(GameManager.Instance.crystal >= greenArrowAccuracyCurrentPrice)
        {
            GameManager.Instance.crystal -= greenArrowAccuracyCurrentPrice;
            GameManager.Instance.greenHitChance += 5f;

            if(GameManager.Instance.greenHitChance >= 100f)
            {
                UpdatePriceText(greenArrowAccuracyPriceText, 0);
                GreenArrowaccuracyUpgrade.interactable = false;
                return;
            }

            greenArrowAccuracyCurrentPrice = NextPrice(greenArrowAccuracyCurrentPrice);
            UpdatePriceText(greenArrowAccuracyPriceText, greenArrowAccuracyCurrentPrice);
        }
        else
        {
            Debug.Log("No enaught crystal.");
        }
    }
#endregion

#region Red Arrow Speed
    public void RedArrowShotSpeed()
    {
        if(GameManager.Instance.crystal >= redArrowSpeedCurrentPrice)
        {
            GameManager.Instance.crystal -= redArrowSpeedCurrentPrice;
            GameManager.Instance.redShotTime -= 0.1f;

            if(GameManager.Instance.redShotTime <= 0.15f)
            {
                UpdatePriceText(redArrowSpeedPriceText, 0);
                RedArrowSpeedUpgrade.interactable = false;
                return;
            }

            redArrowSpeedCurrentPrice = NextPrice(redArrowSpeedCurrentPrice);
            UpdatePriceText(redArrowSpeedPriceText,  redArrowSpeedCurrentPrice);
        }
        else
        {
            Debug.Log("No enaught crystal.");
        }
    }
#endregion

#region Red Arrow Accuracy
    public void RedArrowAccuracy()
    {
        if(GameManager.Instance.crystal >= redArrowAccuracyCurrentPrice)
        {
            GameManager.Instance.crystal -= redArrowAccuracyCurrentPrice;
            GameManager.Instance.redHitChance += 5f;

            if(GameManager.Instance.redHitChance >= 100f)
            {
                UpdatePriceText(redArrowAccuracyPriceText, 0);
                RedArrowaccuracyUpgrade.interactable = false;
                return;
            }

            redArrowAccuracyCurrentPrice =  NextPrice(redArrowAccuracyCurrentPrice);
            UpdatePriceText(redArrowAccuracyPriceText, redArrowAccuracyCurrentPrice);
        }
        else
        {
            Debug.Log("No enaught crystal.");
        }
    }
#endregion

#region Second Target
    public void SecondTarget()
    {
        if(GameManager.Instance.crystal >= 25)
        {
            GameManager.Instance.crystal -=25;
            GameManager.Instance.isSecundTarget = true;

            SecondTargetButtonUpgrade.interactable = false;

            AutoTargetButtonUpgrade.gameObject.SetActive(true);
        }
    }
#endregion
    public void TargetHealth()
    {
        if(GameManager.Instance.crystal >= healthCurrentPrice)
        {
            GameManager.Instance.crystal -= healthCurrentPrice;
            GameManager.Instance.maxHealth += 100;
            if(GameManager.Instance.maxHealth > 1000)
            {
                UpdatePriceText(healthTargetPriceText, 0);
                TargetHealtButtonUpgrade.interactable = false;
                return;
            }

            //healthCurrentPrice *= 2;
            healthCurrentPrice = NextFibNumber(healthCurrentPrice) * 2;
            UpdatePriceText(healthTargetPriceText, healthCurrentPrice);
        }
        else
        {
            Debug.Log("No enaught crystal.");
        }
    }

    public void AutoTarget()
    {
        if(GameManager.Instance.crystal >= 100)
        {
            GameManager.Instance.crystal -= 100;
            GameManager.Instance.autoTarget = true;

            AutoTargetButtonUpgrade.interactable = false;
            AutoTargetRespawnSpeed.gameObject.SetActive(true);
            GameManager.Instance.respawnTime = 5.5f;
        }
    }

    public void AutoTargetRespawnTime()
    {
        if(GameManager.Instance.crystal >= autoTargetRespawnPrice)
        {
            GameManager.Instance.crystal -= autoTargetRespawnPrice;
            GameManager.Instance.respawnTime -= 1f;
            if(GameManager.Instance.respawnTime <= 0.55f)
            {
                UpdatePriceText(autoTargetRespawnText, 0);
                AutoTargetRespawnSpeed.interactable = false;
                return;
            }
            autoTargetRespawnPrice = autoTargetRespawnPrice + 200; //NextFibNumber(autoTargetRespawnPrice) * 2;
            UpdatePriceText(autoTargetRespawnText, autoTargetRespawnPrice);
        }
    }

    public void DoubleTarget()
    {
        if(GameManager.Instance.crystal >= 100)
        {
        GameManager.Instance.crystal -= 100;
        GameManager.Instance.shootBoth = true;

        DoubleTargetButtonUpgrade.interactable = false;

        GreenArrowButonUpgrade.gameObject.SetActive(true);
        RedArrowButtonUpgrade.gameObject.SetActive(true);
        }
    }   

    public void GreenArrow()
    {
        if(GameManager.Instance.crystal >= 350 && GameManager.Instance.shootBoth)
        {
            GameManager.Instance.isGreen = true;
            GameManager.Instance.crystal -= 350;

            GreenArrowButonUpgrade.interactable = false;

            GreenArrowSpeedUpgrade.gameObject.SetActive(true);
            GreenArrowaccuracyUpgrade.gameObject.SetActive(true);
            

        }
    }

    public void RedArrow()
    {
        if(GameManager.Instance.crystal >= 500 && GameManager.Instance.shootBoth)
        {
            GameManager.Instance.isRed = true;
            GameManager.Instance.crystal -= 500;

            RedArrowButtonUpgrade.interactable = false;

            RedArrowSpeedUpgrade.gameObject.SetActive(true);
            RedArrowaccuracyUpgrade.gameObject.SetActive(true);

        }
    }


    void UpdatePriceText(TextMeshProUGUI priceText, long currentPrice)
    {
        //priceText.text = currentPrice.ToString();
        priceText.text = FormatNumber(currentPrice);
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


    private long NextFibNumber(long number)
    {
        long a = 0, b = 1;
        while (b <= number)
        {
            long temp = a+b;
            a = b;
            b = temp;
        }
        return b;
    }

    private long NextPrice(long number)
    {
        long first = 1, second = 100, third = 1000;

        if(number <= 10)
        {
            return number += first;
        }
        else if(number > 10 && number <= 500)
        {
            return number += second;
        }
        else
        {
            return number += third;
        }
        
    }
        
}
