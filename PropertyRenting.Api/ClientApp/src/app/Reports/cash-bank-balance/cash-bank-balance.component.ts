import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { CashBankBalance } from "../../Models/Reports/cash-bank-balance";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { CashBankService } from "../../Services/cash-bank.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";
import { Lookup } from "../../Models/lookup";
import { AuthService } from "../../Services/auth.service";

@Component({
    selector: "app-cash-bank-balance",
    templateUrl: "./cash-bank-balance.component.html",
    styleUrls: ["./cash-bank-balance.component.css"],
})
export class CashBankBalanceComponent implements OnInit {
    data: CashBankBalance[] = [];
    cashBanks: Lookup[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    filterForm!: FormGroup;
    showReport = false;
    minDate: any = null;
    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private cashBankService: CashBankService,
        public authService: AuthService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.CashBankBalanceReportItems;

        this.createForm();
        this.loadCashBanks();
        this.loadReport();
    }
    createForm() {
        const date = new Date();
        date.setMonth(0, 1);
        if (this.authService.IsSubAdmin()) {
            this.minDate = date;
        }
        this.filterForm = this.fb.group({
            CashBankId: [null],
            FromDate: [date],
            ToDate: [null],
        });
    }

    loadCashBanks() {
        this.cashBankService.GetLookup().subscribe(
            (res) => {
                this.cashBanks = res;
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get CashBankId() {
        return this.filterForm.controls["CashBankId"] as FormControl;
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
            .GetCashBankBalance(
                this.CashBankId.value,
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
            .ExportCashBankBalance(
                "PDF",
                this.CashBankId.value,
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
                    a.download = "CashBankBalance_" + ChangedFormat + ".pdf";
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
            .ExportCashBankBalance(
                "EXCEL",
                this.CashBankId.value,
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
                    a.download = "CashBankBalance_" + ChangedFormat + ".xls";
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
