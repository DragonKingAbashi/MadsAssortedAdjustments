﻿using System;
using System.Collections.Generic;
using System.Linq;
using Base.Core;
using Base.Defs;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Geoscape.Entities.PhoenixBases;
using PhoenixPoint.Geoscape.Entities.PhoenixBases.FacilityComponents;
using PhoenixPoint.Geoscape.Entities.Sites;
using PhoenixPoint.Geoscape.Levels;
using PhoenixPoint.Geoscape.Levels.Factions;
using PhoenixPoint.Geoscape.View.ViewControllers.PhoenixBase;

namespace MadsAssortedAdjustments
{
    internal static class FacilityAdjustments
    {
        internal static float currentHealFacilityHealOutput;
        internal static float currentHealFacilityStaminaHealOutput;
        internal static float currentVehicleSlotFacilityAircraftHealOuput;
        internal static float currentVehicleSlotFacilityVehicleHealOuput;
        internal static float currentHealFacilityMutogHealOutput;
        internal static float currentFoodProductionFacilitySuppliesOutput;
        internal static int currentExperienceFacilityExperienceOutput;



        public static void Apply()
        {
            HarmonyInstance harmony = HarmonyInstance.Create(typeof(EconomyAdjustments).Namespace);
            DefRepository defRepository = GameUtl.GameComponent<DefRepository>();

            List<HealFacilityComponentDef> healFacilityComponentDefs = defRepository.DefRepositoryDef.AllDefs.OfType<HealFacilityComponentDef>().ToList();
            foreach (HealFacilityComponentDef hfcDef in healFacilityComponentDefs)
            {
                if (hfcDef.name.Contains("MedicalBay"))
                {
                    hfcDef.BaseHeal = AssortedAdjustments.MadsAssortedAdjustmentsConfig.MedicalBayBaseHeal;
                    currentHealFacilityHealOutput = hfcDef.BaseHeal;
                }
                else if (hfcDef.name.Contains("MutationLab"))
                {
                    hfcDef.BaseHeal = AssortedAdjustments.MadsAssortedAdjustmentsConfig.MutationLabMutogHealAmount;
                    currentHealFacilityMutogHealOutput = hfcDef.BaseHeal;
                }
                else if (hfcDef.name.Contains("LivingQuarters"))
                {
                    hfcDef.BaseStaminaHeal = AssortedAdjustments.MadsAssortedAdjustmentsConfig.LivingQuartersBaseStaminaHeal;
                    currentHealFacilityStaminaHealOutput = hfcDef.BaseStaminaHeal;
                }
                Logger.Info($"[FacilityAdjustments_Apply] hfcDef: {hfcDef.name}, GUID: {hfcDef.Guid}, BaseHeal: {hfcDef.BaseHeal}, BaseStaminaHeal: {hfcDef.BaseStaminaHeal}");
            }

            List<ExperienceFacilityComponentDef> experienceFacilityComponentDefs = defRepository.DefRepositoryDef.AllDefs.OfType<ExperienceFacilityComponentDef>().ToList();
            foreach (ExperienceFacilityComponentDef efcDef in experienceFacilityComponentDefs)
            {
                if (efcDef.name.Contains("TrainingFacility"))
                {
                    efcDef.ExperiencePerUser = AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilityBaseExperienceAmount;
                    currentExperienceFacilityExperienceOutput = efcDef.ExperiencePerUser;

                    efcDef.SkillPointsPerDay = AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilityBaseSkillPointsAmount;
                }
                Logger.Info($"[FacilityAdjustments_Apply] efcDef: {efcDef.name}, GUID: {efcDef.Guid}, ExperiencePerUser: {efcDef.ExperiencePerUser}, SkillPointsPerDay: {efcDef.SkillPointsPerDay}");
            }

            List<VehicleSlotFacilityComponentDef> vehicleSlotFacilityComponentDefs = defRepository.DefRepositoryDef.AllDefs.OfType<VehicleSlotFacilityComponentDef>().Where(vsfDef => vsfDef.name.Contains("VehicleBay")).ToList();
            foreach (VehicleSlotFacilityComponentDef vsfDef in vehicleSlotFacilityComponentDefs)
            {
                vsfDef.AircraftHealAmount = AssortedAdjustments.MadsAssortedAdjustmentsConfig.VehicleBayAircraftHealAmount;
                vsfDef.VehicleHealAmount = AssortedAdjustments.MadsAssortedAdjustmentsConfig.VehicleBayVehicleHealAmount;

                currentVehicleSlotFacilityAircraftHealOuput = vsfDef.AircraftHealAmount;
                currentVehicleSlotFacilityVehicleHealOuput = vsfDef.VehicleHealAmount;

                Logger.Info($"[FacilityAdjustments_Apply] vsfDef: {vsfDef.name}, GUID: {vsfDef.Guid}, AircraftHealAmount: {vsfDef.AircraftHealAmount}, VehicleHealAmount: {vsfDef.VehicleHealAmount}");
            }

            List<ResourceGeneratorFacilityComponentDef> resourceGeneratorFacilityComponentDefs = defRepository.DefRepositoryDef.AllDefs.OfType<ResourceGeneratorFacilityComponentDef>().ToList();
            foreach (ResourceGeneratorFacilityComponentDef rgfDef in resourceGeneratorFacilityComponentDefs)
            {
                if (rgfDef.name.Contains("FabricationPlant"))
                {
                    ResourcePack resources = rgfDef.BaseResourcesOutput;
                    ResourceUnit supplies = new ResourceUnit(ResourceType.Production, AssortedAdjustments.MadsAssortedAdjustmentsConfig.FabricationPlantGenerateProductionAmount);
                    resources.Set(supplies);

                    // When added here they are also affected by general research buffs. This is NOT intended.
                    /*
                    if(AssortedAdjustments.Settings.FabricationPlantGenerateMaterialsAmount > 0f)
                    {
                        float value = AssortedAdjustments.Settings.FabricationPlantGenerateMaterialsAmount / AssortedAdjustments.Settings.GenerateResourcesBaseDivisor;
                        ResourceUnit materials = new ResourceUnit(ResourceType.Materials, value);
                        resources.AddUnique(materials);
                    }
                    */
                }
                else if (rgfDef.name.Contains("ResearchLab"))
                {
                    ResourcePack resources = rgfDef.BaseResourcesOutput;
                    ResourceUnit supplies = new ResourceUnit(ResourceType.Research, AssortedAdjustments.MadsAssortedAdjustmentsConfig.ResearchLabGenerateResearchAmount);
                    resources.Set(supplies);

                    // When added here they are also affected by general research buffs (Synedrion research). This is NOT intended.
                    /*
                    if (AssortedAdjustments.Settings.ResearchLabGenerateTechAmount > 0f)
                    {
                        float value = AssortedAdjustments.Settings.ResearchLabGenerateTechAmount / AssortedAdjustments.Settings.GenerateResourcesBaseDivisor;
                        ResourceUnit tech = new ResourceUnit(ResourceType.Tech, value);
                        resources.AddUnique(tech);
                    }
                    */
                }
                else if (rgfDef.name.Contains("FoodProduction"))
                {
                    ResourcePack resources = rgfDef.BaseResourcesOutput;
                    ResourceUnit supplies = new ResourceUnit(ResourceType.Supplies, AssortedAdjustments.MadsAssortedAdjustmentsConfig.FoodProductionGenerateSuppliesAmount);
                    resources.Set(supplies);

                    currentFoodProductionFacilitySuppliesOutput = AssortedAdjustments.MadsAssortedAdjustmentsConfig.FoodProductionGenerateSuppliesAmount;
                }
                else if (rgfDef.name.Contains("BionicsLab"))
                {
                    ResourcePack resources = rgfDef.BaseResourcesOutput;
                    ResourceUnit supplies = new ResourceUnit(ResourceType.Research, AssortedAdjustments.MadsAssortedAdjustmentsConfig.BionicsLabGenerateResearchAmount);
                    resources.Set(supplies);
                }
                else if (rgfDef.name.Contains("MutationLab"))
                {
                    ResourcePack resources = rgfDef.BaseResourcesOutput;
                    ResourceUnit supplies = new ResourceUnit(ResourceType.Mutagen, AssortedAdjustments.MadsAssortedAdjustmentsConfig.MutationLabGenerateMutagenAmount);
                    resources.Set(supplies);
                }

                Logger.Info($"[FacilityAdjustments_Apply] rgfDef: {rgfDef.name}, GUID: {rgfDef.Guid}, BaseResourcesOutput: {rgfDef.BaseResourcesOutput.ToString()}");
            }



            HarmonyHelpers.Patch(harmony, typeof(ResourceGeneratorFacilityComponent), "UpdateOutput", typeof(FacilityAdjustments), null, "Postfix_ResourceGeneratorFacilityComponent_UpdateOutput");
            HarmonyHelpers.Patch(harmony, typeof(HealFacilityComponent), "UpdateOutput", typeof(FacilityAdjustments), null, "Postfix_HealFacilityComponent_UpdateOutput");
            HarmonyHelpers.Patch(harmony, typeof(ExperienceFacilityComponent), "UpdateOutput", typeof(FacilityAdjustments), null, "Postfix_ExperienceFacilityComponent_UpdateOutput");
            HarmonyHelpers.Patch(harmony, typeof(VehicleSlotFacilityComponent), "UpdateOutput", typeof(FacilityAdjustments), null, "Postfix_VehicleSlotFacilityComponent_UpdateOutput");
            if (AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilitiesGenerateSkillpoints && AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilityBaseSkillPointsAmount > 0)
            {
                HarmonyHelpers.Patch(harmony, typeof(GeoLevelController), "DailyUpdate", typeof(FacilityAdjustments), null, "Postfix_GeoLevelController_DailyUpdate");
            }

            // UI
            HarmonyHelpers.Patch(harmony, typeof(UIFacilityTooltip), "Show", typeof(FacilityAdjustments), null, "Postfix_UIFacilityTooltip_Show");
            HarmonyHelpers.Patch(harmony, typeof(UIFacilityInfoPopup), "Show", typeof(FacilityAdjustments), null, "Postfix_UIFacilityInfoPopup_Show");
        }



