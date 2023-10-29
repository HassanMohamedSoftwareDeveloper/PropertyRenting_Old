import { Component, Input, OnInit } from "@angular/core";
import { AuthService } from "../../Services/auth.service";

@Component({
    selector: "app-side-bar",
    templateUrl: "./side-bar.component.html",
    styleUrls: ["./side-bar.component.css"],
})
export class SideBarComponent implements OnInit {
    @Input() sideNavStatus = false;
    isAdmin = false;
    constructor(private autService: AuthService) {}
    ngOnInit(): void {
        this.isAdmin = this.autService.IsAdmin();
    }
    definitionsList = [
        {
            number: 2,
            name: "SideBar.Countries",
            icon: "countries.png",
            link: "definitions/countries",
            activeOptionsExact: true,
        },
        {
            number: 3,
            name: "SideBar.Cities",
            icon: "cities.png",
            link: "definitions/cities",
            activeOptionsExact: true,
        },
        {
            number: 4,
            name: "SideBar.Districts",
            icon: "districts.png",
            link: "definitions/districts",
            activeOptionsExact: true,
        },
        {
            number: 5,
            name: "SideBar.Employees",
            icon: "Employees.png",
            link: "definitions/employees",
            activeOptionsExact: true,
        },
        {
            number: 6,
            name: "SideBar.Contributers",
            icon: "Contributers.png",
            link: "definitions/contributers",
            activeOptionsExact: true,
        },
        {
            number: 7,
            name: "SideBar.Buildings",
            icon: "buildings.png",
            link: "definitions/buildings",
            activeOptionsExact: false,
        },
        {
            number: 8,
            name: "SideBar.Units",
            icon: "units.png",
            link: "definitions/units",
            activeOptionsExact: false,
        },

        {
            number: 9,
            name: "SideBar.Renters",
            icon: "Renters.png",
            link: "definitions/renters",
            activeOptionsExact: false,
        },
        {
            number: 10,
            name: "SideBar.Nationalities",
            icon: "nationality.png",
            link: "definitions/nationalities",
            activeOptionsExact: true,
        },
        {
            number: 11,
            name: "SideBar.Owners",
            icon: "Owners.png",
            link: "definitions/owners",
            activeOptionsExact: true,
        },
    ];
    accountList = [
        {
            number: 12,
            name: "SideBar.Accounts",
            icon: "accounts.png",
            link: "accounts/accounts",
            activeOptionsExact: false,
        },
        {
            number: 13,
            name: "SideBar.AccountSetup",
            icon: "settings.png",
            link: "accounts/accountsetup",
            activeOptionsExact: false,
        },
        {
            number: 13,
            name: "SideBar.Expenses",
            icon: "expenses.png",
            link: "accounts/expenses",
            activeOptionsExact: false,
        },
    ];
    contractList = [
        {
            number: 1,
            name: "SideBar.ContractAdditions",
            icon: "ownercontract.png",
            link: "contracts/contract-additions",
            activeOptionsExact: false,
        },
        {
            number: 2,
            name: "SideBar.OwnerContracts",
            icon: "ownercontract.png",
            link: "contracts/ownercontracts",
            activeOptionsExact: false,
        },
        {
            number: 3,
            name: "SideBar.RenterContracts",
            icon: "rentercontract.png",
            link: "contracts/rentercontracts",
            activeOptionsExact: false,
        },
    ];
    voucherList = [
        {
            number: 1,
            name: "SideBar.CashBank",
            icon: "exchange.png",
            link: "financials/cash-bank",
            activeOptionsExact: false,
        },
        {
            number: 2,
            name: "SideBar.Exchanges",
            icon: "exchange.png",
            link: "financials/exchanges",
            activeOptionsExact: false,
        },
        {
            number: 3,
            name: "SideBar.Receipts",
            icon: "receipt.png",
            link: "financials/receipts",
            activeOptionsExact: false,
        },
        {
            number: 4,
            name: "SideBar.Vouchers",
            icon: "receipt.png",
            link: "financials/vouchers",
            activeOptionsExact: false,
        },
    ];
    reportList = [
        {
            number: 1,
            name: "SideBar.ActiveRenters",
            icon: "reports.png",
            link: "reports/active-renters",
            activeOptionsExact: false,
        },
        {
            number: 2,
            name: "SideBar.BuildingRevenueExpense",
            icon: "reports.png",
            link: "reports/building-revenue-expense",
            activeOptionsExact: false,
        },
        {
            number: 3,
            name: "SideBar.RenterDueInstallments",
            icon: "reports.png",
            link: "reports/renter-due-installments",
            activeOptionsExact: false,
        },
        {
            number: 4,
            name: "SideBar.OwnerDueInstallments",
            icon: "reports.png",
            link: "reports/owner-due-installments",
            activeOptionsExact: false,
        },
        {
            number: 5,
            name: "SideBar.AccountBalance",
            icon: "reports.png",
            link: "reports/account-balance",
            activeOptionsExact: false,
        },
        {
            number: 6,
            name: "SideBar.AccountTransactions",
            icon: "reports.png",
            link: "reports/account-transaction",
            activeOptionsExact: false,
        },
        {
            number: 7,
            name: "SideBar.RenterBalance",
            icon: "reports.png",
            link: "reports/renter-balance",
            activeOptionsExact: false,
        },
        {
            number: 8,
            name: "SideBar.RenterTransactions",
            icon: "reports.png",
            link: "reports/renter-transaction",
            activeOptionsExact: false,
        },
        {
            number: 9,
            name: "SideBar.OwnerBalance",
            icon: "reports.png",
            link: "reports/owner-balance",
            activeOptionsExact: false,
        },
        {
            number: 10,
            name: "SideBar.OwnerTransactions",
            icon: "reports.png",
            link: "reports/owner-transaction",
            activeOptionsExact: false,
        },
        {
            number: 11,
            name: "SideBar.BuildingBalance",
            icon: "reports.png",
            link: "reports/building-balance",
            activeOptionsExact: false,
        },
        {
            number: 12,
            name: "SideBar.BuildingTransactions",
            icon: "reports.png",
            link: "reports/building-transaction",
            activeOptionsExact: false,
        },
        {
            number: 13,
            name: "SideBar.UnitBalance",
            icon: "reports.png",
            link: "reports/unit-balance",
            activeOptionsExact: false,
        },
        {
            number: 14,
            name: "SideBar.UnitTransactions",
            icon: "reports.png",
            link: "reports/unit-transaction",
            activeOptionsExact: false,
        },
        {
            number: 15,
            name: "SideBar.CashBankBalance",
            icon: "reports.png",
            link: "reports/cash-bank-balance",
            activeOptionsExact: false,
        },
        {
            number: 16,
            name: "SideBar.CashBankTransactions",
            icon: "reports.png",
            link: "reports/cash-bank-transaction",
            activeOptionsExact: false,
        },
        {
            number: 17,
            name: "SideBar.CurrentUnits",
            icon: "reports.png",
            link: "reports/current-units",
            activeOptionsExact: false,
        },
        {
            number: 18,
            name: "SideBar.AvailableUnits",
            icon: "reports.png",
            link: "reports/available-units",
            activeOptionsExact: false,
        },
    ];
}
