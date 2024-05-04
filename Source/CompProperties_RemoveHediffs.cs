using System.Collections.Generic;

using RimWorld;
using Verse;

namespace ExampleMedicalItem
{
  // This is the custom CompProperties used to configure this effect in XML
  public class CompProperties_RemoveHediffs : CompProperties_UseEffect
  {
    // List of HediffDefs that we want this medicine to remove
    public List<HediffDef> hediffsRemoved;

    // List of HediffDefs that this medicine causes as a side effect
    public List<HediffDef> sideEffects;

    public CompProperties_RemoveHediffs()
    {
      // Specifying the compClass in the constructor ensures that the correct ThingComp subclass is invoked
      compClass = typeof(CompUseEffect_RemoveHediffs);
    }

    public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
    {
      // Throw a configuration error on game load if no HediffDefs were specified for purging
      if (hediffsRemoved.NullOrEmpty())
      {
        // Error messages usually don't need to be localized as they are meant to inform developers, not players
        yield return $"(Example Medical Item) {parentDef.defName} has CompProperties_RemoveHediffs, but is not configured to remove any hediffs.";
      }
    }
  }
}
