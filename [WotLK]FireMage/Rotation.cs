using System;
using System.Threading;
using wShadow.Templates;
using wShadow.Warcraft.Classes;
using wShadow.Warcraft.Defines;
using wShadow.Warcraft.Managers;



public class FireMageWotlk: Rotation
{
	
 private int debugInterval = 5; // Set the debug interval in seconds
    private DateTime lastDebugTime = DateTime.MinValue;
	
    public override void Initialize()
    {  
	// Can set min/max levels required for this rotation.
        
		 lastDebugTime = DateTime.Now;
        LogPlayerStats();
        // Use this method to set your tick speeds.
        // The simplest calculation for optimal ticks (to avoid key spam and false attempts)

		// Assuming wShadow is an instance of some class containing UnitRatings property
        SlowTick = 800;
        FastTick = 200;

        // You can also use this method to add to various action lists.

        // This will add an action to the internal passive tick.
        // bool: needTarget -> If true action will not fire if player does not have a target
        // Func<bool>: function -> Action to attempt, must return true or false.
        PassiveActions.Add((true, () => false));

        // This will add an action to the internal combat tick.
        // bool: needTarget -> If true action will not fire if player does not have a target
        // Func<bool>: function -> Action to attempt, must return true or false.
        CombatActions.Add((true, () => false));
		
		
		
    }
	public override bool PassivePulse()
	{
	 // Variables for player and target instances
var me = Api.Player;
var target = Api.Target;
var pet = me.Pet();

if ((DateTime.Now - lastDebugTime).TotalSeconds >= debugInterval)
        {
            LogPlayerStats();
            lastDebugTime = DateTime.Now; // Update lastDebugTime
        }
// Health percentage of the player
var healthPercentage = me.HealthPercent;
var mana = me.ManaPercent;
var targetDistance = target.Position.Distance2D(me.Position);
ShadowApi shadowApi = new ShadowApi();

if (me.IsDead() || me.IsGhost() || me.IsCasting() || me.IsMoving() || me.IsChanneling() ) return false;
        if (me.HasAura("Drink") || me.HasAura("Food")) return false;
		

	string[] waterTypes = { "Conjured Mana Strudel","Conjured Mountain Spring Water","Conjured Crystal Water","Conjured Sparkling Water","Conjured Mineral Water","Conjured Spring Water","Conjured Purified Water","Conjured Fresh Water", "Conjured Water" };
string[] foodTypes = { "Conjured Mana Strudel","Conjured Cinnamon Roll","Conjured Sweet Roll","Conjured Sourdough","Conjured Pumpernickel","Conjured Rye","Conjured Bread","Conjured Muffin" };

bool needsWater = true;
bool needsFood = true;
foreach (string waterType in waterTypes)
{
    if (shadowApi.Inventory.HasItem(waterType))
    {
        needsWater = false;
        break;
    }
}

foreach (string foodType in foodTypes)
{
    if (shadowApi.Inventory.HasItem(foodType))
    {
        needsFood = false;
        break;
    }
}


		
		if (Api.Spellbook.CanCast("Conjure Refreshment") && (needsWater || needsFood))
    {
        if (Api.Spellbook.Cast("Conjure Refreshment"))
        {
            Console.WriteLine("Conjured Refreshment.");
            // Add further actions if needed after conjuring water
        }
 }
		if (Api.Spellbook.CanCast("Mage Armor")  && !me.HasAura("Mage Armor"))
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Mage Armor");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Mage Armor"))
        return true;
	}
		if (Api.Spellbook.CanCast("Frost Armor")  && !me.HasAura("Frost Armor") && !me.HasAura("Mage Armor"))
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Frost Armor");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Frost Armor"))
        return true;
	}
		if (Api.Spellbook.CanCast("Amplify Magic")  && (!me.HasAura(43017) ||  !me.HasPermanent(43017)) 
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Amplify Magic");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Amplify Magic"))
        return true;
	}	
	if (Api.Spellbook.CanCast("Arcane Intellect")  && !me.HasPermanent("Arcane Intellect"))
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Arcane Intellect");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Arcane Intellect"))
        return true;
	}
	

	


// Now needsWater variable will indicate if the character needs water
if (needsWater)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Character needs water!");
    Console.ResetColor();

    // Add logic here to conjure water or perform any action needed to acquire water
    // Example: Cast "Conjure Water" spell
    // Assuming the API allows for conjuring water in a similar way to casting spells
    if (Api.Spellbook.CanCast("Conjure Water"))
    {
        if (Api.Spellbook.Cast("Conjure Water"))
        {
            Console.WriteLine("Conjured water.");
            // Add further actions if needed after conjuring water
        }
 }
}

// Now needsWater variable will indicate if the character needs food
if (needsFood)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Character needs food!");
    Console.ResetColor();

    // Add logic here to conjure water or perform any action needed to acquire food
    // Example: Cast "Conjure food" spell
    // Assuming the API allows for conjuring food in a similar way to casting spells
    if (Api.Spellbook.CanCast("Conjure Food"))
    {
        if (Api.Spellbook.Cast("Conjure Food"))
        {
            Console.WriteLine("Conjured Food.");
            // Add further actions if needed after conjuring water
        }

}
}

	
	
	
	if (!target.IsDead())

if (Api.Spellbook.CanCast("Fireball")  &&  mana > 20)
  
    {
        var reaction = me.GetReaction(target);
        
        if (reaction != UnitReaction.Friendly)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Casting Fireball");
            Console.ResetColor();
            
            if (Api.Spellbook.Cast("Fireball"))
            {
                return true;
            }
        }
        else
        {
            // Handle if the target is friendly
            Console.WriteLine("Target is friendly. Skipping Fireball cast.");
        }
    }
    else
    {
        // Handle if the target is not valid
        Console.WriteLine("Invalid target. Skipping Fireball cast.");
    }
				return base.PassivePulse();

		}
		
