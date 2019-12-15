using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using PartsTrader.ClientTools.Api;
using PartsTrader.ClientTools.Api.Data;
using PartsTrader.ClientTools.Integration;

namespace PartsTrader.ClientTools
{
    public class PartCatalogue : IPartCatalogue
    {
        private IPartsTraderPartsService partsService;

        public PartCatalogue(IPartsTraderPartsService partsTraderPartsService)
        {
            partsService = partsTraderPartsService;
        }

        public IEnumerable<PartSummary> GetCompatibleParts(string partNumber)
        {
            bool isValidPartNumber = ValidatePartNumber(partNumber);
            if (!isValidPartNumber)
            {
                throw new InvalidPartException("Invalid part number : " + partNumber + " provided. Please check part number and try again");
            }

            bool isPartInExclusionList = CheckExclusionList(partNumber);
            if (isPartInExclusionList)
            {
                return new List<PartSummary>();
            }

            return partsService.FindAllCompatibleParts(partNumber);
        }

        public bool ValidatePartNumber(string partNumber)
        {
            if(partNumber == null)
            {
                return false;
            }

            Regex validFormat = new Regex(@"^[0-9][0-9][0-9][0-9]-[A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9]+$");

            return validFormat.IsMatch(partNumber);
        }

        public bool CheckExclusionList(string partNumber)
        {
            List<PartSummary> partsSummaryList;
            string executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            using (StreamReader sr = new StreamReader(@executingDirectory+"/Exclusions.json"))
            {
                string json = sr.ReadToEnd();
                partsSummaryList = new JavaScriptSerializer().Deserialize<List<PartSummary>>(json);
            }            

            return partsSummaryList.Any(p => p.PartNumber == partNumber);
        }
    }
}