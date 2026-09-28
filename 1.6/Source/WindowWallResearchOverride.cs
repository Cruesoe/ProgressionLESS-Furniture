using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ProgressionLESSFurniture
{
    [StaticConstructorOnStartup]
    public static class ResearchPrerequisiteOverride
    {
        static ResearchPrerequisiteOverride()
        {
            LongEventHandler.ExecuteWhenFinished(Apply);
        }

        private static void Apply()
        {
            ResearchProjectDef windowWallProject = DefDatabase<ResearchProjectDef>.GetNamedSilentFail("Ferny_WindowWall");
            if (windowWallProject != null)
            {
                SetOnlyPrerequisite("RB_GlassWall", windowWallProject);
                SetOnlyPrerequisite("RB_ReinforcedGlassWall", windowWallProject);
                SetOnlyPrerequisite("RB_ClerestoryWall", windowWallProject);
                SetOnlyPrerequisite("RB_ReinforcedClerestoryWall", windowWallProject);
            }

            ResearchProjectDef plantPotsProject = DefDatabase<ResearchProjectDef>.GetNamedSilentFail("Ferny_PlantPots");
            if (plantPotsProject != null)
            {
                SetOnlyPrerequisite("VFE_LongPlantPot", plantPotsProject);
            }

            ResearchProjectDef modularCounterProject = DefDatabase<ResearchProjectDef>.GetNamedSilentFail("Ferny_ModularCounter");
            if (modularCounterProject != null)
            {
                SetOnlyPrerequisite("Table_Counter", modularCounterProject);
            }

            ResearchProjectDef cribProject = DefDatabase<ResearchProjectDef>.GetNamedSilentFail("Ferny_Crib");
            if (cribProject != null)
            {
                SetOnlyPrerequisite("Crib", cribProject);
            }
        }

        private static void SetOnlyPrerequisite(string defName, ResearchProjectDef prerequisite)
        {
            ThingDef thingDef = DefDatabase<ThingDef>.GetNamedSilentFail(defName);
            if (thingDef == null)
            {
                return;
            }

            thingDef.researchPrerequisites = new List<ResearchProjectDef> { prerequisite };
        }
    }
}
