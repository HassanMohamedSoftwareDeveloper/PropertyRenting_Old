import { Component, OnInit } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { UnitTransaction } from "../../Models/Reports/unit-transaction";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { UnitService } from "../../Services/unit.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";
import { DatePipe } from "@angular/common";
import { UnitLookup } from "../../Models/unit-lookup";
import { AuthService } from "../../Services/auth.service";

@Component({
    selector: "app-unit-transaction",
    templateUrl: "./unit-transaction.component.html",
    styleUrls: ["./unit-transaction.component.css"],
})
export class UnitTransactionComponent implements OnInit {
    data: UnitTransaction[] = [];
    units: UnitLookup[] = [];
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
        private unitService: UnitService,
        public authService: AuthService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.UnitTransactionReportItems;

        this.createForm();
        this.loadUnits();
    }
    createForm() {
        const date = new Date();
        date.setMonth(0, 1);
        if (this.authService.IsSubAdmin()) {
            this.minDate = date;
        }
        this.filterForm = this.fb.group({
            UnitId: [null, Validators.required],
            FromDate: [date],
            ToDate: [null],
        });
    }

    loadUnits() {
        this.unitService.GetLookup().subscribe(
            (res) => {
                this.units = res;
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get UnitId() {
        return this.filterForm.controls["UnitId"] as FormControl;
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
            .GetUnitTransactions(
                this.UnitId.value,
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
        const unit = this.units.find((x) => x.id == this.UnitId.value);
        const name = unit?.description || "";
        this.reportService
            .ExportUnitTransactions(
                "PDF",
                this.UnitId.value,
                name,
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
                    a.download = "UnitTransaction_" + ChangedFormat + ".pdf";
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
        const unit = this.units.find((x) => x.id == this.UnitId.value);
        const name = unit?.description || "";
        this.reportService
            .ExportUnitTransactions(
                "EXCEL",
                this.UnitId.value,
                name,
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
                    a.download = "UnitTransaction_" + ChangedFormat + ".xls";
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
