using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static TransferWise.IResponse;
using static TransferWise.TransferWise;

namespace TransferWise
{
    internal class WiseService
    {
        private static readonly HttpClient clientWiseAPI = new();
        private static readonly WiseRSA wiseRSA = new();
        private List<PersonalProfile> personalProfiles = new();

        private readonly string baseURL = "https://api.transferwise.com";

        private readonly string apiKey = string.Empty;
        private readonly string privateKeyPath = "Keys/private.pem";


        public async Task GetStatementOfAllBalances(DateTime? intervalStart = null, DateTime? intervalEnd = null, Enums.TypeOfGenerateStatement typeOfGenerateStatement = Enums.TypeOfGenerateStatement.json, Enums.TypeOfRequestStatement typeOfRequestStatement = Enums.TypeOfRequestStatement.COMPACT)
        {
            if (personalProfiles != null)
            {
                string statementString = typeOfGenerateStatement.ToString();
                string intervalStartString = intervalStart.GetValueOrDefault(DateTime.UtcNow.AddMonths(-1)).ToString("yyyy-MM-ddTHH:mm:ssZ");
                string intervalEndString = intervalEnd.GetValueOrDefault(DateTime.UtcNow).ToString("yyyy-MM-ddTHH:mm:ssZ");
                string typeOfRequestStatementString = typeOfRequestStatement.ToString();
                foreach (PersonalProfile personalProfile in personalProfiles)
                {
                    if (personalProfile.ListOfBalances != null)
                    {
                        foreach (Balances balances in personalProfile.ListOfBalances)
                        {
                            string url = "/v1/profiles/" + personalProfile.Id + "/balance-statements/" + balances.Id + "/statement." + statementString + "?currency=" + balances.Currency + "&intervalStart=" + intervalStartString + "&intervalEnd=" + intervalEndString + "&type=" + typeOfRequestStatementString;
                            HttpResponseMessage response = await clientWiseAPI.GetAsync(url);
                            if (response != null)
                            {
                                string x2faApproval = response.Headers.GetValues("x-2fa-approval").First();
                                BalanceStatement.BalanceOfStatement balanceOfStatement = await SendGetRequestWithSignedOTT<BalanceStatement.BalanceOfStatement>(url, x2faApproval);
                                personalProfile.ListOfBalances[personalProfile.ListOfBalances.IndexOf(balances)].BalanceOfStatement = balanceOfStatement;
                            }
                        }
                        PrintInformationAboutStatementOfProfile(personalProfile);
                    }
                }
            }
        }


     

     
        public async Task GetProfilesAndInformationAboutBalances()
        {
            personalProfiles.Clear();
            personalProfiles = await GetProfiles();
            if (personalProfiles.Count != 0)
            {
                foreach (PersonalProfile personalProfile in personalProfiles)
                {
                    if (personalProfile.Id != null)
                    {
                        personalProfiles[personalProfiles.IndexOf(personalProfile)].ListOfBalances = new List<Balances>() { };
                        foreach (Enums.TypeOfAccount typeOfAccount in Enum.GetValues(typeof(Enums.TypeOfAccount)))
                        {
                            List<Balances> listOfBalances = await GetBalancesOfProfiles((int)personalProfile.Id, typeOfAccount);
                            foreach (Balances balances in listOfBalances)
                            {
                                personalProfiles[personalProfiles.IndexOf(personalProfile)].ListOfBalances!.Add(balances);
                            }
                        }
                        PrintInformationAboutBalances(personalProfile);
                    }
                }
            }
        }


        private static async Task<List<PersonalProfile>> GetProfiles()
        {
            List<PersonalProfile>? personalProfiles = await clientWiseAPI.GetFromJsonAsync<List<PersonalProfile>>("/v2/profiles");
            return personalProfiles ?? new List<PersonalProfile>();
        }


