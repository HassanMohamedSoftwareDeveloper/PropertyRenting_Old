import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { AccountBalance } from "../../Models/Reports/account-balance";
import { AccountService } from "../../Services/account.service";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";
import { AccountLookup } from "../../Models/account-lookup";

@Component({
    selector: "app-account-balance",
    templateUrl: "./account-balance.component.html",
    styleUrls: ["./account-balance.component.css"],
})
export class AccountBalanceComponent implements OnInit {
    data: AccountBalance[] = [];
    accounts: AccountLookup[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    filterForm!: FormGroup;
    showReport = false;

    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private accountService: AccountService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.AccountBalanceReportItems;

        this.createForm();
        this.loadAccounts();
        this.loadReport();
    }
    createForm() {
        this.filterForm = this.fb.group({
            AccountId: [null],
            FromDate: [null],
            ToDate: [null],
        });
    }

    loadAccounts() {
        this.accountService.GetLookup().subscribe(
            (res) => {
                this.accounts = res;
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get AccountId() {
        return this.filterForm.controls["AccountId"] as FormControl;
    }

    get FromDate() {
        return this.filterForm.controls["FromDate"] as FormControl;
    }
    get ToDate() {
        return this.filterForm.controls["ToDate"] as FormControl;
    }

    get MinDate() {
        return new Date(Date.parse(this.FromDate.value));
    }
    EmptyToDate(value: Date) {
        if (this.MinDate > value) {
            this.ToDate.setValue(null);
        }
    }

    loadReport() {
        this.showReport = false;
        this.reportService
            .GetAccountBalance(
                this.AccountId.value,
                this.FromDate.value,
                this.ToDate.value
            )
            .subscribe(
                (res) => {
                    this.data = res;
                    this.showReport = true;
                },
                (error) => {
                    const msg =
                        this.translateService.Translate("ErrorOccurred");
                    this.alertify.error(msg);
                    console.log(error);
                }
            );
    }
    onSubmit() {
        this.loadReport();
    }
    exportPDF() {
        this.reportService
            .ExportAccountBalance(
                "PDF",
                this.AccountId.value,
                this.FromDate.value,
                this.ToDate.value
            )
            .subscribe(
                (blob) => {
                    const today = new Date();
                    const pipe = new DatePipe("en-US");
                    const ChangedFormat = pipe.transform(
                        today,
                        "dd-MM-YYYY_HH-mm-ss"
                    );
                    const a = document.createElement("a");
                    const objectUrl = URL.createObjectURL(blob);
                    a.href = objectUrl;
                    a.download = "AccountBalance_" + ChangedFormat + ".pdf";
                    a.target = "_blank";
                    a.click();
                    URL.revokeObjectURL(objectUrl);
                },
                (error) => {
                    console.log(error);
                }
            );
    }
    exportExcel() {
        this.reportService
            .ExportAccountBalance(
                "EXCEL",
                this.AccountId.value,
                this.FromDate.value,
                this.ToDate.value
            )
            .subscribe(
                (blob) => {
                    const today = new Date();
                    const pipe = new DatePipe("en-US");
                    const ChangedFormat = pipe.transform(
                        today,
                        "dd-MM-YYYY_HH-mm-ss"
                    );
                    const a = document.createElement("a");
                    const objectUrl = URL.createObjectURL(blob);
                    a.href = objectUrl;
                    a.download = "AccountBalance_" + ChangedFormat + ".xls";
                    a.target = "_blank";
                    a.click();
                    URL.revokeObjectURL(objectUrl);
                },
                (error) => {
                    console.log(error);
                }
            );
    }
}
