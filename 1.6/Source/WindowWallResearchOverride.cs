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
            SetPrerequisites("RB_GlassWall", "Ferny_WindowWall", "Smithing");
            SetPrerequisites("RB_ReinforcedGlassWall", "Ferny_WindowWall", "Smithing");
            SetPrerequisites("RB_ClerestoryWall", "Ferny_WindowWall", "Smithing");
            SetPrerequisites("RB_ReinforcedClerestoryWall", "Ferny_WindowWall", "Smithing");
            SetPrerequisites("VFE_LongPlantPot", "Ferny_PlantPots");
            SetPrerequisites("Table_Counter", "ComplexFurniture", "Ferny_Tables");
            SetPrerequisites("Crib", "Ferny_Crib", "Ferny_RoughCrib");
        }

        // Replaces the building's research with the given projects; skipped if the building or first project is missing, later projects only when loaded
        private static void SetPrerequisites(string defName, string project, params string[] extras)
        {
            ThingDef thingDef = DefDatabase<ThingDef>.GetNamedSilentFail(defName);
            ResearchProjectDef main = DefDatabase<ResearchProjectDef>.GetNamedSilentFail(project);
            if (thingDef == null || main == null)
            {
                return;
            }

            var prerequisites = new List<ResearchProjectDef> { main };
            foreach (string extra in extras)
            {
                ResearchProjectDef extraDef = DefDatabase<ResearchProjectDef>.GetNamedSilentFail(extra);
                if (extraDef != null && !prerequisites.Contains(extraDef))
                {
                    prerequisites.Add(extraDef);
                }
            }

            thingDef.researchPrerequisites = prerequisites;
        }
    }
}
