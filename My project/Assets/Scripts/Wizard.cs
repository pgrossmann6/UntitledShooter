using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Wizard : MonoBehaviour
{
    [Header("Wizard")]
    public int WizardMagicPower;
    public int WizardSpeed;
    public int WizardHP;
    public static int SpellSpeed = 10;
    public static int SpellSize = 1;

    public static int ZombiePower = 2;
    public static int ZombieSpeed = 6;
    public static int ZombieHealth = 10;
    public static int ZombieDecay = 1;

    private CharacterStats _stats;


    [Space(10)]
    [Header("Wizard")]
    [SerializeField] private Vector3Int WizDamageLv;  // X= actual Lv , Y= Max Lv  , Z = Price
    [SerializeField] private Vector3Int WizSpeedLv;
    [SerializeField] private Vector3Int WizHpLv;

    public string W_Dmg_description, W_Speed_description, W_HP_Description;
    [SerializeField] private TextMeshProUGUI Wz_Dmg, Wz_Spd, Wz_HP;
    [Space(10)]

    [Header("Spell")]
    [SerializeField] private Vector3Int SpellSpeedLv;
    [SerializeField] private Vector3Int SpellSizeLv;

    public bool isPiercing = false;
    public int pirecing_price;
    public string S_Speed_description, S_size_description, S_P_description;
    [SerializeField] private TextMeshProUGUI Sp_Speed, Sp_Size, Sp_Piercing;

    [Space(10)]

    [Header("Zombie")]
    [SerializeField] private Vector3Int ZombieDamageLv;
    [SerializeField] private Vector3Int ZombieSpeedLv;
    [SerializeField] private Vector3Int ZombieHpLv;    
    [SerializeField] private Vector3Int ZombieDecayLv;
    public string Z_Dmg_description, Z_Spd_description, Z_Hp_description, Z_Decay_description;
    [SerializeField] private TextMeshProUGUI Zb_Dmg,Zb_Spd,Zb_HP,Zb_Dec;

    [Space(10)]

    [Header("WizardLeveling")]
    private int experiencePoints = 0;
    private int[] xpTolevelUp = {3, 3, 4, 4, 5, 6, 7, 10, 13, 15, 17};
    [SerializeField] private int level = 0;
    public Slider XP_slider;
    [Space(10)]

    [Header("Upgrade Window")]
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI skillPointsDisplay;
    private string skillSelected;
    public int skillCost;
    public int skillpoints;

    [SerializeField] private GameObject BuyButton;
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            BuyButton.SetActive(false);
            description.text = "";
        }
    }


    private void Start()
    {
        SpellSpeed = 10;
        SpellSize = 1;
        ZombiePower = 2;
        ZombieSpeed = 6;
        ZombieHealth = 10;
        ZombieDecay = 1;

        //Debug.Log($"{WizDamageLv}");
        XP_slider.value = 0;
        XP_slider.maxValue = (level < xpTolevelUp.Length-1)? xpTolevelUp[level]: 20;
        _stats = GetComponent<CharacterStats>();
        _stats.SetSpeed(WizardSpeed);
        _stats.power = WizardMagicPower;
        _stats.SetMaxHealth(WizardHP);
        description.text = "";
        BuyButton.SetActive(false);

        Wz_Dmg.text     = $"{WizDamageLv[0]}/{WizDamageLv[1]}";
        Wz_Spd.text     = $"{WizSpeedLv[0]}/{WizSpeedLv[1]}";
        Wz_HP.text      = $"{WizHpLv[0]}/{WizHpLv[1]}";
        Sp_Speed.text   = $"{SpellSpeedLv[0]}/{SpellSpeedLv[1]}";
        Sp_Size.text    = $"{SpellSizeLv[0]}/{SpellSizeLv[1]}";
        Sp_Piercing.text = "0/1";
        Zb_Dmg.text     = $"{ZombieDamageLv[0]}/{ZombieDamageLv[1]}";
        Zb_Spd.text     = $"{ZombieSpeedLv[0]}/{ZombieSpeedLv[1]}";
        Zb_HP.text      = $"{ZombieHpLv[0]}/{ZombieHpLv[1]}";
        Zb_Dec.text     = $"{ZombieDecayLv[0]}/{ZombieDecayLv[1]}";

        EnemyAI.OnDeath += AddXp;
    }

    private void OnDisable()
    {
        EnemyAI.OnDeath -= AddXp;

    }
    
    private void AddXp(int xp)
    {
        experiencePoints += xp;
        XP_slider.value = experiencePoints;
        if(XP_slider.value >= XP_slider.maxValue)
        
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        skillpoints++;
        skillPointsDisplay.text = $"{skillpoints} SkillPoints";
        experiencePoints -= (int)XP_slider.maxValue;
        XP_slider.value = experiencePoints;
        XP_slider.maxValue = (level < xpTolevelUp.Length)? xpTolevelUp[level]: 20;
        //Debug.Log()

        if(XP_slider.value >= XP_slider.maxValue)
        {
            LevelUp();
        }


    }

    private void BuySkill(string skill)
    {
        switch(skill)
        {
            case "WizardDamage": 
                WizDamageLv[0]++;
                if (WizDamageLv[0] == WizDamageLv[1])
                {
                    WizDamageLv[2] = 9999;
                    cost.text = "Skill Maxed";
                }
                else
                {
                    WizDamageLv[2] += 1 ;
                    cost.text = $"SkillPoints Required: {WizDamageLv[2]}";
                }
                skillCost = WizDamageLv[2];

                Wz_Dmg.text = $"{WizDamageLv[0]}/{WizDamageLv[1]}";
                WizardMagicPower = 1 + WizDamageLv[0];
                _stats.SetPower(WizardMagicPower);
                break;

            case "WizardSpeed": 
                WizSpeedLv[0]++;
                if (WizSpeedLv[0] == WizSpeedLv[1])
                {
                    WizSpeedLv[2] = 9999;
                    cost.text = "Skill Maxed";
                }
                else
                {
                    WizSpeedLv[2] += 1 ;
                    cost.text = $"SkillPoints Required: {WizSpeedLv[2]}";
                }
                skillCost = WizSpeedLv[2];

                Wz_Spd.text = $"{WizSpeedLv[0]}/{WizSpeedLv[1]}";
                WizardSpeed = 8 + WizSpeedLv[0];
                _stats.SetSpeed(WizardSpeed);
                break;

            case "WizardHealth": 
                WizHpLv[0]++;
                if (WizHpLv[0] == WizHpLv[1])
                {
                    WizHpLv[2] = 9999;
                    cost.text = "Skill Maxed";
                }
                else
                {
                    WizHpLv[2] += 1 ;
                    cost.text = $"SkillPoints Required: {WizHpLv[2]}";
                }
                skillCost = WizHpLv[2];

                Wz_HP.text = $"{WizHpLv[0]}/{WizHpLv[1]}";
                WizardHP = 5 + (WizHpLv[0] * 3);
                _stats.SetMaxHealth(WizardHP);
                break;

            case "SpellSpeed": 
                SpellSpeedLv[0]++;
                if (SpellSpeedLv[0] == SpellSpeedLv[1])
                {
                    SpellSpeedLv[2] = 9999;
                    cost.text = "Skill Maxed";
                }
                else
                {
                    SpellSpeedLv[2] += 1 ;
                    cost.text = $"SkillPoints Required: {SpellSpeedLv[2]}";
                }
                skillCost = SpellSpeedLv[2];

                Sp_Speed.text = $"{SpellSpeedLv[0]}/{SpellSpeedLv[1]}";
                SpellSpeed = (1+SpellSpeedLv[0]) * 10;
                break;
            case "SpellSize": 
                SpellSizeLv[0]++;
                if (SpellSizeLv[0] == SpellSizeLv[1])
                {
                    SpellSizeLv[2] = 9999;
                    cost.text = "Skill Maxed";
                }
                else
                {
                    SpellSizeLv[2] += 1 ;
                    cost.text = $"SkillPoints Required: {SpellSizeLv[2]}";
                }
                skillCost = SpellSizeLv[2];

                Sp_Size.text = $"{SpellSizeLv[0]}/{SpellSizeLv[1]}";
                SpellSize = 1 + SpellSizeLv[0];
                break;
            case "PiercingSpell": 
                isPiercing = true;
                if (isPiercing)
                {
                    pirecing_price = 9999;
                    Sp_Piercing.text = "1/1";
                    skillCost = pirecing_price;

                }
                break;
            case "ZombieDamage": 
                ZombieDamageLv[0]++;
                if (ZombieDamageLv[0] == ZombieDamageLv[1])
                {
                    ZombieDamageLv[2] = 9999;
                    cost.text = "Skill Maxed";
                }
                else
                {
                    ZombieDamageLv[2] += 1 ;
                    cost.text = $"SkillPoints Required: {ZombieDamageLv[2]}";
                }
                skillCost = ZombieDamageLv[2];

                Zb_Dmg.text = $"{ZombieDamageLv[0]}/{ZombieDamageLv[1]}";
                ZombiePower = 2 + ZombieDamageLv[0];
                break;
            case "ZombieSpeed": 
                ZombieSpeedLv[0]++;
                if (ZombieSpeedLv[0] == ZombieSpeedLv[1])
                {
                    ZombieSpeedLv[2] = 9999;
                    cost.text = "Skill Maxed";
                }
                else
                {
                    ZombieSpeedLv[2] += 1 ;
                    cost.text = $"SkillPoints Required: {ZombieSpeedLv[2]}";
                }
                skillCost = ZombieSpeedLv[2];

                Zb_Spd.text = $"{ZombieSpeedLv[0]}/{ZombieSpeedLv[1]}";
                ZombieSpeed = 6 + (ZombieSpeedLv[0] * 2);
                break;
            case "ZombieHealth": 
                ZombieHpLv[0]++;
                if (ZombieHpLv[0] == ZombieHpLv[1])
                {
                    ZombieHpLv[2] = 9999;
                    cost.text = "Skill Maxed";
                }
                else
                {
                    ZombieHpLv[2] += 1 ;
                    cost.text = $"SkillPoints Required: {ZombieHpLv[2]}";
                }
                skillCost = ZombieHpLv[2];

                Zb_HP.text = $"{ZombieHpLv[0]}/{ZombieHpLv[1]}";
                ZombieHealth = 10 + (ZombieHpLv[0] * 3);
                break;
            case "ZombieDecay": 
                ZombieDecayLv[0]++;
                if (ZombieDecayLv[0] == ZombieDecayLv[1])
                {
                    ZombieDecayLv[2] = 9999;
                    cost.text = "Skill Maxed";
                }
                else
                {
                    ZombieDecayLv[2] += 1 ;
                    cost.text = $"SkillPoints Required: {ZombieDecayLv[2]}";
                }
                skillCost = ZombieDecayLv[2];

                Zb_Dec.text = $"{ZombieDecayLv[0]}/{ZombieDecayLv[1]}";
                ZombieDecay = 1 + ZombieDecayLv[0];
                break;
            
            
        }
        cost.text = $"SkillPoints Required: {skillCost}";
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
    }

    public void ButtomWizDamage()
    {
        skillSelected = "WizardDamage";
        description.text = W_Dmg_description;
        skillCost = WizDamageLv[2];
        cost.text = $"SkillPoints Required: {WizDamageLv[2]}";
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
        BuyButton.SetActive(true);

        
    }

    public void BuySkill()
    {
        if (skillpoints >= skillCost)
        {
            skillpoints -= skillCost;
            BuySkill(skillSelected);
            if (skillpoints == 0)
            {
                skillPointsDisplay.text = " ";
            }
            else
            {
                skillPointsDisplay.text = $"{skillpoints} SkillPoints";
            }
        }
    }

    public void ButtomWizSpeed()
    {
        skillSelected = "WizardSpeed";
        description.text = W_Speed_description;
        skillCost = WizSpeedLv[2];
        cost.text = $"SkillPoints Required: {WizSpeedLv[2]}";
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
        BuyButton.SetActive(true);

    }
    public void ButtomWizHp()
    {
        skillSelected = "WizardHealth";
        description.text = W_HP_Description;
        skillCost = WizHpLv[2];
        cost.text = $"SkillPoints Required: {skillCost}";
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
        BuyButton.SetActive(true);

    }

    public void ButtomSpellSpeed()
    {
        skillSelected = "SpellSpeed";
        description.text = S_Speed_description;
        skillCost = SpellSpeedLv[2];
        
        cost.text = $"SkillPoints Required: {skillCost}";
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
        BuyButton.SetActive(true);

    }
    public void ButtomSpellSize()
    {
        skillSelected = "SpellSize";
        description.text = S_size_description;
        skillCost = SpellSizeLv[2];
        
        cost.text = $"SkillPoints Required: {skillCost}";
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
        BuyButton.SetActive(true);

    }
    public void ButtomSpellPiercing()
    {
        skillSelected = "PiercingSpell";
        description.text = S_P_description;
        skillCost = pirecing_price;
        
        cost.text = $"SkillPoints Required: {skillCost}";
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
        BuyButton.SetActive(true);
    }
    public void ButtomZombieDamage(){
        skillSelected = "ZombieDamage";
        description.text = Z_Dmg_description;
        skillCost = ZombieDamageLv[2];
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
        cost.text = $"SkillPoints Required: {skillCost}";
        BuyButton.SetActive(true);
    }
    public void ButtomZombieSpeed(){
        skillSelected = "ZombieSpeed";
        description.text = Z_Spd_description;
        skillCost = ZombieSpeedLv[2];
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
        cost.text = $"SkillPoints Required: {skillCost}";
        BuyButton.SetActive(true);
    }
    public void ButtomZombieHealth(){
        skillSelected = "ZombieHealth";
        description.text = Z_Hp_description;
        skillCost = ZombieHpLv[2];
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
        cost.text = $"SkillPoints Required: {skillCost}";
        BuyButton.SetActive(true);
    }
    public void ButtomZombieDecay(){
        skillSelected = "ZombieDecay";
        description.text = Z_Decay_description;
        skillCost = ZombieDecayLv[2];
        if (skillCost == 9999)
        {
            cost.text = "Skill Maxed";

        }
        cost.text = $"SkillPoints Required: {skillCost}";
        BuyButton.SetActive(true);
    }

}
