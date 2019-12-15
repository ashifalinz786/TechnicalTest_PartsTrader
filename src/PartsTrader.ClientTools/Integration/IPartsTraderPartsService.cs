using System.Collections.Generic;

using PartsTrader.ClientTools.Api.Data;

namespace PartsTrader.ClientTools.Integration
{
    /// <summary>
    /// Provides access to the PartsTrader Parts Service.
    /// </summary>
    public interface IPartsTraderPartsService
    {
        /// <summary>
        /// Retrieves all parts from the remote parts repository that are compatible with the provided part number.
        /// </summary>
        /// <param name="partNumber">
        /// The part number to lookup.
        /// </param>
        /// <returns>
        /// All compatible parts found.
        /// </returns>
        IEnumerable<PartSummary> FindAllCompatibleParts(string partNumber);
    }
}
