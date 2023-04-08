import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AlertifyService } from "./Services/alertify.service";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { TranslateLoader, TranslateModule } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import {
    HttpClient,
    HttpClientModule,
    HTTP_INTERCEPTORS,
} from "@angular/common/http";
import { CountryListComponent } from "./Definitions/Country/country-list/country-list.component";
import { CountryAddUpdateComponent } from "./Definitions/Country/country-add-update/country-add-update.component";
import { NavBarComponent } from "./Layout/nav-bar/nav-bar.component";
import { SideBarComponent } from "./Layout/side-bar/side-bar.component";
import { CountryService } from "./Services/country.service";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ModalModule } from "ngx-bootstrap/modal";
import { ModalComponent } from "./CustomTemplates/modal/modal.component";
import { HomeComponent } from "./home/home.component";
import { NotFoundComponent } from "./not-found/not-found.component";
import { TranslationService } from "./Services/translation.service";
import { PaginationComponent } from "./CustomTemplates/pagination/pagination.component";
import { PaginationModule } from "ngx-bootstrap/pagination";
import { CityListComponent } from "./Definitions/City/city-list/city-list.component";
import { CityAddUpdateComponent } from "./Definitions/City/city-add-update/city-add-update.component";
import { CityService } from "./Services/city.service";
import { EmployeeAddUpdateComponent } from "./Definitions/Employee/employee-add-update/employee-add-update.component";
import { EmployeeListComponent } from "./Definitions/Employee/employee-list/employee-list.component";
import { OwnerListComponent } from "./Definitions/Owner/owner-list/owner-list.component";
import { OwnerAddUpdateComponent } from "./Definitions/Owner/owner-add-update/owner-add-update.component";
import { DistrictAddUpdateComponent } from "./Definitions/District/district-add-update/district-add-update.component";
import { DistrictListComponent } from "./Definitions/District/district-list/district-list.component";
import { EmployeeService } from "./Services/employee.service";
import { DistrictService } from "./Services/district.service";
import { OwnerService } from "./Services/owner.service";
import { ListComponent } from "./CustomTemplates/list/list.component";
import { BuildingAddUpdateComponent } from "./Definitions/Building/building-add-update/building-add-update.component";
import { BuildingListComponent } from "./Definitions/Building/building-list/building-list.component";
import { BuildingService } from "./Services/building.service";
import { CommonService } from "./Services/common.service";
import { ButtonsModule } from "ngx-bootstrap/buttons";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { CustomCollapseComponent } from "./CustomTemplates/custom-collapse/custom-collapse.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { CollapseButtonComponent } from "./CustomTemplates/collapse-button/collapse-button.component";
import { BuildingContributerAddUpdateComponent } from "./Definitions/Building/building-contributer-add-update/building-contributer-add-update.component";
import { BsDatepickerModule } from "ngx-bootstrap/datepicker";
import { UnitListComponent } from "./Definitions/Unit/unit-list/unit-list.component";
import { UnitAddUpdateComponent } from "./Definitions/Unit/unit-add-update/unit-add-update.component";
import { RenterAddUpdateComponent } from "./Definitions/Renter/renter-add-update/renter-add-update.component";
import { RenterListComponent } from "./Definitions/Renter/renter-list/renter-list.component";
import { UnitService } from "./Services/unit.service";
import { RenterService } from "./Services/renter.service";
import { NationalityListComponent } from "./Definitions/Nationality/nationality-list/nationality-list.component";
import { NationalityAddUpdateComponent } from "./Definitions/Nationality/nationality-add-update/nationality-add-update.component";
import { RenterContactPersonAddUpdateComponent } from "./Definitions/Renter/renter-contact-person-add-update/renter-contact-person-add-update.component";
import { ContributerListComponent } from "./Definitions/Contributer/contributer-list/contributer-list.component";
import { ContributerAddUpdateComponent } from "./Definitions/Contributer/contributer-add-update/contributer-add-update.component";
import { OwnerContractDetailsComponent } from "./Contracts/OwnerContract/owner-contract-details/owner-contract-details.component";
import { OwnerContractListComponent } from "./Contracts/OwnerContract/owner-contract-list/owner-contract-list.component";
import { RenterContractListComponent } from "./Contracts/RnterContract/renter-contract-list/renter-contract-list.component";
import { RenterContractDetailsComponent } from "./Contracts/RnterContract/renter-contract-details/renter-contract-details.component";
import { AccountListComponent } from "./Contracts/Account/account-list/account-list.component";
import { AccountDetailsComponent } from "./Contracts/Account/account-details/account-details.component";
import { AccountSetupDetailsComponent } from "./Contracts/AccountSetup/account-setup-details/account-setup-details.component";
import { DecimalNumberInputComponent } from "./CustomTemplates/decimal-number-input/decimal-number-input.component";
import { IntegerNumberInputComponent } from "./CustomTemplates/integer-number-input/integer-number-input.component";
import { GridPageComponent } from "./CustomTemplates/grid-page/grid-page.component";
import { FooterComponent } from "./Layout/footer/footer.component";
import { PageHeaderComponent } from "./Layout/page-header/page-header.component";
import { BreadcrumbService } from "./Services/breadcrumb.service";
import { LoginComponent } from "./Auth/login/login.component";
import { ExpenseListComponent } from "./Definitions/Expense/expense-list/expense-list.component";
import { ExpenseDetailsComponent } from "./Definitions/Expense/expense-details/expense-details.component";
import { ContractAdditionsComponent } from "./Contracts/RnterContract/contract-additions/contract-additions.component";
import { LoaderComponent } from "./Shared/loader/loader.component";
import { LoaderService } from "./Services/loader.service";
import { LoaderInterceptor } from "./Interceptors/loader.interceptor";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { NgSelectModule } from "@ng-select/ng-select";
import { ReceiptListComponent } from "./Vouchers/Receipt/receipt-list/receipt-list.component";
import { ReceiptDetailsComponent } from "./Vouchers/Receipt/receipt-details/receipt-details.component";
import { ExchangeDetailsComponent } from "./Vouchers/Exchange/exchange-details/exchange-details.component";
import { ExchangeListComponent } from "./Vouchers/Exchange/exchange-list/exchange-list.component";
import { InstallmentsComponent } from "./Vouchers/installments/installments.component";
import { RegisterComponent } from "./Auth/register/register.component";
import { AlertModule } from "ngx-alerts";
import { AuthHeaderInterceptor } from "./Interceptors/auth-header.interceptor";
import { UnauthorizedComponent } from "./unauthorized/unauthorized.component";
import { ContractViewComponent } from "./Contracts/RnterContract/contract-view/contract-view.component";
import { OwnerContractViewComponent } from "./Contracts/OwnerContract/owner-contract-view/owner-contract-view.component";
import { ConfirmDialogComponent } from "./Shared/confirm-dialog/confirm-dialog.component";
import { DialogService } from "./Services/dialog.service";
import { MatDialogModule } from "@angular/material/dialog";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { ActiveRentersComponent } from "./Reports/active-renters/active-renters.component";
import { BuildingRevenueExpenseComponent } from "./Reports/building-revenue-expense/building-revenue-expense.component";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatNativeDateModule } from "@angular/material/core";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";

