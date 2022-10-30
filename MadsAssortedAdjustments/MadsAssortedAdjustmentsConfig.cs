using PhoenixPoint.Modding;
using System;
using System.Reflection;

namespace MadsAssortedAdjustments
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class MadsAssortedAdjustmentsConfig : ModConfig
    { 
    internal class Settings
    {
		/// Only public fields are serialized.
		/// Supported types for in-game UI are:

			[ConfigField(text: "Disables rock tiles in phoenix bases.",
			description: "Disables rock tiles in phoenix bases.")]
			public bool DisableRocksAtBases = true;

            [ConfigField(text: "Enable Unlock Items by Research mod",
            description: "Enables the use of the below functions")]
            public bool UnlockItemsByResearch = true;
            [ConfigField(text: "Unlocks Phoenix Elite Gear via Research Paths",
            description: "Unlock Phoenix Gold Gear Crafting")]
            public bool UnlockPhoenixEliteGear = true;
            [ConfigField(text: "Unlocks Promo Gear via Research Paths",
            description: "Unlock Promo Gear Crafting")]
            public bool UnlockPromoArmor = true;
            [ConfigField(text: "Unlocks Living Weapons via Research Paths",
            description: "Unlock Living Weapons Crafting")]
            public bool UnlockLivingWeapons = true;
            [ConfigField(text: "Unlocks Living Armor via Research Paths",
            description: "Unlock Living Armor Crafting")]
            public bool UnlockLivingArmor = true;
            [ConfigField(text: "Unlocks Independent Ammunition via Research Paths",
            description: "Unlock Independent Ammo Crafting")]
            public bool UnlockIndependentAmmunition = true;
            [ConfigField(text: "Unlocks Independent Weapons via Research Paths.",
            description: "Unlock Independent Weapon Crafting")]
            public bool UnlockIndependentWeapons = true;
            [ConfigField(text: "Unlocks Independent Armor via Research Paths",
            description: "Unlocks Independent Armor Crafting")]
            public bool UnlockIndependentArmor = true;
            [ConfigField(text: "Unlocks Hidden Items via Research Paths",
            description: "Unlock Hidden Items Crafting")]
            public bool UnlockHiddenItems = true;

            [ConfigField(text: "Empty Aircrafts/vehicles can be scrapped",
            description: "If an aircraft is completely empty, you can scrap it from the roster list.")]
            public bool EnableScrapAircraft = true;

            [ConfigField(text: "Fully leveled soldiers gain SP thru XP",
            description: "Fully leveled soldiers will convert some experience to skill points. Base rate is dependent on difficulty setting, somewhere between 1 and 2 percent.")]
            public bool EnableExperienceToSkillpointConversion = true;
            [ConfigField(text: "Add converted SP to Soldier Pool",
                description: "Will add the converted skill points to the soldier's pool.")]
            public bool XPtoSPAddToPersonalPool = false;
            [ConfigField(text: "Add converted SP to Faction's Pool",
                description: "Will add the converted skill points to the faction's pool.")]
            public bool XPtoSPAddToFactionPool = true;
            [ConfigField(text: "XP to SP multiplier",
                description:"Will multiply the converted skill points by its value.")]
            public float XPtoSPConversionMultiplier = 2f;
            internal float XPtoSPConversionRate = 0.01f; // Default is dependent on difficulty setting, this is just a fallback if the setting is unretrievable.

            [ConfigField(text:"Enable Facility Changes",
                description:"General switch to enable the related subfeatures")]
            public bool EnableFacilityAdjustments = true;
            // Healing
            [ConfigField(text: "Heal Rate for Med Bays",
                description:"Healing rate for medical bays, vanilla default is 4")]
            public float MedicalBayBaseHeal = 8f;
            [ConfigField(text: "Stamina regen rate for Living Quarters",
                description:"Stamina regeneration rate for living quarters, vanilla default is 2")]
            public float LivingQuartersBaseStaminaHeal = 4f;
            [ConfigField(text: "Healing rate for aircraft at vehicle bays",
               description: "Healing rate for aircraft at vehicle bays, vanilla default is 48")]
            public int VehicleBayAircraftHealAmount = 60;
            [ConfigField(text: "Healing rate for vehicles at vehicle bays",
                description:"Healing rate for vehicles at vehicle bays, vanilla default is 20")]
            public int VehicleBayVehicleHealAmount = 40;
            [ConfigField(text: "Healing rate for mutogs at mutation labs",
                description:"Healing rate for mutogs at mutation labs, vanilla default is 20")]
            public int MutationLabMutogHealAmount = 40;
            //Training
            [ConfigField(text: "XP gain per hour for soldiers via training facilities",
                description:"Experience gain rate per hour for soldiers at training facilities, vanilla default is 2")]
            public int TrainingFacilityBaseExperienceAmount = 2;
            [ConfigField(text: "Enable training facilities to add SP to faction pool",
                description:"Enables training facilities to add skillpoints to the faction pool. This is currently disabled in vanilla.")]
            public bool TrainingFacilitiesGenerateSkillpoints = true;
            [ConfigField(text: "Global SP gain per day per facility",
                description:"Global skillpoints gain rate per day per facility, vanilla default is 1. Needs the above flag set to true!")]
            public int TrainingFacilityBaseSkillPointsAmount = 1;
            // Resource Generators
            [ConfigField(text: "Production point generated at fabrication plants",
                description:"Production points generated at fabrication plants, vanilla default is 4")]
            public float FabricationPlantGenerateProductionAmount = 4f;
            [ConfigField(text: "Research points generated at research labs",
                description:"Research points generated at research labs, vanilla default is 4")]
            public float ResearchLabGenerateResearchAmount = 4f;
            [ConfigField(text: "Food generated by Food Production facilities",
                description:"Supplies (Food) generated at food production facilities, vanilla default is 0.33 (translates to 8 food/day).")]
            public float FoodProductionGenerateSuppliesAmount = 0.5f;
            [ConfigField(text: "Research points generated at bionic labs",
                description:"Research points generated at bionic labs, vanilla default is 4")]
            public float BionicsLabGenerateResearchAmount = 4f;
            [ConfigField(text: "Mutagens generated by mutation labs",
                description:"Mutagen points generated at mutation labs, vanilla default is 0.25 (translates to 6 mutagen/day).")]
            public float MutationLabGenerateMutagenAmount = 0.25f;
            // Add Tech & Materials to Facilities
            [ConfigField(text: "Fabrication plant generates materials",
                description:"Fabrication plant generate this amount of materials per day.")]
            public float FabricationPlantGenerateMaterialsAmount = 1f;
            [ConfigField(text: "Research lab generates tech",
                description:"Research labs generate this amount of tech per day.")]
            public float ResearchLabGenerateTechAmount = 1f;
            internal float GenerateResourcesBaseDivisor = 24f;

            [ConfigField(text: "Enable Economy Adjustments",
                description: "General switch to enable the related subfeatures.")]
            public bool EnableEconomyAdjustments = true;
            [ConfigField(text: "Resource/Manufacturing Multiplier",
                description:"General multiplier for manufacturing costs.")]
            public float ResourceMultiplier = 0.75f;
            [ConfigField(text: "Scrap Multiplier",
                description:"General multiplier for scrapping costs, vanilla default is 0.5")]
            public float ScrapMultiplier = 0.5f;
            [ConfigField(text: "Manufacturing time Multiplier",
                description:"General multiplier for manufacturing times.")]
            public float CostMultiplier = 0.5f;

            public string DebugDevKey { get; internal set; }
            public int DebugLevel { get; internal set; }
            public string BalancePresetId { get; internal set; }

            public bool Equals(Settings obj)
        {
            Type t = this.GetType();
            Type o = obj.GetType();

            if (t != o)
            {
                return false;
            }

            FieldInfo[] tFields = t.GetFields(BindingFlags.Instance | BindingFlags.Public);
            //FieldInfo[] oFields = o.GetFields(BindingFlags.Instance | BindingFlags.Public);


            foreach (FieldInfo fi in tFields)
            {
                if (fi.Name == "BalancePresetId" || fi.Name == "BalancePresetState" || fi.Name == "DebugDevKey" || fi.Name == "PresetStateHash")
                {
                    continue;
                }

                object tValue = fi.GetValue(this);
                object oValue = fi.GetValue(obj);
                Logger.Info($"[Settings_Equals] {fi.Name}: {tValue} <-> {oValue}");

                if (!tValue.Equals(oValue))
                {
                    return false;
                }
            }

            return true;
        }



        public override string ToString()
        {
            string result = "";
            Type t = this.GetType();
            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (FieldInfo fi in fields)
            {
                result += "\n";
                result += fi.Name;
                result += ": ";
                result += fi.GetValue(this).ToString();
            }
            return result;
        }



        public void ToHtmlFile(string destination)
        {
            string result = "<!doctype html><html lang=en><head><meta charset=utf-8><title>Assorted Adjustments: Settings</title><style>html {font-family: sans-serif;} body {padding:2em;} h1 {padding-left: 10px;} th {font-size:1.4em;}</style></head><body>\n";
            result += "<h1>SETTINGS</h1>\n";

            result += "<table cellpadding=0 cellspacing=10>\n";
            result += $"<tr><th align=left>Name</th><th align=left>Value</th><th align=left>Description</th><th align=right>Default</th></tr>\n";

            Type t = this.GetType();
            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public);

            foreach (FieldInfo fi in fields)
            {
                Annotation annotation = Attribute.IsDefined(fi, typeof(Annotation)) ? (Annotation)Attribute.GetCustomAttribute(fi, typeof(Annotation)) : null;

                string settingName = fi.Name;
                string setValue = fi.GetValue(this).ToString();
                string settingDesc = annotation?.Description;
                string defaultValue = annotation?.DefaultValue;

                if (annotation?.DefaultValue != null && setValue != defaultValue)
                {
                    setValue = $"<b>{setValue}</b>";
                }

                if (annotation != null && annotation.StartSection)
                {
                    result += $"<tr><td colspan=4><br><b>{annotation.SectionLabel}</b></td></tr>\n";
                }

                result += "<tr><td>";
                result += $" {settingName} ";
                result += "</td><td>";
                result += $" {setValue} ";
                result += "</td><td>";
                result += $" {settingDesc} ";
                result += "</td><td align=right>";
                result += $" <i>{defaultValue}</i> ";
                result += "</td></tr>\n";
            }
            result += "</table></body></html>";

            System.IO.File.WriteAllText(destination, result);
        }



        public void ToMarkdownFile(string destination)
        {
            string result = "";
            result += "# SETTINGS";
            result += "\n\n";

            result += $"|Name|Value|Description|Default|\n";
            result += $"|:---|:----|:----------|:-----:|\n";

            Type t = this.GetType();
            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public);

            foreach (FieldInfo fi in fields)
            {
                Annotation annotation = Attribute.IsDefined(fi, typeof(Annotation)) ? (Annotation)Attribute.GetCustomAttribute(fi, typeof(Annotation)) : null;

                string settingName = fi.Name;
                string setValue = fi.GetValue(this).ToString();
                string settingDesc = annotation?.Description;
                string defaultValue = annotation?.DefaultValue;

                if (annotation?.DefaultValue != null && setValue != defaultValue)
                {
                    setValue = $"<b>{setValue}</b>";
                }

                if (annotation != null && annotation.StartSection)
                {
                    result += $"| . | . | . | . |\n";
                    result += $"| . | . | . | . |\n";
                    result += $"| . | . | . | . |\n";
                    result += $"| <b>{annotation.SectionLabel}</b> | | | |\n";
                }

                result += "|";
                result += $" {settingName} ";
                result += "|";
                result += $" {setValue} ";
                result += "|";
                result += $" {settingDesc} ";
                result += "|";
                result += $" <i>{defaultValue}</i> ";
                result += "|\n";
            }

            System.IO.File.WriteAllText(destination, result);
        }
    }
}
		}


