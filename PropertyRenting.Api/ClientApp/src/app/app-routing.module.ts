import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ChangePasswordComponent } from "./Auth/change-password/change-password.component";
import { LoginComponent } from "./Auth/login/login.component";
import { RegisterComponent } from "./Auth/register/register.component";
import { UserListComponent } from "./Auth/user-list/user-list.component";
import { AccountListComponent } from "./Contracts/Account/account-list/account-list.component";
import { AccountSetupDetailsComponent } from "./Contracts/AccountSetup/account-setup-details/account-setup-details.component";
import { ContractAddionsDetailsComponent } from "./Contracts/ContractAddions/contract-addions-details/contract-addions-details.component";
import { ContractAddionsListComponent } from "./Contracts/ContractAddions/contract-addions-list/contract-addions-list.component";
import { OwnerContractDetailsComponent } from "./Contracts/OwnerContract/owner-contract-details/owner-contract-details.component";
import { OwnerContractListComponent } from "./Contracts/OwnerContract/owner-contract-list/owner-contract-list.component";
import { OwnerContractViewComponent } from "./Contracts/OwnerContract/owner-contract-view/owner-contract-view.component";
import { ContractViewComponent } from "./Contracts/RnterContract/contract-view/contract-view.component";
import { RenterContractDetailsComponent } from "./Contracts/RnterContract/renter-contract-details/renter-contract-details.component";
import { RenterContractListComponent } from "./Contracts/RnterContract/renter-contract-list/renter-contract-list.component";
import { BuildingAddUpdateComponent } from "./Definitions/Building/building-add-update/building-add-update.component";
import { BuildingListComponent } from "./Definitions/Building/building-list/building-list.component";
import { CityListComponent } from "./Definitions/City/city-list/city-list.component";
import { ContributerListComponent } from "./Definitions/Contributer/contributer-list/contributer-list.component";
import { CountryListComponent } from "./Definitions/Country/country-list/country-list.component";
import { DistrictListComponent } from "./Definitions/District/district-list/district-list.component";
import { EmployeeListComponent } from "./Definitions/Employee/employee-list/employee-list.component";
import { ExpenseDetailsComponent } from "./Definitions/Expense/expense-details/expense-details.component";
import { ExpenseListComponent } from "./Definitions/Expense/expense-list/expense-list.component";
import { NationalityListComponent } from "./Definitions/Nationality/nationality-list/nationality-list.component";
import { OwnerListComponent } from "./Definitions/Owner/owner-list/owner-list.component";
import { RenterAddUpdateComponent } from "./Definitions/Renter/renter-add-update/renter-add-update.component";
import { RenterListComponent } from "./Definitions/Renter/renter-list/renter-list.component";
import { UnitAddUpdateComponent } from "./Definitions/Unit/unit-add-update/unit-add-update.component";
import { UnitListComponent } from "./Definitions/Unit/unit-list/unit-list.component";
import { AdminGuard } from "./Guards/admin.guard";
import { AuthGuard } from "./Guards/auth.guard";
import { HomeComponent } from "./home/home.component";
import { NotFoundComponent } from "./not-found/not-found.component";

import { AccountBalanceComponent } from "./Reports/account-balance/account-balance.component";
import { AccountTransactionComponent } from "./Reports/account-transaction/account-transaction.component";
import { ActiveRentersComponent } from "./Reports/active-renters/active-renters.component";
import { BuildingBalanceComponent } from "./Reports/building-balance/building-balance.component";
import { BuildingRevenueExpenseComponent } from "./Reports/building-revenue-expense/building-revenue-expense.component";
import { BuildingTransactionComponent } from "./Reports/building-transaction/building-transaction.component";
import { CashBankBalanceComponent } from "./Reports/cash-bank-balance/cash-bank-balance.component";
import { CashBankTransactionComponent } from "./Reports/cash-bank-transaction/cash-bank-transaction.component";
import { OwnerBalanceComponent } from "./Reports/owner-balance/owner-balance.component";
import { OwnerDueInstallmentsComponent } from "./Reports/owner-due-installments/owner-due-installments.component";
import { OwnerTransactionComponent } from "./Reports/owner-transaction/owner-transaction.component";
import { RenterBalanceComponent } from "./Reports/renter-balance/renter-balance.component";
import { RenterDueInstallmentsComponent } from "./Reports/renter-due-installments/renter-due-installments.component";
import { RenterTransactionComponent } from "./Reports/renter-transaction/renter-transaction.component";
import { UnitAvailableComponent } from "./Reports/unit-available/unit-available.component";
import { UnitBalanceComponent } from "./Reports/unit-balance/unit-balance.component";
import { UnitCurrentComponent } from "./Reports/unit-current/unit-current.component";
import { UnitTransactionComponent } from "./Reports/unit-transaction/unit-transaction.component";

