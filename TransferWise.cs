using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TransferWise
{
    internal class TransferWise
    {




        //public class Amount
        //{
        //    public int? Value { get; set; }
        //    public string? Currency { get; set; }
        //}

        //public class CashAmount
        //{
        //    public int? Value { get; set; }
        //    public string? Currency { get; set; }
        //}

        //public class ReservedAmount
        //{
        //    public int? Value { get; set; }
        //    public string? Currency { get; set; }
        //}

        //public class Balance
        //{
        //    public int? Id { get; set; }
        //    public string? Currency { get; set; }
        //    public string? Type { get; set; }
        //    public string? Name { get; set; }
        //    public string? Icon { get; set; }
        //    public string? InvestmentState { get; set; }
        //    public Amount? Amount { get; set; }
        //    public ReservedAmount? ReservedAmount { get; set; }
        //    public CashAmount? CashAmount { get; set; }
        //    public TotalWorth? TotalWorth { get; set; }
        //    public DateTime? CreationTime { get; set; }
        //    public DateTime? ModificationTime { get; set; }
        //    public bool? Visible { get; set; }
        //}

        //public class TotalWorth
        //{
        //    public int? Value { get; set; }
        //    public string? Currency { get; set; }
        //}



        //public class Details
        //{
        //    public string? FirstName { get; set; }
        //    public string? LastName { get; set; }
        //    public string? DateOfBirth { get; set; }
        //    //public DateTime? DateOfBirth { get; set; }
        //    public string? PhoneNumber { get; set; }
        //    public string? Avatar { get; set; }
        //    public string? Occupation { get; set; }
        //    public string? Occupations { get; set; }
        //    public int? PrimaryAddress { get; set; }
        //    public string? FirstNameInKana { get; set; }
        //    public string? LastNameInKana { get; set; }
        //}

        //public class PersonalProfileee
        //{
        //    public int? Id { get; set; }
        //    public string? Type { get; set; }
        //    public Details Details { get; set; }
        //}





        public class Address
        {
            public string? AddressFirstLine { get; set; }
            public string? City { get; set; }
            public string? CountryIso2Code { get; set; }
            public string? CountryIso3Code { get; set; }
            public string? PostCode { get; set; }
            public object? StateCode { get; set; }
        }

        public class PlaceOfBirth
        {
            public string? RawValue { get; set; }
            public object? City { get; set; }
            public object? CountryIso2Code { get; set; }
            public object? CountryIso3Code { get; set; }
            public object? PostCode { get; set; }
            public object? StateCode { get; set; }
        }

        public class PersonalProfile
        {
            public string? Type { get; set; }
            public int? Id { get; set; }
            public int? UserId { get; set; }
            public Address? Address { get; set; }
            public string? Email { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public bool? Obfuscated { get; set; }
            public string? CurrentState { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? DateOfBirth { get; set; }
            public PlaceOfBirth? PlaceOfBirth { get; set; }
            public string? PhoneNumber { get; set; }
            public List<object>? SecondaryAddresses { get; set; }
            public string? FullName { get; set; }
            public List<Balances>? ListOfBalances { get; set; }
            
        }







        public class Amount
        {
            public double? Value { get; set; }
            public string? Currency { get; set; }
        }

        public class CashAmount
        {
            public double? Value { get; set; }
            public string? Currency { get; set; }
        }

        public class ReservedAmount
        {
            public double? Value { get; set; }
            public string? Currency { get; set; }
        }

        public class Balances
        {
            public int? Id { get; set; }
            public string? Currency { get; set; }
            public Amount? Amount { get; set; }
            public ReservedAmount? ReservedAmount { get; set; }
            public CashAmount? CashAmount { get; set; }
            public TotalWorth? TotalWorth { get; set; }
            public string? Type { get; set; }
            public string? TypeOfAccount { get; set; } // STANDARD - SAVINGS
            public object? Name { get; set; }
            public object? Icon { get; set; }
            public string? InvestmentState { get; set; }
            public DateTime? CreationTime { get; set; }
            public DateTime? ModificationTime { get; set; }
            public bool? Visible { get; set; }
            public bool? Primary { get; set; }
            public object? GroupId { get; set; }
            public BalanceStatement.BalanceOfStatement? BalanceOfStatement { get; set; }    
        }

        public class TotalWorth
        {
            public double? Value { get; set; }
            public string? Currency { get; set; }
        }




    }
}
