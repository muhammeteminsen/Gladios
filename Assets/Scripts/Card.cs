
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    public WaweManager waweManager;
    public GameObject upgradeUI; // UI that contains the upgrades
    public Image abilityUýImage;

    private BaseAbility assignedAbility; // Store the ability object
    private GeneralUpgradeSO assignedUpgrade; // Store the upgrade object if relevant

    private bool skillApplied = false;

    // Function to set the card's details and assign the ability
    public void SetCardDetails(BaseAbility ability)
    {
        assignedAbility = ability; // Save the ability object
        assignedUpgrade = null;    // Clear any assigned upgrade

        // Set the text for the UI elements
        if (abilityUýImage != null)
        {
            abilityUýImage.sprite = ability.abilitySprite;
        }
        else
        {
            Debug.LogError("Text fields are not assigned in the inspector!");
        }
       
    }
    
    // Overload for GeneralUpgradeSO
    public void SetCardDetails(GeneralUpgradeSO upgrade)
    {
        assignedUpgrade = upgrade; // Save the upgrade object
        assignedAbility = null;    // Clear any assigned ability

        // Set text for general upgrades
        abilityUýImage.sprite = assignedUpgrade.selectedSprite;
        
    }

    // Function to clear the card's details
    public void ClearCardDetails()
    {
        abilityUýImage.sprite = null;

        
    }

    public void SelectedCard()
    {
        if (skillApplied)
        {
            Debug.LogWarning("Skill already applied, ignoring further clicks.");
            return;
        }

        if (upgradeUI != null)
        {
            upgradeUI.SetActive(false);
            skillApplied = true;
            ApplySelectedSkillOrUpgrade();

            waweManager.waweCanStart = true;
        }

    }
   
    // Function to apply the selected skill or upgrade
    private void ApplySelectedSkillOrUpgrade()
    {
        // Apply assigned ability
        if (assignedAbility != null)
        {
            
            // Notify UpgradeManager that the skill was selected
            UpgradeManager.Instance.ApplySelected(assignedAbility);
        }
        // Apply assigned upgrade
        else if (assignedUpgrade != null)
        {
            Debug.Log("Selected Upgrade: Bonus Health: " + assignedUpgrade.healthBonus +
                      ", Speed: " + assignedUpgrade.speedBonus +
                      ", Damage: " + assignedUpgrade.damageBonus);
            // Notify UpgradeManager that the upgrade was selected
            UpgradeManager.Instance.ApplySelected(null, assignedUpgrade);
        }
        else
        {
            Debug.LogWarning("No ability or upgrade assigned to this card.");
        }
    }
}