import { RenterDueInstallmentsComponent } from "./Reports/renter-due-installments/renter-due-installments.component";
import { OwnerDueInstallmentsComponent } from "./Reports/owner-due-installments/owner-due-installments.component";
import { AccountBalanceComponent } from "./Reports/account-balance/account-balance.component";
import { ExchangeViewComponent } from "./Vouchers/Exchange/exchange-view/exchange-view.component";
import { ReceiptViewComponent } from "./Vouchers/Receipt/receipt-view/receipt-view.component";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import { MatMenuModule } from "@angular/material/menu";
import { MatDividerModule } from "@angular/material/divider";
import { MatTreeModule } from "@angular/material/tree";
import { MatTableModule } from "@angular/material/table";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { ContractAddionsListComponent } from "./Contracts/ContractAddions/contract-addions-list/contract-addions-list.component";
import { ContractAddionsDetailsComponent } from "./Contracts/ContractAddions/contract-addions-details/contract-addions-details.component";
import { MatRadioModule } from "@angular/material/radio";
import { CashBankListComponent } from "./Vouchers/CashBank/cash-bank-list/cash-bank-list.component";
import { CashBankDetailsComponent } from "./Vouchers/CashBank/cash-bank-details/cash-bank-details.component";
import { VoucherListComponent } from "./Vouchers/Voucher/voucher-list/voucher-list.component";
import { VoucherDetailsComponent } from "./Vouchers/Voucher/voucher-details/voucher-details.component";
import { VoucherViewComponent } from "./Vouchers/Voucher/voucher-view/voucher-view.component";
import { AccountTransactionComponent } from "./Reports/account-transaction/account-transaction.component";
import { RenterTransactionComponent } from "./Reports/renter-transaction/renter-transaction.component";
import { OwnerTransactionComponent } from "./Reports/owner-transaction/owner-transaction.component";
import { BuildingTransactionComponent } from "./Reports/building-transaction/building-transaction.component";
import { UnitTransactionComponent } from "./Reports/unit-transaction/unit-transaction.component";
import { UnitBalanceComponent } from "./Reports/unit-balance/unit-balance.component";
import { BuildingBalanceComponent } from "./Reports/building-balance/building-balance.component";
import { OwnerBalanceComponent } from "./Reports/owner-balance/owner-balance.component";
import { RenterBalanceComponent } from "./Reports/renter-balance/renter-balance.component";
import { CashBankBalanceComponent } from "./Reports/cash-bank-balance/cash-bank-balance.component";
import { CashBankTransactionComponent } from "./Reports/cash-bank-transaction/cash-bank-transaction.component";
import { UnitCurrentComponent } from "./Reports/unit-current/unit-current.component";
import { UnitAvailableComponent } from "./Reports/unit-available/unit-available.component";
import { UserListComponent } from "./Auth/user-list/user-list.component";
import { ChangePasswordComponent } from './Auth/change-password/change-password.component';

