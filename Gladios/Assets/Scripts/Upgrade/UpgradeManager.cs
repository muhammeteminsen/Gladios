using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using Unity.VisualScripting;

[Serializable]
public class WeaponSkills
{
   public List<BaseAbility> weaponAbilities;
}

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance; // Singleton instance for easy access

    [SerializeField] List<GeneralUpgradeSO> generalUpgrades;
    [SerializeField] List<WeaponSkills> weaponsSkils;
    [SerializeField] int weaponIndex;
    [SerializeField] Card cardObject1, cardObject2, cardObject3;
    [SerializeField] AnimationController animationController;

    private void Awake()
    {
        Instance = this; // Setup the singleton
    }

    // Get the active weapon index based on animationController
    public void GetWeaponIndex()
    {
        for (int i = 0; i < animationController.weapons.Length; i++)
        {
            if (animationController.weapons[i].activeSelf)
            {
                weaponIndex = i;
                print(animationController.weapons[i].name + " is Active");
            }
        }
    }

    // Function to get a random skill from the current weapon
    BaseAbility RandomSkillForWeapon()
    {
        WeaponSkills selectedSkills = weaponsSkils[weaponIndex];

        if (selectedSkills.weaponAbilities.Count == 0)
        {
            Debug.LogWarning("No abilities available for the selected weapon!");
            return null;
        }

        int skillIndex = UnityEngine.Random.Range(0, selectedSkills.weaponAbilities.Count);
        return selectedSkills.weaponAbilities[skillIndex];
    }

    // Function to get a random general upgrade
    GeneralUpgradeSO RandomGeneralUpgrade()
    {
        int index = UnityEngine.Random.Range(0, generalUpgrades.Count);
        return generalUpgrades[index];
    }

    // Function to assign random skills to the cards
    public void AssignCardSkills()
    {
        BaseAbility randomSkill1 = RandomSkillForWeapon();
        BaseAbility randomSkill2 = RandomSkillForWeapon();
        GeneralUpgradeSO randomUpgrade = RandomGeneralUpgrade();

        // Assign random abilities/upgrades to the cards
        cardObject1.SetCardDetails(randomSkill1);
        cardObject2.SetCardDetails(randomSkill2);
        cardObject3.SetCardDetails(randomUpgrade);
    }

    // Apply the selected skill (this is called from the Card class)
    public void ApplySelectedSkill(BaseAbility selectedAbility)
    {
        Debug.Log("Applying Ability: " + selectedAbility.abilityName);

        // Implement logic to apply the selected ability to the player or weapon
        // Example: Apply ability effects here
    }

    // Optionally reset the card display
    void ResetCardDisplay()
    {
        cardObject1.ClearCardDetails();
        cardObject2.ClearCardDetails();
        cardObject3.ClearCardDetails();
        // Alternatively, hide the cards if needed
    }
}
