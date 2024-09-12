using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Card : MonoBehaviour
{
    [SerializeField] GameObject upgradeUI; // UI that contains the upgrades
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    BaseAbility assignedAbility; // Store the skill object here

    bool skillApplied = false;
    // Function to set the card's details and assign the ability
    public void SetCardDetails(BaseAbility ability)
    {
        assignedAbility = ability; // Save the ability object

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

    // Overload for GeneralUpgradeSO (if you're also setting upgrades this way)
    public void SetCardDetails(GeneralUpgradeSO upgrade)
    {
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
        if (titleText != null && descriptionText != null)
        {
            titleText.text = "";
            descriptionText.text = "";
        }
    }

    // Function to handle card selection (called when the button is pressed)
    public void SelectedCard()
    {
        // Check if the skill has already been applied
        if (!skillApplied && upgradeUI != null && upgradeUI.activeSelf)
        {
            // Hide the upgrade UI and apply the skill
            upgradeUI.SetActive(false);
            ApplySelectedSkill();
            skillApplied = true; // Set the flag to true
        }
    }

    // Function to apply the selected skill
    private void ApplySelectedSkill()
    {
        // Check if the ability is assigned
        if (assignedAbility != null)
        {
            Debug.Log("Selected Ability: " + assignedAbility.abilityName);

            // Notify UpgradeManager or another system that the skill was selected
            UpgradeManager.Instance.ApplySelectedSkill(assignedAbility);
        }
        else
        {
            Debug.LogWarning("No ability assigned to this card.");
        }
    }
}
