using TransferWise;

WiseService wiseService = new();
await wiseService.GetProfilesAndInformationAboutBalances();
await wiseService.GetStatementOfAllBalances();
Console.ReadKey();




