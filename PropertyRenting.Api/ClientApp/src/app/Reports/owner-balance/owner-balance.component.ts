import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { Owner } from "../../Models/owner";
import { OwnerBalance } from "../../Models/Reports/owner-balance";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { OwnerService } from "../../Services/owner.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";

@Component({
    selector: "app-owner-balance",
    templateUrl: "./owner-balance.component.html",
    styleUrls: ["./owner-balance.component.css"],
})
export class OwnerBalanceComponent implements OnInit {
    data: OwnerBalance[] = [];
    owners: Owner[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    filterForm!: FormGroup;
    showReport = false;
    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private ownerService: OwnerService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.OwnerBalanceReportItems;

        this.createForm();
        this.loadOwners();
        this.loadReport();
    }
    createForm() {
        this.filterForm = this.fb.group({
            OwnerId: [null],
            FromDate: [null],
            ToDate: [null],
        });
    }

    loadOwners() {
        this.ownerService.GetAllOwners().subscribe(
            (res) => {
                this.owners = res;
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get OwnerId() {
        return this.filterForm.controls["OwnerId"] as FormControl;
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
            .GetOwnerBalance(
                this.OwnerId.value,
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
            .ExportOwnerBalance(
                "PDF",
                this.OwnerId.value,
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
                    a.download = "OwnerBalance_" + ChangedFormat + ".pdf";
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
            .ExportOwnerBalance(
                "EXCEL",
                this.OwnerId.value,
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
                    a.download = "OwnerBalance_" + ChangedFormat + ".xls";
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