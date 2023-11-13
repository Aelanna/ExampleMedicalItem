using System.Collections.Generic;

using RimWorld;
using Verse;

namespace ExampleMedicalItem
{
    // This is the ThingComp subclass that is invoked when the parent item is used
    public class CompUseEffect_RemoveHediffs : CompUseEffect
    {

        // This helper property lets us read the configuration
        public CompProperties_RemoveHediffs Props => (CompProperties_RemoveHediffs)props;

        // A predicate method to help filter hediffs on the user
        public bool IsRemovableHediff(Hediff hediff)
        {
            return Props.hediffsRemoved.Contains(hediff.def);
        }

        // DoEffect is called when the item is used
        public override void DoEffect(Pawn usedBy)
        {
            base.DoEffect(usedBy);

            // Find list of removable hediffs
            List<Hediff> hediffs = new List<Hediff>();
            usedBy.health.hediffSet.GetHediffs(ref hediffs, IsRemovableHediff);

            if (hediffs.Count > 0)
            {
                // Remove each hediff from the user
                foreach (Hediff hediff in hediffs)
                {
                    usedBy.health.RemoveHediff(hediff);
                }

                // Notify the player of how many hediffs were removed
                Messages.Message(
                    text: "ExampleMedicalItem_CountConditionsRemoved".Translate(parent.LabelCapNoCount, hediffs.Count, usedBy.Name.ToStringShort),
                    def: MessageTypeDefOf.TaskCompletion,
                    historical: false
                );
            }
            else
            {
                // Notify the player that no hediffs were removed
                Messages.Message(
                    text: "ExampleMedicalItem_NoConditionsRemoved".Translate(parent.LabelCap, usedBy.Name.ToStringShort),
                    def: MessageTypeDefOf.TaskCompletion,
                    historical: false
                );
            }

            // Add each side effect to the user
            if (Props.sideEffects != null)
            {
                foreach(HediffDef def in Props.sideEffects)
                {
                    usedBy.health.AddHediff(def);
                }
            }
        }
    }
}
