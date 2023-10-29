import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { Renter } from "../../Models/renter";
import { RenterDueInstallments } from "../../Models/Reports/renter-due-installments";
import { Unit } from "../../Models/unit";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { RenterService } from "../../Services/renter.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";
import { UnitService } from "../../Services/unit.service";

@Component({
    selector: "app-renter-due-installments",
    templateUrl: "./renter-due-installments.component.html",
    styleUrls: ["./renter-due-installments.component.css"],
})
export class RenterDueInstallmentsComponent implements OnInit {
    data: RenterDueInstallments[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    filterForm!: FormGroup;
    renters: Renter[] = [];
    units: Unit[] = [];
    showReport = false;
    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private renterService: RenterService,
        private unitService: UnitService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.RenterDueInstallmentsReportItems;
        this.createForm();
        this.loadRenters();
        this.loadUnits();
        this.loadReport();
    }
    loadRenters() {
        this.renterService.GetAll().subscribe(
            (res) => (this.renters = res),
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }
    loadUnits() {
        this.unitService.GetAll().subscribe(
            (res) => (this.units = res),
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }

    createForm() {
        this.filterForm = this.fb.group({
            RenterId: [null],
            UnitId: [null],
            ToDate: [null],
        });
    }

    get RenterId() {
        return this.filterForm.controls["RenterId"] as FormControl;
    }
    get UnitId() {
        return this.filterForm.controls["UnitId"] as FormControl;
    }
    get ToDate() {
        return this.filterForm.controls["ToDate"] as FormControl;
    }
    loadReport() {
        this.showReport = false;
        this.reportService
            .GetRenterDueInstallments(
                this.ToDate.value,
                this.RenterId.value,
                this.UnitId.value
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
            .ExportRenterDueInstallments(
                "PDF",
                this.ToDate.value,
                this.RenterId.value,
                this.UnitId.value
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
                        "RenterDueInstallments_" + ChangedFormat + ".pdf";
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
            .ExportRenterDueInstallments(
                "EXCEL",
                this.ToDate.value,
                this.RenterId.value,
                this.UnitId.value
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
                        "RenterDueInstallments_" + ChangedFormat + ".xls";
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
