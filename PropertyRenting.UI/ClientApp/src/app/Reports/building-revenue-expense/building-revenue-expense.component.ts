import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
} from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { Building } from "../../Models/building";
import { BuildingRevenuExpense } from "../../Models/Reports/building-revenu-expense";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { BuildingService } from "../../Services/building.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";

@Component({
    selector: "app-building-revenue-expense",
    templateUrl: "./building-revenue-expense.component.html",
    styleUrls: ["./building-revenue-expense.component.css"],
})
export class BuildingRevenueExpenseComponent implements OnInit {
    data: BuildingRevenuExpense[] = [];
    buildings: Building[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    filterForm!: FormGroup;
    submitted = false;
    showReport = false;
    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private buildingservice: BuildingService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.BuildingRevenueExpenseReportItems;

        this.createForm();
        this.loadBuildings();
        this.loadReport();
    }
    createForm() {
        this.filterForm = this.fb.group({
            BuildingId: [null],
            ToDate: [null],
        });
    }

    loadBuildings() {
        this.buildingservice.GetAllBuildings().subscribe(
            (res) => {
                this.buildings = res;
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get BuildingId() {
        return this.filterForm.controls["BuildingId"] as FormControl;
    }

    get ToDate() {
        return this.filterForm.controls["ToDate"] as FormControl;
    }

    loadReport() {
        this.showReport = false;
        this.reportService
            .GetBuildingRevenueExpenses(
                this.BuildingId.value,
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
        this.submitted = true;
        if (this.filterForm.invalid) {
            return;
        }
        this.loadReport();
    }
    validateDropdown(control: AbstractControl) {
        const thisValue = control.value;
        if (
            thisValue == undefined ||
            thisValue == null ||
            thisValue == "" ||
            thisValue == "null"
        ) {
            return { required: true };
        }
        return null;
    }

    exportPDF() {
        this.reportService
            .ExportBuildingRevenueExpenses(
                "PDF",
                this.BuildingId.value,
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
                    a.download =
                        "BuildingRevenueExpenses_" + ChangedFormat + ".pdf";
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
            .ExportBuildingRevenueExpenses(
                "EXCEL",
                this.BuildingId.value,
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
                    a.download =
                        "BuildingRevenueExpenses_" + ChangedFormat + ".xls";
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
