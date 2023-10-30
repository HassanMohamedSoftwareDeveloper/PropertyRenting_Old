import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { OwnerDueInstallments } from "../../Models/Reports/owner-due-installments";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { BuildingService } from "../../Services/building.service";
import { OwnerService } from "../../Services/owner.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";
import { Lookup } from "../../Models/lookup";

@Component({
    selector: "app-owner-due-installments",
    templateUrl: "./owner-due-installments.component.html",
    styleUrls: ["./owner-due-installments.component.css"],
})
export class OwnerDueInstallmentsComponent implements OnInit {
    data: OwnerDueInstallments[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    filterForm!: FormGroup;
    submitted = false;
    owners: Lookup[] = [];
    buildings: Lookup[] = [];
    showReport = false;
    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private ownerService: OwnerService,
        private buildingService: BuildingService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.OwnerDueInstallmentsReportItems;
        this.createForm();
        this.loadOwners();
        this.loadBuildings();
        this.loadReport();
    }
    loadOwners() {
        this.ownerService.GetLookup().subscribe(
            (res) => (this.owners = res),
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }
    loadBuildings() {
        this.buildingService.GetLookup().subscribe(
            (res) => (this.buildings = res),
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }

    createForm() {
        this.filterForm = this.fb.group({
            OwnerId: [null],
            BuildingId: [null],
            ToDate: [null],
        });
    }

    get OwnerId() {
        return this.filterForm.controls["OwnerId"] as FormControl;
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
            .GetOwnerDueInstallments(
                this.ToDate.value,
                this.OwnerId.value,
                this.BuildingId.value
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
            .ExportOwnerDueInstallments(
                "PDF",
                this.ToDate.value,
                this.OwnerId.value,
                this.BuildingId.value
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
                        "OwnerDueInstallments_" + ChangedFormat + ".pdf";
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
            .ExportOwnerDueInstallments(
                "EXCEL",
                this.ToDate.value,
                this.OwnerId.value,
                this.BuildingId.value
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
                        "OwnerDueInstallments_" + ChangedFormat + ".xls";
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