import { UnauthorizedComponent } from "./unauthorized/unauthorized.component";
import { CashBankDetailsComponent } from "./Vouchers/CashBank/cash-bank-details/cash-bank-details.component";
import { CashBankListComponent } from "./Vouchers/CashBank/cash-bank-list/cash-bank-list.component";
import { ExchangeDetailsComponent } from "./Vouchers/Exchange/exchange-details/exchange-details.component";
import { ExchangeListComponent } from "./Vouchers/Exchange/exchange-list/exchange-list.component";
import { ExchangeViewComponent } from "./Vouchers/Exchange/exchange-view/exchange-view.component";
import { ReceiptDetailsComponent } from "./Vouchers/Receipt/receipt-details/receipt-details.component";
import { ReceiptListComponent } from "./Vouchers/Receipt/receipt-list/receipt-list.component";
import { ReceiptViewComponent } from "./Vouchers/Receipt/receipt-view/receipt-view.component";
import { VoucherDetailsComponent } from "./Vouchers/Voucher/voucher-details/voucher-details.component";
import { VoucherListComponent } from "./Vouchers/Voucher/voucher-list/voucher-list.component";
import { VoucherViewComponent } from "./Vouchers/Voucher/voucher-view/voucher-view.component";
import { UpdateUserComponent } from "./Auth/update-user/update-user.component";
import { ResetUserPasswordComponent } from "./Auth/reset-user-password/reset-user-password.component";
import { ContributorBalanceComponent } from "./Reports/contributor-balance/contributor-balance.component";
import { ContributorTransactionComponent } from "./Reports/contributor-transaction/contributor-transaction.component";

const routes: Routes = [
    { path: "", component: HomeComponent, canActivate: [AuthGuard] },
    { path: "auth/login", component: LoginComponent },
    {
        path: "auth/change-password",
        component: ChangePasswordComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "auth/users",
        component: UserListComponent,
        canActivate: [AdminGuard],
    },
    {
        path: "auth/users/add",
        component: RegisterComponent,
        canActivate: [AdminGuard],
    },
    {
        path: "auth/users/edit/:id",
        component: UpdateUserComponent,
        canActivate: [AdminGuard],
    },
    {
        path: "auth/users/reset-password/:id",
        component: ResetUserPasswordComponent,
        canActivate: [AdminGuard],
    },
    { path: "not-authorized", component: UnauthorizedComponent },
    {
        path: "definitions/countries",
        component: CountryListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/cities",
        component: CityListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/owners",
        component: OwnerListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/contributers",
        component: ContributerListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/districts",
        component: DistrictListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/employees",
        component: EmployeeListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/buildings",
        component: BuildingListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/buildings/add",
        component: BuildingAddUpdateComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/buildings/edit/:id",
        component: BuildingAddUpdateComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/units",
        component: UnitListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/units/add",
        component: UnitAddUpdateComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/units/edit/:id",
        component: UnitAddUpdateComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/renters",
        component: RenterListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/renters/add",
        component: RenterAddUpdateComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/renters/edit/:id",
        component: RenterAddUpdateComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "definitions/nationalities",
        component: NationalityListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/ownercontracts",
        component: OwnerContractListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/ownercontracts/add",
        component: OwnerContractDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/ownercontracts/edit/:id",
        component: OwnerContractDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/ownercontracts/details/:id",
        component: OwnerContractViewComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/rentercontracts",
        component: RenterContractListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/rentercontracts/add",
        component: RenterContractDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/rentercontracts/edit/:id",
        component: RenterContractDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/rentercontracts/details/:id",
        component: ContractViewComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/contract-additions",
        component: ContractAddionsListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/contract-additions/add",
        component: ContractAddionsDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "contracts/contract-additions/edit/:id",
        component: ContractAddionsDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "accounts/accounts",
        component: AccountListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "accounts/accountsetup",
        component: AccountSetupDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "accounts/expenses",
        component: ExpenseListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "accounts/expenses/add",
        component: ExpenseDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "accounts/expenses/edit/:id",
        component: ExpenseDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/receipts",
        component: ReceiptListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/receipt/add",
        component: ReceiptDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/receipt/edit/:id",
        component: ReceiptDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/receipt/details/:id",
        component: ReceiptViewComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/exchanges",
        component: ExchangeListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/exchange/add",
        component: ExchangeDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/exchange/edit/:id",
        component: ExchangeDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/exchange/details/:id",
        component: ExchangeViewComponent,
        canActivate: [AuthGuard],
    },

    {
        path: "financials/vouchers",
        component: VoucherListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/voucher/add",
        component: VoucherDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/voucher/edit/:id",
        component: VoucherDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/voucher/details/:id",
        component: VoucherViewComponent,
        canActivate: [AuthGuard],
    },

    {
        path: "financials/cash-bank",
        component: CashBankListComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/cash-bank/add",
        component: CashBankDetailsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "financials/cash-bank/edit/:id",
        component: CashBankDetailsComponent,
        canActivate: [AuthGuard],
    },

    {
        path: "reports/active-renters",
        component: ActiveRentersComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/building-revenue-expense",
        component: BuildingRevenueExpenseComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/renter-due-installments",
        component: RenterDueInstallmentsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/owner-due-installments",
        component: OwnerDueInstallmentsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/account-balance",
        component: AccountBalanceComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/account-transaction",
        component: AccountTransactionComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/renter-balance",
        component: RenterBalanceComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/renter-transaction",
        component: RenterTransactionComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/owner-balance",
        component: OwnerBalanceComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/owner-transaction",
        component: OwnerTransactionComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/building-balance",
        component: BuildingBalanceComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/building-transaction",
        component: BuildingTransactionComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/unit-balance",
        component: UnitBalanceComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/unit-transaction",
        component: UnitTransactionComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/cash-bank-balance",
        component: CashBankBalanceComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/cash-bank-transaction",
        component: CashBankTransactionComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/current-units",
        component: UnitCurrentComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/available-units",
        component: UnitAvailableComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/contributor-balance",
        component: ContributorBalanceComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "reports/contributor-transaction",
        component: ContributorTransactionComponent,
        canActivate: [AuthGuard],
    },
    {
        path: "**",
        pathMatch: "full",
        component: NotFoundComponent,
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