        // Patches
        public static void Postfix_ResourceGeneratorFacilityComponent_UpdateOutput(ResourceGeneratorFacilityComponent __instance)
        {
            try
            {
                if (__instance.Facility.PxBase.Site.Owner is GeoPhoenixFaction)
                {
                    string owningFaction = __instance.Facility.PxBase.Site.Owner.Name.Localize();
                    string facilityName = __instance.Facility.ViewElementDef.DisplayName1.Localize();
                    string facilityId = __instance.Facility.FacilityId.ToString();
                    //Logger.Info($"[ResourceGeneratorFacilityComponent_UpdateOutput_POSTFIX] owningFaction: {owningFaction}, facilityName: {facilityName}, facilityId: {facilityId}, ResourceOutput: {__instance.ResourceOutput}");

                    if (__instance.Def.name.Contains("FoodProduction"))
                    {
                        currentFoodProductionFacilitySuppliesOutput = __instance.ResourceOutput.Values.Where(u => u.Type == ResourceType.Supplies).First().Value;

                        /*
                        foreach (ResourceUnit resourceUnit in __instance.ResourceOutput.Values)
                        {
                            if (resourceUnit.Type == ResourceType.Supplies)
                            {
                                currentFoodProductionFacilitySuppliesOutput = resourceUnit.Value;
                            }
                        }
                        */
                    }
                }

                // All factions
                if (__instance.Def.name.Contains("FabricationPlant") && AssortedAdjustments.MadsAssortedAdjustmentsConfig.FabricationPlantGenerateMaterialsAmount > 0)
                {
                    float value = AssortedAdjustments.MadsAssortedAdjustmentsConfig.FabricationPlantGenerateMaterialsAmount / AssortedAdjustments.MadsAssortedAdjustmentsConfig.GenerateResourcesBaseDivisor;
                    ResourceUnit materials = new ResourceUnit(ResourceType.Materials, value);
                    __instance.ResourceOutput.AddUnique(materials);
                }
                else if (__instance.Def.name.Contains("ResearchLab") && AssortedAdjustments.MadsAssortedAdjustmentsConfig.ResearchLabGenerateTechAmount > 0)
                {
                    float value = AssortedAdjustments.MadsAssortedAdjustmentsConfig.ResearchLabGenerateTechAmount / AssortedAdjustments.MadsAssortedAdjustmentsConfig.GenerateResourcesBaseDivisor;
                    ResourceUnit tech = new ResourceUnit(ResourceType.Tech, value);
                    __instance.ResourceOutput.AddUnique(tech);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public static void Postfix_HealFacilityComponent_UpdateOutput(HealFacilityComponent __instance)
        {
            try
            {
                if (__instance.Facility.PxBase.Site.Owner is GeoPhoenixFaction)
                {
                    string owningFaction = __instance.Facility.PxBase.Site.Owner.Name.Localize();
                    string facilityName = __instance.Facility.ViewElementDef.DisplayName1.Localize();
                    string facilityId = __instance.Facility.FacilityId.ToString();
                    //Logger.Info($"[ResourceGeneratorFacilityComponent_UpdateOutput_POSTFIX] owningFaction: {owningFaction}, facilityName: {facilityName}, facilityId: {facilityId}, HealOutput: {__instance.HealOutput}, StaminaHealOutput: {__instance.StaminaHealOutput}");

                    if (__instance.Def.name.Contains("MedicalBay"))
                    {
                        currentHealFacilityHealOutput = __instance.HealOutput;
                    }
                    else if (__instance.Def.name.Contains("LivingQuarters"))
                    {
                        currentHealFacilityStaminaHealOutput = __instance.StaminaHealOutput;
                    }
                    else if (__instance.Def.name.Contains("MutationLab"))
                    {
                        currentHealFacilityMutogHealOutput = __instance.HealOutput;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public static void Postfix_ExperienceFacilityComponent_UpdateOutput(ExperienceFacilityComponent __instance)
        {
            try
            {
                if (__instance.Facility.PxBase.Site.Owner is GeoPhoenixFaction)
                {
                    string owningFaction = __instance.Facility.PxBase.Site.Owner.Name.Localize();
                    string facilityName = __instance.Facility.ViewElementDef.DisplayName1.Localize();
                    string facilityId = __instance.Facility.FacilityId.ToString();
                    //Logger.Info($"[ResourceGeneratorFacilityComponent_UpdateOutput_POSTFIX] owningFaction: {owningFaction}, facilityName: {facilityName}, facilityId: {facilityId}, HealOutput: {__instance.HealOutput}, StaminaHealOutput: {__instance.StaminaHealOutput}");

                    if (__instance.Def.name.Contains("TrainingFacility"))
                    {
                        currentExperienceFacilityExperienceOutput = __instance.ExperienceOutput;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public static void Postfix_VehicleSlotFacilityComponent_UpdateOutput(VehicleSlotFacilityComponent __instance)
        {
            try
            {
                if (__instance.Facility.PxBase.Site.Owner is GeoPhoenixFaction)
                {
                    string owningFaction = __instance.Facility.PxBase.Site.Owner.Name.Localize();
                    string facilityName = __instance.Facility.ViewElementDef.DisplayName1.Localize();
                    string facilityId = __instance.Facility.FacilityId.ToString();
                    //Logger.Info($"[ResourceGeneratorFacilityComponent_UpdateOutput_POSTFIX] owningFaction: {owningFaction}, facilityName: {facilityName}, facilityId: {facilityId}, AircraftHealOuput: {__instance.AircraftHealOuput}, VehicletHealOuput: {__instance.VehicletHealOuput}");

                    currentVehicleSlotFacilityAircraftHealOuput = __instance.AircraftHealOuput;
                    currentVehicleSlotFacilityVehicleHealOuput = __instance.VehicletHealOuput;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }



        public static void Postfix_GeoLevelController_DailyUpdate(GeoLevelController __instance)
        {
            try
            {
                foreach (GeoFaction faction in __instance.Factions)
                {
                    if (faction.Def.UpdateFaction && faction is GeoPhoenixFaction geoPhoenixFaction)
                    {
                        geoPhoenixFaction.UpdateBasesDaily();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }



        public static void Postfix_UIFacilityTooltip_Show(UIFacilityTooltip __instance, PhoenixFacilityDef facility, GeoPhoenixBase currentBase)
        {
            try
            {
                if (currentBase == null)
                {
                    return;
                }

                if (facility.name.Contains("MedicalBay"))
                {
                    //float baseHealOutput = facility.GetComponent<HealFacilityComponentDef>().BaseHeal;
                    //float currentBonusValue = currentHealFacilityHealOutput > baseHealOutput ? (currentHealFacilityHealOutput - baseHealOutput) : 0;
                    //string currentBonus = currentBonusValue > 0 ? $"({baseHealOutput} + {currentBonusValue})" : "";

                    __instance.Description.text = $"All soldiers at the base (even if assigned to an aircraft) will recover {currentHealFacilityHealOutput} Hit Points per hour for each medical facility in the base.";
                }
                else if (facility.name.Contains("LivingQuarters"))
                {
                    __instance.Description.text = $"All soldiers at the base (even if assigned to an aircraft) will recover {currentHealFacilityStaminaHealOutput} Stamina points per hour for each living quarters in the base.";
                }
                else if (facility.name.Contains("TrainingFacility"))
                {
                    string s1 = $"All soldiers at the base (even if assigned to an aircraft) will gain {currentExperienceFacilityExperienceOutput} Experience Points per hour for each training facility in the base.";
                    string s2 = "";
                    if (AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilitiesGenerateSkillpoints && AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilityBaseSkillPointsAmount > 0)
                    {
                        string pluralizeSP = AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilityBaseSkillPointsAmount > 1 ? "skillpoints" : "skillpoint";
                        s2 = $"Contributes {AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilityBaseSkillPointsAmount} {pluralizeSP} to the global pool every day.";
                    }
                    __instance.Description.text = $"{s1}\n{s2}";
                }
                else if (facility.name.Contains("MutationLab"))
                {
                    string org = __instance.Description.text;
                    string add = $"All mutogs at the base (even if assigned to an aircraft) will recover additional {currentHealFacilityMutogHealOutput} Hit Points per hour for each mutation lab in the base.";
                    __instance.Description.text = $"{org}\n{add}";
                }
                else if (facility.name.Contains("VehicleBay"))
                {
                    __instance.Description.text = $"Vehicles and aircraft at the base recover {currentVehicleSlotFacilityVehicleHealOuput} Hit Points per hour. Allows maintenance of 2 ground vehicles and 2 aircraft.";
                }
                else if (facility.name.Contains("FabricationPlant") && AssortedAdjustments.MadsAssortedAdjustmentsConfig.FabricationPlantGenerateMaterialsAmount > 0)
                {
                    string org = __instance.Description.text;
                    string add = $"Every plant generates {AssortedAdjustments.MadsAssortedAdjustmentsConfig.FabricationPlantGenerateMaterialsAmount} material per hour.";
                    __instance.Description.text = $"{org}\n{add}";
                }
                else if (facility.name.Contains("ResearchLab") && AssortedAdjustments.MadsAssortedAdjustmentsConfig.ResearchLabGenerateTechAmount > 0)
                {
                    string org = __instance.Description.text;
                    string add = $"Every lab generates {AssortedAdjustments.MadsAssortedAdjustmentsConfig.ResearchLabGenerateTechAmount} tech per hour.";
                    __instance.Description.text = $"{org}\n{add}";
                }
                else if (facility.name.Contains("FoodProduction"))
                {
                    int foodProductionUnits = (int)Math.Round(currentFoodProductionFacilitySuppliesOutput * 24);
                    __instance.Description.text = $"A food production facility that generates enough food for {foodProductionUnits} soldiers each day.";
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public static void Postfix_UIFacilityInfoPopup_Show(UIFacilityInfoPopup __instance, GeoPhoenixFacility facility)
        {
            try
            {
                if (facility.Def.name.Contains("MedicalBay"))
                {
                    __instance.Description.text = $"All soldiers at the base (even if assigned to an aircraft) will recover {currentHealFacilityHealOutput} Hit Points per hour for each medical facility in the base.";
                }
                else if (facility.Def.name.Contains("LivingQuarters"))
                {
                    __instance.Description.text = $"All soldiers at the base (even if assigned to an aircraft) will recover {currentHealFacilityStaminaHealOutput} Stamina points per hour for each living quarters in the base.";
                }
                else if (facility.Def.name.Contains("TrainingFacility"))
                {
                    string s1 = $"All soldiers at the base (even if assigned to an aircraft) will gain {currentExperienceFacilityExperienceOutput} Experience Points per hour for each training facility in the base.";
                    string s2 = "";
                    if (AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilitiesGenerateSkillpoints && AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilityBaseSkillPointsAmount > 0)
                    {
                        string pluralizeSP = AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilityBaseSkillPointsAmount > 1 ? "skillpoints" : "skillpoint";
                        s2 = $"Contributes {AssortedAdjustments.MadsAssortedAdjustmentsConfig.TrainingFacilityBaseSkillPointsAmount} {pluralizeSP} to the global pool every day.";
                    }
                    __instance.Description.text = $"{s1}\n{s2}";
                }
                else if (facility.Def.name.Contains("MutationLab"))
                {
                    string org = __instance.Description.text;
                    string add = $"All mutogs at the base (even if assigned to an aircraft) will recover additional {currentHealFacilityMutogHealOutput} Hit Points per hour for each mutation lab in the base.";
                    __instance.Description.text = $"{org}\n{add}";
                }
                else if (facility.Def.name.Contains("VehicleBay"))
                {
                    __instance.Description.text = $"Vehicles and aircraft at the base recover {currentVehicleSlotFacilityVehicleHealOuput} Hit Points per hour. Allows maintenance of 2 ground vehicles and 2 aircraft.";
                }
                else if (facility.Def.name.Contains("FabricationPlant") && AssortedAdjustments.MadsAssortedAdjustmentsConfig.FabricationPlantGenerateMaterialsAmount > 0)
                {
                    string org = __instance.Description.text;
                    string add = $"Every plant generates {AssortedAdjustments.MadsAssortedAdjustmentsConfig.FabricationPlantGenerateMaterialsAmount} material per hour.";
                    __instance.Description.text = $"{org}\n{add}";
                }
                else if (facility.Def.name.Contains("ResearchLab") && AssortedAdjustments.MadsAssortedAdjustmentsConfig.ResearchLabGenerateTechAmount > 0)
                {
                    string org = __instance.Description.text;
                    string add = $"Every lab generates {AssortedAdjustments.MadsAssortedAdjustmentsConfig.ResearchLabGenerateTechAmount} tech per hour.";
                    __instance.Description.text = $"{org}\n{add}";
                }
                else if (facility.Def.name.Contains("FoodProduction"))
                {
                    int foodProductionUnits = (int)Math.Round(currentFoodProductionFacilitySuppliesOutput * 24);
                    __instance.Description.text = $"A food production facility that generates enough food for {foodProductionUnits} soldiers each day.";
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}