public override bool CombatPulse()
    {
	// Variables for player and target instances
var me = Api.Player;
var target = Api.Target;
 if ((DateTime.Now - lastDebugTime).TotalSeconds >= debugInterval)
        {
            LogPlayerStats();
            lastDebugTime = DateTime.Now; // Update lastDebugTime
        }
// Health percentage of the player
var healthPercentage = me.HealthPercent;
var targethealth = target.HealthPercent;

// Power percentages for different resources
		var mana = me.ManaPercent;

// Target distance from the player
	var targetDistance = target.Position.Distance2D(me.Position);

		if (me.IsDead() || me.IsGhost() || me.IsCasting()  ) return false;
        if (me.HasAura("Drink") || me.HasAura("Food")) return false;
		
		
		
	if (Api.Player.InCombat() && Api.Target != null && Api.Target.IsValid())
		{
				if (Api.Spellbook.CanCast("Mana Shield") && !Api.Spellbook.OnCooldown("Mana Shield") && healthPercentage<=30)
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Mana Shield");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Mana Shield"))
        return true;
	}
	
	
	if (Api.Spellbook.CanCast("Mirror Image") && !Api.Spellbook.OnCooldown("Mirror Image") && mana>=10)
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Mirror Image");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Mirror Image"))
        return true;
	}

    // Single Target Abilities
    if (!target.IsDead())
    {
				if (Api.Spellbook.CanCast("Combustion") && !Api.Spellbook.OnCooldown("Combustion") && !me.HasPermanent(28682))
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Combustion");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Combustion"))
        return true;
	}
		if (Api.Spellbook.CanCast("Living Bomb") && mana>=22 && !target.HasAura("Living Bomb") && targethealth>=30  && !Api.Spellbook.OnCooldown("Living Bomb"))
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Living Bomb");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Living Bomb"))
        return true;
	}
	
	if (me.HasAura("Hot Streak") && mana>=22 && !target.HasAura("Pyroblast") && targethealth>=20)
	{
		Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Pyroblast");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Pyroblast"))
        return true;
	}
		if (Api.Spellbook.CanCast("Fire Blast") && me.IsMoving()  && !Api.Spellbook.OnCooldown("Fire Blast"))
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Fire Blast");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Fire Blast"))
        return true;
	}
        if (Api.Spellbook.CanCast("Fireball") && mana>=19 && targethealth>20)
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Fireball");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Fireball"))
        return true;
	}
	
	if (Api.Spellbook.CanCast("Frostbolt")  && targethealth<20 && mana>20)
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Frostbolt");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Frostbolt"))
        return true;
	}
	
	if (Api.Spellbook.CanCast("Shoot")   )
	{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Casting Shoot");
    Console.ResetColor();

    if (Api.Spellbook.Cast("Shoot"))
        return true;
	}
    }
	
	}

// Check if in combat and if there are multiple targets nearby
if (me.InCombat() && Api.UnfriendlyUnitsNearby(10, true) >= 2)
{
    
    // Multi-Target Abilities
    
    if (!target.IsDead())
        {
            // Logic for multi-target abilities, e.g. AoE spells, debuffs, etc.
            // Example: if (me.CanCast("AoE_Spell") && target.Distance < 8)
            // {
            //     me.Cast("AoE_Spell");
            // }
        }
    

		
			
    	
	
    }

return base.CombatPulse();
}
private void LogPlayerStats()
    {
        // Variables for player and target instances
var me = Api.Player;
var target = Api.Target;
		var mana = me.ManaPercent;
ShadowApi shadowApi = new ShadowApi();

// Health percentage of the player
var healthPercentage = me.HealthPercent;


// Target distance from the player
		var targetDistance = target.Position.Distance2D(me.Position);
		

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{mana}% Mana available");
        Console.WriteLine($"{healthPercentage}% Health available");
		Console.ResetColor();


	if (me.HasAura("Frost Armor")) // Replace "Thorns" with the actual aura name
	{
		 Console.ForegroundColor = ConsoleColor.Blue;
Console.ResetColor();
    var remainingTimeSeconds = me.AuraRemains("Frost Armor");
    var remainingTimeMinutes = remainingTimeSeconds / 60; // Convert seconds to minutes
    var roundedMinutes = Math.Round(remainingTimeMinutes/ 1000,1); // Round to one decimal place

    Console.WriteLine($"Remaining time for Frost Armor: {roundedMinutes} minutes");
	Console.ResetColor();
	}
	


// Define food and water types
	string[] waterTypes = { "Conjured Mana Strudel","Conjured Mountain Spring Water","Conjured Crystal Water","Conjured Sparkling Water","Conjured Mineral Water","Conjured Spring Water","Conjured Purified Water","Conjured Fresh Water", "Conjured Water" };
string[] foodTypes = { "Conjured Mana Strudel","Conjured Cinnamon Roll","Conjured Sweet Roll","Conjured Sourdough","Conjured Pumpernickel","Conjured Rye","Conjured Bread","Conjured Muffin" };

// Count food items in the inventory
int foodCount = 0;
foreach (string foodType in foodTypes)
{
    int count = shadowApi.Inventory.ItemCount(foodType);
    foodCount += count;
}

// Count water items in the inventory
int waterCount = 0;
foreach (string waterType in waterTypes)
{
    int count = shadowApi.Inventory.ItemCount(waterType);
    waterCount += count;
}

// Display the counts of food and water items
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Current Food Count: " + foodCount);
Console.WriteLine("Current Water Count: " + waterCount);
Console.ResetColor();



Console.ResetColor();
    }
	}