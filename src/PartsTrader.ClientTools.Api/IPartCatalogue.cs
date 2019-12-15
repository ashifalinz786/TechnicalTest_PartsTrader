using System.Collections.Generic;

using PartsTrader.ClientTools.Api.Data;

namespace PartsTrader.ClientTools.Api
{
    /// <summary>
    /// Provides access to the PartsTrader parts catalogue.
    /// </summary>
    /// <remarks>
    /// The parts catalogue maintains a set of compatible parts whereby one specific part may be replaced by another similar or
    /// compatible part during repairs.
    /// </remarks>
    public interface IPartCatalogue
    {
        /// <summary>
        /// Retrieves all compatible parts for the nominated part.
        /// </summary>
        /// <param name="partNumber">
        /// The part number for the part being checked.
        /// </param>
        /// <exception cref="InvalidPartException">
        /// If the supplied part number does not represent a valid part.
        /// </exception>
        /// <returns>
        /// Summaries of all compatible parts for the nominated part type.
        /// </returns>
        IEnumerable<PartSummary> GetCompatibleParts(string partNumber);
    }
}