@NgModule({
    declarations: [
        AppComponent,
        CountryListComponent,
        CountryAddUpdateComponent,
        NavBarComponent,
        SideBarComponent,
        ModalComponent,
        HomeComponent,
        NotFoundComponent,
        PaginationComponent,
        CityListComponent,
        CityAddUpdateComponent,
        EmployeeAddUpdateComponent,
        EmployeeListComponent,
        OwnerListComponent,
        OwnerAddUpdateComponent,
        DistrictAddUpdateComponent,
        DistrictListComponent,
        ListComponent,
        BuildingAddUpdateComponent,
        BuildingListComponent,
        CustomCollapseComponent,
        CollapseButtonComponent,
        BuildingContributerAddUpdateComponent,
        UnitListComponent,
        UnitAddUpdateComponent,
        RenterAddUpdateComponent,
        RenterListComponent,
        NationalityListComponent,
        NationalityAddUpdateComponent,
        RenterContactPersonAddUpdateComponent,
        ContributerListComponent,
        ContributerAddUpdateComponent,
        OwnerContractDetailsComponent,
        OwnerContractListComponent,
        RenterContractListComponent,
        RenterContractDetailsComponent,
        AccountListComponent,
        AccountDetailsComponent,
        AccountSetupDetailsComponent,
        DecimalNumberInputComponent,
        IntegerNumberInputComponent,
        GridPageComponent,
        FooterComponent,
        PageHeaderComponent,
        LoginComponent,
        ExpenseListComponent,
        ExpenseDetailsComponent,
        ContractAdditionsComponent,
        LoaderComponent,
        ReceiptListComponent,
        ReceiptDetailsComponent,
        ExchangeDetailsComponent,
        ExchangeListComponent,
        InstallmentsComponent,
        RegisterComponent,
        UnauthorizedComponent,
        ContractViewComponent,
        OwnerContractViewComponent,
        ConfirmDialogComponent,
        ActiveRentersComponent,
        BuildingRevenueExpenseComponent,

        RenterDueInstallmentsComponent,
        OwnerDueInstallmentsComponent,
        AccountBalanceComponent,
        ExchangeViewComponent,
        ReceiptViewComponent,
        ContractAddionsListComponent,
        ContractAddionsDetailsComponent,
        CashBankListComponent,
        CashBankDetailsComponent,
        VoucherListComponent,
        VoucherDetailsComponent,
        VoucherViewComponent,
        AccountTransactionComponent,
        RenterTransactionComponent,
        OwnerTransactionComponent,
        BuildingTransactionComponent,
        UnitTransactionComponent,
        UnitBalanceComponent,
        BuildingBalanceComponent,
        OwnerBalanceComponent,
        RenterBalanceComponent,
        CashBankBalanceComponent,
        CashBankTransactionComponent,
        UnitCurrentComponent,
        UnitAvailableComponent,
        UserListComponent,
        ChangePasswordComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: httpTranslateLoader,
                deps: [HttpClient],
            },
        }),
        FormsModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        PaginationModule.forRoot(),
        ButtonsModule.forRoot(),
        CollapseModule.forRoot(),
        BrowserAnimationsModule,
        BsDatepickerModule.forRoot(),
        MatProgressSpinnerModule,
        NgSelectModule,
        AlertModule.forRoot({
            maxMessages: 5,
            timeout: 5000,
            positionX: "right",
            positionY: "top",
        }),
        MatDialogModule,
        MatIconModule,
        MatButtonModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatFormFieldModule,
        MatInputModule,
        MatSlideToggleModule,
        MatMenuModule,
        MatDividerModule,
        MatTreeModule,
        MatTableModule,
        MatCheckboxModule,
        MatRadioModule,
    ],
    providers: [
        AlertifyService,
        TranslationService,
        CountryService,
        CityService,
        EmployeeService,
        DistrictService,
        OwnerService,
        BuildingService,
        CommonService,
        UnitService,
        RenterService,
        BreadcrumbService,
        LoaderService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: LoaderInterceptor,
            multi: true,
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthHeaderInterceptor,
            multi: true,
        },
        DialogService,
    ],
    bootstrap: [AppComponent],
    entryComponents: [ConfirmDialogComponent],
})
export class AppModule {}

export function httpTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, "./assets/i18n/", ".json");
}
