PartsTrader.ClientTools
===================================================================================================

Task Outline
--------------------------------------------------------------------------------------------------- 

The PartsTrader team are developing a set of tools for use in third party repairer estimating software. These tools will provide the ability for repairers to lookup a given part and find all compatible parts that could be used instead. These tools are to be provided to the clients in the form of a simple API that they can reference within their own solutions.

Background
---------------------------------------------------------------------------------------------------

PartsTrader maintains a large catalogue of vehicle parts providing mappings between various national and international standards. When repairers submit part information to PartsTrader, either when quoting or when simply looking up a part, PartsTrader will cache the part information (if it doesn't already know of the part) so as to be able to provide as full and complete a list of parts information as possible to other integrations.

When creating work estimates repairers create a list of the parts need to repair a vehicle; for example, a car may require a new front wheel and a side mirror in order to be road worthy again. In many scenarios car parts are interchangeable; for example, a rear tail light for a 2014 Chevrolet Camaro might be the officially required part for a repair however a Ford Focus 2008 tail light may be either the exact same part or a close enough replacement. In order to maximise the response to part quotes, and thus hopefully reduce costs, repairers want to be able to lookup the central PartsTrader catalogue of parts and retrieve a list of all parts that could be used instead.

In some estimating software, there is no distinction made between a part and a line item, as such an estimate may contain pseudo parts which incur a charge but should not be included in quote requests for parts. For example a repairer may include 1111-OilCheck on their list of parts to indicate that they will be charging for an oil check, however this should not be submitted to PartsTrader as it either contains repair shop operational specifics that PartsTrader shouldn't know about, or it is data that PartsTrader should not be storing in the central parts repository (as this is available to all integrated repairers). In order to prevent non-standard parts being provided to PartsTrader each repairer can maintain their own exclusions list which contains a set of parts that should not be sent through to PartsTrader; it is important that our client tools use this list to exclude parts that shouldn't be uploaded.

Task
---------------------------------------------------------------------------------------------------

A skeleton of the client tools project has been created containing the API exposed to the client and a stub implementation. Full requirements for the integrated PartsTrader Parts Service are not known at present so a simple interface has been provided to abstract the parts lookup. Some initial tests have been created but these do not currently pass. Your task is to complete the implementation such that it meets the following requirements.

### Requirement1 - Validate Part Number 

When given a part number the client tools should validate it to ensure that it conforms to the following specification: 

    partNumber = partId "-" partCode;
    partId     = {4 * digit};
    partCode   = {4 * alphanumeric}, {alphanumeric};

That is a part id comprising of 4 digits, followed by a dash (-), followed by a part code consisting of 4 or more alphanumeric characters. So, 1234-abcd, 1234-a1b2c3d4 would be valid, a234-abcd, 123-abcd would be invalid. Where an invalid number is found an invalid part exception should be thrown.

### Requirement2 - Check Exclusions List

Valid part numbers should be checked against the local exclusions list to determine whether the part should be supplied to PartsTrader or not. If the given part number is found in the list then the part should not be sent to PartsTrader; in this scenario, the lookup should return an empty collection.

### Requirement3 - Lookup Compatible Parts

If a valid part is supplied that is not on the exclusions list then it should be looked up via the PartsTrader Parts Service (represented by the IPartsTraderPartsService interface) in order to retrieve any compatible parts. The results of this lookup should be returned.

Assumptions
---------------------------------------------------------------------------------------------------

The following assumptions have been made in the provided framework - you are free to countermand these as you feel is appropriate to your design.

1. The exclusions list is a JSON file currently stored in the file /Exclusions.json (relative to the current execution directory).
2. Part numbers are not case sensitive.
3. It is sufficient to mock IPartsTraderPartsService for the purpose of testing.

Constraints
---------------------------------------------------------------------------------------------------

There are no constraints to your implemented solution, we have provided a skeleton framework to extend from however you are free to modify, remove, or ignore these according to what you perceive to be the best solution.
We recommend you to use Visual Studio 2015 Community Edition and the expected time to complete this test is up to 8hrs.