using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject upgradeUI; // UI that contains the upgrades
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private BaseAbility assignedAbility; // Store the ability object
    private GeneralUpgradeSO assignedUpgrade; // Store the upgrade object if relevant

    private bool skillApplied = false;

    // Function to set the card's details and assign the ability
    public void SetCardDetails(BaseAbility ability)
    {
        assignedAbility = ability; // Save the ability object
        assignedUpgrade = null;    // Clear any assigned upgrade

        // Set the text for the UI elements
        if (titleText != null && descriptionText != null)
        {
            titleText.text = ability.abilityName;
            descriptionText.text = ability.abilityDescription;
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
        if (titleText != null && descriptionText != null)
        {
            titleText.text = "Upgrade";
            descriptionText.text = $"Bonus Health: {upgrade.healthBonus} \nBonus Speed: {upgrade.speedBonus} \nBonus Damage: {upgrade.damageBonus}";
        }
        else
        {
            Debug.LogError("Text fields are not assigned in the inspector!");
        }
    }

    // Function to clear the card's details
    public void ClearCardDetails()
    {
        assignedAbility = null;
        assignedUpgrade = null;

        if (titleText != null && descriptionText != null)
        {
            titleText.text = "";
            descriptionText.text = "";
        }
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

            
        }
    }
   
    // Function to apply the selected skill or upgrade
    private void ApplySelectedSkillOrUpgrade()
    {
        // Apply assigned ability
        if (assignedAbility != null)
        {
            Debug.Log("Selected Ability: " + assignedAbility.abilityName);
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
