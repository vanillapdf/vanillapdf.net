using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics.Extensions
{
    /// <summary>
    /// Extension methods for PdfAction type checking and conversion.
    /// </summary>
    public static class PdfActionExtensions
    {
        /// <summary>
        /// Checks if the action is of the specified type.
        /// </summary>
        public static bool Is<T>(this PdfAction action) where T : PdfAction
        {
            using (var upgraded = action.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the action as the specified type, or null if type doesn't match.
        /// Caller must dispose the returned object.
        /// </summary>
        public static T As<T>(this PdfAction action) where T : PdfAction
        {
            var upgraded = action.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to the most derived action type.
        /// </summary>
        internal static PdfAction Upgrade(this PdfAction action)
        {
            var actionType = action.ActionType;
            switch (actionType) {
                case PdfActionType.GoTo:
                    return new PdfGoToAction((PdfGoToActionSafeHandle)action.Handle);
                case PdfActionType.URI:
                    return new PdfURIAction((PdfURIActionSafeHandle)action.Handle);
                case PdfActionType.GoToRemote:
                    return new PdfGoToRemoteAction((PdfGoToRemoteActionSafeHandle)action.Handle);
                case PdfActionType.Named:
                    return new PdfNamedAction((PdfNamedActionSafeHandle)action.Handle);
                default:
                    throw new PdfManagedException($"Cannot upgrade action with unsupported type: {actionType}");
            }
        }
    }
}
