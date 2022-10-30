using System;
using System.IO;
using System.Reflection;
using Base.Build;
using System.Linq;
using PhoenixPoint.Home.View.ViewModules;
using HarmonyLib;
using static MadsAssortedAdjustments.MadsAssortedAdjustmentsConfig;

namespace MadsAssortedAdjustments
{
    public static class AssortedAdjustments
    {
        internal static string LogPath;
        internal static string ModDirectory;
        internal static Settings MadsAssortedAdjustmentsConfig;
        internal static string[] ValidPresets = new string[] { "vanilla", "hardcore", "mad" };
        internal static HarmonyInstance Harmony;

        internal static string ModName = "AssortedAdjustments";
        internal static Version ModVersion;



        // Modnix Entrypoints
        public static void SplashMod(Func<string, object, object> api)
        {
            Harmony = HarmonyInstance.Create("de.mad.AssortedAdjustments");

            ModDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            LogPath = Path.Combine(ModDirectory, "AssortedAdjustments.log");
            MadsAssortedAdjustmentsConfig = api("config", null) as Settings ?? new Settings();

            if (!string.IsNullOrEmpty(MadsAssortedAdjustmentsConfig.DebugDevKey) && MadsAssortedAdjustmentsConfig.DebugDevKey == "mad" || !string.IsNullOrEmpty(MadsAssortedAdjustmentsConfig.BalancePresetId) && MadsAssortedAdjustmentsConfig.BalancePresetId == "mad")
            {
                MadsAssortedAdjustmentsConfig.DebugLevel = 3;
            }
            Logger.Initialize(LogPath, MadsAssortedAdjustmentsConfig.DebugLevel, ModDirectory, nameof(AssortedAdjustments));

            object ModInfo = api("mod_info", null);
            ModVersion = (Version)ModInfo.GetType().GetField("Version").GetValue(ModInfo);




            Logger.Always($"Modnix Mad.AssortedAdjustments.SplashMod initialised.");
            //Logger.Always($"Settings: {Settings}");



            try
            {
                MadsAssortedAdjustmentsConfig.ToMarkdownFile(Path.Combine(ModDirectory, "settings-reference.md"));
                MadsAssortedAdjustmentsConfig.ToHtmlFile(Path.Combine(ModDirectory, "settings-reference.htm"));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }




        public static void ApplyAll()
        {

            if (MadsAssortedAdjustmentsConfig.UnlockItemsByResearch)
            {
                UnlockItemsByResearch.Init();
            }
        }



        [HarmonyPatch(typeof(UIModuleBuildRevision), "SetRevisionNumber")]
        public static class UIModuleBuildRevision_SetRevisionNumber_Patch
        {
            public static void Postfix(UIModuleBuildRevision __instance)
            {
                try
                {
                    __instance.BuildRevisionNumber.text = $"{RuntimeBuildInfo.UserVersion} w/{ModName} {ModVersion}";
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }
    }
}
