import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { BuildingTransaction } from "../../Models/Reports/building-transaction";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { BuildingService } from "../../Services/building.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";
import { Lookup } from "../../Models/lookup";

@Component({
    selector: "app-building-transaction",
    templateUrl: "./building-transaction.component.html",
    styleUrls: ["./building-transaction.component.css"],
})
export class BuildingTransactionComponent implements OnInit {
    data: BuildingTransaction[] = [];
    buildings: Lookup[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    filterForm!: FormGroup;
    showReport = false;
    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private buildingService: BuildingService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.BuildingTransactionReportItems;

        this.createForm();
        this.loadBuildings();
    }
    createForm() {
        this.filterForm = this.fb.group({
            BuildingId: [null, Validators.required],
            FromDate: [null],
            ToDate: [null],
        });
    }

    loadBuildings() {
        this.buildingService.GetLookup().subscribe(
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
            .GetBuildingTransactions(
                this.BuildingId.value,
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
        const name =
            this.buildings.find((x) => x.id == this.BuildingId.value)
                ?.description || "";
        this.reportService
            .ExportBuildingTransactions(
                "PDF",
                this.BuildingId.value,
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
                    a.download =
                        "BuildingTransaction_" + ChangedFormat + ".pdf";
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
        const name =
            this.buildings.find((x) => x.id == this.BuildingId.value)
                ?.description || "";
        this.reportService
            .ExportBuildingTransactions(
                "EXCEL",
                this.BuildingId.value,
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
                    a.download =
                        "BuildingTransaction_" + ChangedFormat + ".xls";
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
