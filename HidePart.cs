using System.Collections;
using UnityEngine;
using KSP.UI.Screens;

namespace HidePart
{
    /// <summary>
    /// Adds a filter to the Editor's part list to hide unpurchased parts in Career mode.
    /// This approach uses KSP's native filtering system for maximum compatibility.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    public class HidePartEditorFilter : MonoBehaviour
    {
        private static EditorPartListFilter<AvailablePart> hideUnpurchasedFilter;

        private void Start()
        {
            StartCoroutine(SetupFilter());
        }

        private IEnumerator SetupFilter()
        {
            // Wait for the EditorPartList to be fully instantiated
            while (EditorPartList.Instance == null)
            {
                yield return new WaitForSeconds(0.2f);
            }

            if (hideUnpurchasedFilter == null)
            {
                // The criteria returns TRUE to KEEP the part, FALSE to HIDE it.
                hideUnpurchasedFilter = new EditorPartListFilter<AvailablePart>("HideUnpurchased", (ap) =>
                {
                    // Failsafe: Ensure critical game states exist
                    if (ap == null || HighLogic.CurrentGame == null || HighLogic.CurrentGame.Parameters == null) 
                        return true;

                    // Only apply this logic in Career mode. 
                    // Sandbox and Science modes don't use part purchase mechanics.
                    if (HighLogic.CurrentGame.Mode != Game.Modes.CAREER)
                        return true;

                    // If entry purchase is bypassed in difficulty settings, show all parts.
                    if (HighLogic.CurrentGame.Parameters.Difficulty.BypassEntryPurchaseAfterResearch) 
                        return true;

                    // Only show the part if the entry cost has been paid in R&D.
                    return ResearchAndDevelopment.PartModelPurchased(ap);
                });
            }

            // Apply the filter to the ExcludeFilters list
            // KSP will handle cleaning this up when the Editor scene is destroyed.
            EditorPartList.Instance.ExcludeFilters.AddFilter(hideUnpurchasedFilter);
            Debug.Log("[HidePart] Unpurchased part filter successfully added to VAB/SPH.");

            // Force the UI to refresh and apply the new rules immediately
            EditorPartList.Instance.Refresh();
        }
    }
}
