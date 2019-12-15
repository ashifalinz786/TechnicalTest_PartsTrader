using Moq;
using NUnit.Framework;
using PartsTrader.ClientTools.Api;
using PartsTrader.ClientTools.Api.Data;
using PartsTrader.ClientTools.Integration;
using System.Collections.Generic;
using System.Linq;

namespace PartsTrader.ClientTools.Tests
{
    /// <summary>
    /// Tests for <see cref="PartCatalogue" />.
    /// </summary>
    [TestFixture]
    public class PartCatalogueTests
    {
        private Mock<IPartsTraderPartsService> mockPartsService;

        [SetUp]
        public void Setup()
        {
            List<PartSummary> compatibleParts = new List<PartSummary>() { 
                new PartSummary(),
                new PartSummary()
            };
            mockPartsService = new Mock<IPartsTraderPartsService>();
            mockPartsService.Setup(x => x.FindAllCompatibleParts(It.IsAny<string>()))
                .Returns(compatibleParts);

        }

        #region Validate Parts Number

        [Test]
        public void ValidatePartsNumber_ValidPart_AlphabeticPartCode()
        {      
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            bool isValid = catalogue.ValidatePartNumber("1234-abcd");

            Assert.IsTrue(isValid);
        }

        [Test]
        public void ValidatePartsNumber_ValidPart_AlphanumericPartCode()
        {
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            bool isValid = catalogue.ValidatePartNumber("4321-a1b2c3d4");

            Assert.IsTrue(isValid);

        }

        [Test]
        public void ValidatePartsNumber_InvalidPart_InvalidPartId()
        {
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            bool isValid = catalogue.ValidatePartNumber("a234-abcd");

            Assert.IsFalse(isValid);
        }

        [Test]
        public void ValidatePartsNumber_InvalidPart_InvalidPartCode()
        {
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            bool isValid = catalogue.ValidatePartNumber("1234-abc");

            Assert.IsFalse(isValid);
        }

        #endregion

        #region CheckExclusionList

        [Test]
        public void CheckExclusionList_IsExcluded1()
        {
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            bool IsExcluded = catalogue.CheckExclusionList("1111-Invoice");

            Assert.IsTrue(IsExcluded);
        }

        [Test]
        public void CheckExclusionList_IsExcluded2()
        {
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            bool IsExcluded = catalogue.CheckExclusionList("9999-charge");

            Assert.IsTrue(IsExcluded);
        }

        [Test]
        public void CheckExclusionList_IsNOTExcluded1()
        {
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            bool IsExcluded = catalogue.CheckExclusionList("1234-testCharge");

            Assert.IsFalse(IsExcluded);
        }

        [Test]
        public void CheckExclusionList_IsNOTExcluded2()
        {
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            bool IsExcluded = catalogue.CheckExclusionList("789-asdfjaskjldf");

            Assert.IsFalse(IsExcluded);
        }


        #endregion

        #region GetCompatibleParts

        [Test]
        public void GetCompatibleParts_IsExcluded_ReturnEmpty()
        {
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            IEnumerable<PartSummary> compatibleParts = catalogue.GetCompatibleParts("1111-Invoice");

            Assert.AreEqual(0, compatibleParts.Count());
        }

        [Test]
        public void GetCompatibleParts_IsNOTExcluded_ReturnResults()
        {
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            IEnumerable<PartSummary> compatibleParts = catalogue.GetCompatibleParts("1234-teas");

            Assert.AreEqual(2, compatibleParts.Count());
        }

        [Test]
        public void GetCompatibleParts_InvalidPartNumber_ExceptionThrown()
        {
            PartCatalogue catalogue = new PartCatalogue(mockPartsService.Object);

            string invalidPartNumber = "123-jskldfj";

            Assert.Throws<InvalidPartException>(() => catalogue.GetCompatibleParts(invalidPartNumber));
        }

        #endregion

    }
}