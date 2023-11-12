import { Injectable } from "@angular/core";
import { Breadcrumb } from "../Models/breadcrumb";

@Injectable({
    providedIn: "root",
})
export class BreadcrumbService {
    HomepageItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: true },
    ];

    CountryListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Countries",
            url: "/definitions/countries",
            isActive: true,
        },
    ];

    CityListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Cities",
            url: "/definitions/cities",
            isActive: true,
        },
    ];

    DistrictListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Districts",
            url: "/definitions/districts",
            isActive: true,
        },
    ];

    OwnerListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Owners",
            url: "/definitions/owners",
            isActive: true,
        },
    ];

    EmployeeListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Employees",
            url: "/definitions/employees",
            isActive: true,
        },
    ];

    BuildingListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Buildings",
            url: "/definitions/buildings",
            isActive: true,
        },
    ];

    UnitListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Units",
            url: "/definitions/units",
            isActive: true,
        },
    ];

    RenterListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Renters",
            url: "/definitions/renters",
            isActive: true,
        },
    ];

    NationalityListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Nationalities",
            url: "/definitions/nationalities",
            isActive: true,
        },
    ];

    ContributerListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Contributers",
            url: "/definitions/contributers",
            isActive: true,
        },
    ];

    OwnerContractListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.OwnerContracts",
            url: "/contracts/ownerContracts",
            isActive: true,
        },
    ];

    RenterContractListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.RenterContracts",
            url: "/contracts/renterContracts",
            isActive: true,
        },
    ];

    AccountListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Accounts",
            url: "/accounts/accounts",
            isActive: true,
        },
    ];

    AccountSetupItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.AccountSetup",
            url: "/accounts/accountsetup",
            isActive: true,
        },
    ];

    OwnerContractDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.OwnerContracts",
            url: "/contracts/ownercontracts",
            isActive: false,
        },
        {
            displayName: "SideBar.OwnerContractDetails",
            url: "",
            isActive: true,
        },
    ];

    RenterContractDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.RenterContracts",
            url: "/contracts/rentercontracts",
            isActive: false,
        },
        {
            displayName: "SideBar.RenterContractDetails",
            url: "",
            isActive: true,
        },
    ];

    BuildingDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Buildings",
            url: "/definitions/buildings",
            isActive: false,
        },
        {
            displayName: "SideBar.BuildingDetails",
            url: "",
            isActive: true,
        },
    ];

    UnitDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Units",
            url: "/definitions/units",
            isActive: false,
        },
        {
            displayName: "SideBar.UnitDetails",
            url: "",
            isActive: true,
        },
    ];

    RenterDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Renters",
            url: "/definitions/renters",
            isActive: false,
        },
        {
            displayName: "SideBar.RenterDetails",
            url: "",
            isActive: true,
        },
    ];

    ExpenseListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Expenses",
            url: "",
            isActive: true,
        },
    ];

    ExpenseDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Expenses",
            url: "/accounts/expenses",
            isActive: false,
        },
        {
            displayName: "SideBar.ExpenseDetails",
            url: "",
            isActive: true,
        },
    ];

    ExchangeListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Exchanges",
            url: "",
            isActive: true,
        },
    ];

    ExchangeDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Exchanges",
            url: "/financials/exchanges",
            isActive: false,
        },
        {
            displayName: "SideBar.ExchangeDetails",
            url: "",
            isActive: true,
        },
    ];

    ReceiptListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Receipts",
            url: "",
            isActive: true,
        },
    ];

    ReceiptDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Receipts",
            url: "/financials/receipts",
            isActive: false,
        },
        {
            displayName: "SideBar.ReceiptDetails",
            url: "",
            isActive: true,
        },
    ];

    ActiveRenterReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.ActiveRenters",
            url: "",
            isActive: true,
        },
    ];

    BuildingRevenueExpenseReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.BuildingRevenueExpense",
            url: "",
            isActive: true,
        },
    ];

    RenterDueInstallmentsReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.RenterDueInstallments",
            url: "",
            isActive: true,
        },
    ];
    OwnerDueInstallmentsReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.OwnerDueInstallments",
            url: "",
            isActive: true,
        },
    ];

    ContractAdditionListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.ContractAdditions",
            url: "",
            isActive: true,
        },
    ];

    ContractAdditionDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.ContractAdditions",
            url: "/contracts/contract-additions",
            isActive: false,
        },
        {
            displayName: "SideBar.ContractAdditionsDetails",
            url: "",
            isActive: true,
        },
    ];

    CashBankListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.CashBank",
            url: "",
            isActive: true,
        },
    ];

    CashBankDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.CashBank",
            url: "/vouchers/cash-bank",
            isActive: false,
        },
        {
            displayName: "SideBar.CashBankDetails",
            url: "",
            isActive: true,
        },
    ];

    VoucherListItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Vouchers",
            url: "",
            isActive: true,
        },
    ];

    VoucherDetailsItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Vouchers",
            url: "/financials/vouchers",
            isActive: false,
        },
        {
            displayName: "SideBar.VoucherDetails",
            url: "",
            isActive: true,
        },
    ];

    AccountBalanceReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.AccountBalance",
            url: "",
            isActive: true,
        },
    ];
    AccountTransactionReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.AccountTransactions",
            url: "",
            isActive: true,
        },
    ];

    RenterBalanceReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.RenterBalance",
            url: "",
            isActive: true,
        },
    ];
    RenterTransactionReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.RenterTransactions",
            url: "",
            isActive: true,
        },
    ];

    OwnerBalanceReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.OwnerBalance",
            url: "",
            isActive: true,
        },
    ];
    OwnerTransactionReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.OwnerTransactions",
            url: "",
            isActive: true,
        },
    ];

    BuildingBalanceReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.BuildingBalance",
            url: "",
            isActive: true,
        },
    ];
    BuildingTransactionReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.BuildingTransactions",
            url: "",
            isActive: true,
        },
    ];

    UnitBalanceReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.UnitBalance",
            url: "",
            isActive: true,
        },
    ];
    UnitTransactionReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.UnitTransactions",
            url: "",
            isActive: true,
        },
    ];

    CashBankBalanceReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.CashBankBalance",
            url: "",
            isActive: true,
        },
    ];
    CashBankTransactionReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.CashBankTransactions",
            url: "",
            isActive: true,
        },
    ];

    CurrentUnitsReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.CurrentUnits",
            url: "",
            isActive: true,
        },
    ];
    AvailableUnitsReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.AvailableUnits",
            url: "",
            isActive: true,
        },
    ];
    ContributorBalanceReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.ContributorBalance",
            url: "",
            isActive: true,
        },
    ];
    ContributorTransactionReportItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.ContributorTransactions",
            url: "",
            isActive: true,
        },
    ];
    UsersItems: Breadcrumb[] = [
        { displayName: "SideBar.Home", url: "/", isActive: false },
        {
            displayName: "SideBar.Users",
            url: "",
            isActive: true,
        },
    ];
}
