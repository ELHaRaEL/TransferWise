using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TransferWise.IResponse;

namespace TransferWise
{
    internal class BalanceStatement
    {


        public class BalanceOfStatement : IGetResponse
        {
            public AccountHolder? AccountHolder { get; set; }
            public Issuer? Issuer { get; set; }
            public List<object>? BankDetails { get; set; }
            public List<Transaction>? Transactions { get; set; }
            public StartOfStatementBalance? StartOfStatementBalance { get; set; }
            public EndOfStatementBalance? EndOfStatementBalance { get; set; }
            public object? EndOfStatementUnrealisedGainLoss { get; set; }
            public object? BalanceAssetConfiguration { get; set; }
            public Query? Query { get; set; }
            public Request? Request { get; set; }
        }

        public class AccountHolder
        {
            public string? Type { get; set; }
            public Address? Address { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
        }

        public class Address
        {
            public string? AddressFirstLine { get; set; }
            public string? City { get; set; }
            public string? PostCode { get; set; }
            public object? StateCode { get; set; }
            public string? CountryName { get; set; }
        }

        public class Amount
        {
            public double? Value { get; set; }
            public string? Currency { get; set; }
            public bool? Zero { get; set; }
        }

        public class Details
        {
            public string? Type { get; set; }
            public string? Description { get; set; }
        }

        public class EndOfStatementBalance
        {
            public double? Value { get; set; }
            public string? Currency { get; set; }
            public bool? Zero { get; set; }
        }

        public class Issuer
        {
            public string? Name { get; set; }
            public string? FirstLine { get; set; }
            public string? City { get; set; }
            public string? PostCode { get; set; }
            public object? StateCode { get; set; }
            public string? CountryCode { get; set; }
            public string? Country { get; set; }
        }

        public class Query
        {
            public DateTime? IntervalStart { get; set; }
            public DateTime? IntervalEnd { get; set; }
            public string? Type { get; set; }
            public bool? AddStamp { get; set; }
            public string? Currency { get; set; }
            public int? ProfileId { get; set; }
            public string? Timezone { get; set; }
        }

        public class Request
        {
            public string? Id { get; set; }
            public string? CreationTime { get; set; }
            public int? ProfileId { get; set; }
            public string? Currency { get; set; }
            public int? BalanceId { get; set; }
            public object? BalanceName { get; set; }
            public DateTime? IntervalStart { get; set; }
            public DateTime? IntervalEnd { get; set; }
        }
        public class RunningBalance
        {
            public double? Value { get; set; }
            public string? Currency { get; set; }
            public bool? Zero { get; set; }
        }

        public class StartOfStatementBalance
        {
            public double? Value { get; set; }
            public string? Currency { get; set; }
            public bool? Zero { get; set; }
        }

        public class TotalFees
        {
            public double? Value { get; set; }
            public string? Currency { get; set; }
            public bool? Zero { get; set; }
        }

        public class Transaction
        {
            public string? Type { get; set; }
            public DateTime? Date { get; set; }
            public Amount? Amount { get; set; }
            public TotalFees? TotalFees { get; set; }
            public Details? Details { get; set; }
            public object? ExchangeDetails { get; set; }
            public RunningBalance? RunningBalance { get; set; }
            public string? ReferenceNumber { get; set; }
            public object? Attachment { get; set; }
            public List<object>? ActivityAssetAttributions { get; set; }
        }

    }
}