        private static async Task<List<Balances>> GetBalancesOfProfiles(int profileID, Enums.TypeOfAccount typeOfAccount)
        {
            List<Balances>? balances = await clientWiseAPI.GetFromJsonAsync<List<Balances>>("/v4/profiles/" + profileID + "/balances?types=" + typeOfAccount.ToString());
            return balances ?? new List<Balances>();
        }

    
        private static void PrintInformationAboutBalances(PersonalProfile personalProfile)
        {
            Console.WriteLine("==============================================================================");
            Console.WriteLine("Account: {0,-12} ID: {1,-12}", personalProfile.Type, personalProfile.Id);
            Console.WriteLine(personalProfile.FullName);
            if (personalProfile.Address != null)
            {
                Console.WriteLine("{0,-4}{1} {2} {3}", personalProfile.Address.CountryIso2Code, personalProfile.Address.City, personalProfile.Address.PostCode, personalProfile.Address.AddressFirstLine);
            }
            Console.WriteLine(personalProfile.PhoneNumber);
            if (personalProfile.ListOfBalances != null)
            {
                Console.Write("({0}) Balances in curriencies: ", personalProfile.ListOfBalances.Count);
                foreach (Balances balances in personalProfile.ListOfBalances)
                {
                    Console.Write(" {0}", balances.Currency);
                }
                Console.WriteLine();
            }
        }
        private static void PrintInformationAboutStatementOfProfile(PersonalProfile personalProfile)
        {
            Console.WriteLine("==============================================================================");
            Console.WriteLine("Account: {0,-12} ID: {1,-12}", personalProfile.Type, personalProfile.Id);
            Console.WriteLine(personalProfile.FullName);
            if (personalProfile.Address != null)
            {
                Console.WriteLine("{0,-4}{1} {2} {3}", personalProfile.Address.CountryIso2Code, personalProfile.Address.City, personalProfile.Address.PostCode, personalProfile.Address.AddressFirstLine);
            }
            Console.WriteLine(personalProfile.PhoneNumber);
            if (personalProfile.ListOfBalances != null)
            {
                Console.Write("({0}) Balances in curriencies: ", personalProfile.ListOfBalances.Count);
                foreach (Balances balances in personalProfile.ListOfBalances)
                {
                    Console.Write(" {0}", balances.Currency);
                }

                foreach (Balances balances in personalProfile.ListOfBalances)
                {
                    if (balances.BalanceOfStatement != null)
                    {
                        Console.WriteLine("\n==============================================================================");
                        Console.WriteLine("Balance ID: {0}           Creation time: {1}", balances.BalanceOfStatement.Request!.BalanceId, balances.BalanceOfStatement.Request.CreationTime);
                        Console.WriteLine("Currency:  {0}\n", balances.BalanceOfStatement.Request.Currency);
                        Console.WriteLine("{0,20}{1,30}", "Interval Start", "Interval End");
                        Console.WriteLine("{0,20}{1,30}", balances.BalanceOfStatement.Request.IntervalStart.ToString(), balances.BalanceOfStatement.Request.IntervalEnd.ToString());
                        Console.WriteLine("{0,20}{1,30}", "Statement", "Statement");
                        Console.WriteLine("{0,16} {1,3}{2,26} {3,3}", balances.BalanceOfStatement.StartOfStatementBalance!.Value, balances.BalanceOfStatement.StartOfStatementBalance.Currency, balances.BalanceOfStatement.EndOfStatementBalance!.Value, balances.BalanceOfStatement.EndOfStatementBalance.Currency);
                        Console.WriteLine("\nNumber of transactions in the period: {0}", balances.BalanceOfStatement.Transactions!.Count);
                        Console.WriteLine("==========================================");

                        for (int i = 0; i < balances.BalanceOfStatement.Transactions.Count; i++)
                        {
                            Console.WriteLine("[{0}/{1}]     {2}    {3}\n", (i + 1), balances.BalanceOfStatement.Transactions.Count, balances.BalanceOfStatement.Transactions[i].Date.ToString(), balances.BalanceOfStatement.Transactions[i].ReferenceNumber);
                            Console.WriteLine("{0,10}{1,10}", "Amount", "Fee");
                            Console.WriteLine("{0,10}{1,10}{2,5}", balances.BalanceOfStatement.Transactions[i].Amount!.Value, balances.BalanceOfStatement.Transactions[i].TotalFees!.Value, balances.BalanceOfStatement.Transactions[i].Amount!.Currency);
                            Console.WriteLine("\nDescription: {0}", balances.BalanceOfStatement.Transactions[i].Details!.Description);
                            Console.WriteLine("Type: {0}", balances.BalanceOfStatement.Transactions[i].Details!.Type);
                            Console.WriteLine("\nSummary: {0} {1}", balances.BalanceOfStatement.Transactions[i].RunningBalance!.Value, balances.BalanceOfStatement.Transactions[i].RunningBalance!.Currency);
                        }

                    }
                }


            }
        }

        private static async Task<T> SendGetRequestWithSignedOTT<T>(string url, string x2faApproval) where T : IGetResponse, new()
        {
            clientWiseAPI.DefaultRequestHeaders.Add("x-2fa-approval", x2faApproval);
            string signedOneTimeToken = SignOTT(x2faApproval);
            clientWiseAPI.DefaultRequestHeaders.Add("X-Signature", signedOneTimeToken);
            T? t = await clientWiseAPI.GetFromJsonAsync<T>(url);
            clientWiseAPI.DefaultRequestHeaders.Remove("x-2fa-approval");
            clientWiseAPI.DefaultRequestHeaders.Remove("X-Signature");
            return t ?? new T();
        }


        private void SetUserAgent(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
            httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }


        private static string SignOTT(string oneTimeTokenToSing)
        {
            return wiseRSA.SignData(oneTimeTokenToSing);
        }


        private class WiseArgs
        {
            private string privateKeyPath = "Keys/private.pem";
            private string apiKey = string.Empty;
            public string PrivateKeyPath { get { return privateKeyPath; } set { privateKeyPath = value; } }
            public string ApiKey { get { return apiKey; } set { apiKey = value; } }

        }

        public WiseService()
        {
            string wiseJson = File.ReadAllText("Wise.json");
            WiseArgs? _1 = JsonSerializer.Deserialize<WiseArgs>(wiseJson);
            if (_1 != null)
            {
                apiKey = _1.ApiKey;
                privateKeyPath = _1.PrivateKeyPath;
                _1 = null;
            }
            SetUserAgent(clientWiseAPI);
            wiseRSA.SetRSAImportPrivateKey(privateKeyPath);
            clientWiseAPI.BaseAddress = new Uri(baseURL);

        }

    }
}
