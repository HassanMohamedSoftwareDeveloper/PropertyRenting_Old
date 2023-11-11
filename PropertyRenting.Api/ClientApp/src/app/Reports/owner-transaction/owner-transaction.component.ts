import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { OwnerTransaction } from "../../Models/Reports/owner-transaction";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { OwnerService } from "../../Services/owner.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";
import { Lookup } from "../../Models/lookup";
import { AuthService } from "../../Services/auth.service";

@Component({
    selector: "app-owner-transaction",
    templateUrl: "./owner-transaction.component.html",
    styleUrls: ["./owner-transaction.component.css"],
})
export class OwnerTransactionComponent implements OnInit {
    data: OwnerTransaction[] = [];
    owners: Lookup[] = [];
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
        private ownerService: OwnerService,
        public authService: AuthService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.OwnerTransactionReportItems;

        this.createForm();
        this.loadOwners();
    }
    createForm() {
        const date = new Date();
        date.setMonth(0, 1);
        if (this.authService.IsSubAdmin()) {
            this.minDate = date;
        }
        this.filterForm = this.fb.group({
            OwnerId: [null, Validators.required],
            FromDate: [date],
            ToDate: [null],
        });
    }

    loadOwners() {
        this.ownerService.GetLookup().subscribe(
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
            .GetOwnerTransactions(
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
        const name =
            this.owners.find((x) => x.id == this.OwnerId.value)?.description ||
            "";
        this.reportService
            .ExportOwnerTransactions(
                "PDF",
                this.OwnerId.value,
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
                    a.download = "OwnerTransaction_" + ChangedFormat + ".pdf";
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
            this.owners.find((x) => x.id == this.OwnerId.value)?.description ||
            "";
        this.reportService
            .ExportOwnerTransactions(
                "EXCEL",
                this.OwnerId.value,
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
                    a.download = "OwnerTransaction_" + ChangedFormat + ".xls";
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
