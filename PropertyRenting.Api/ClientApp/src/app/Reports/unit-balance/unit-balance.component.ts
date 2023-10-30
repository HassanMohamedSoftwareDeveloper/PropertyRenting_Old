import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { UnitBalance } from "../../Models/Reports/unit-balance";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";
import { UnitService } from "../../Services/unit.service";
import { UnitLookup } from "../../Models/unit-lookup";

@Component({
    selector: "app-unit-balance",
    templateUrl: "./unit-balance.component.html",
    styleUrls: ["./unit-balance.component.css"],
})
export class UnitBalanceComponent implements OnInit {
    data: UnitBalance[] = [];
    units: UnitLookup[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    filterForm!: FormGroup;
    showReport = false;
    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private unitService: UnitService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.UnitBalanceReportItems;

        this.createForm();
        this.loadUnits();
        this.loadReport();
    }
    createForm() {
        this.filterForm = this.fb.group({
            UnitId: [null],
            FromDate: [null],
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
            .GetUnitBalance(
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
        this.reportService
            .ExportUnitBalance(
                "PDF",
                this.UnitId.value,
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
                    a.download = "UnitBalance_" + ChangedFormat + ".pdf";
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
            .ExportUnitBalance(
                "EXCEL",
                this.UnitId.value,
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
                    a.download = "UnitBalance_" + ChangedFormat + ".xls";
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
