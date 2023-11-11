import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { RenterTransaction } from "../../Models/Reports/renter-transaction";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { RenterService } from "../../Services/renter.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";
import { Lookup } from "../../Models/lookup";
import { AuthService } from "../../Services/auth.service";

@Component({
    selector: "app-renter-transaction",
    templateUrl: "./renter-transaction.component.html",
    styleUrls: ["./renter-transaction.component.css"],
})
export class RenterTransactionComponent implements OnInit {
    data: RenterTransaction[] = [];
    renters: Lookup[] = [];
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
        private renterService: RenterService,
        public authService: AuthService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.RenterTransactionReportItems;

        this.createForm();
        this.loadRenters();
    }
    createForm() {
        const date = new Date();
        date.setMonth(0, 1);
        if (this.authService.IsSubAdmin()) {
            this.minDate = date;
        }
        this.filterForm = this.fb.group({
            RenterId: [null, Validators.required],
            FromDate: [date],
            ToDate: [null],
        });
    }

    loadRenters() {
        this.renterService.GetLookup().subscribe(
            (res) => {
                this.renters = res;
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get RenterId() {
        return this.filterForm.controls["RenterId"] as FormControl;
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
            .GetRenterTransactions(
                this.RenterId.value,
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
            this.renters.find((x) => x.id == this.RenterId.value)
                ?.description || "";
        this.reportService
            .ExportRenterTransactions(
                "PDF",
                this.RenterId.value,
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
                    a.download = "RenterTransaction_" + ChangedFormat + ".pdf";
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
            this.renters.find((x) => x.id == this.RenterId.value)
                ?.description || "";
        this.reportService
            .ExportRenterTransactions(
                "EXCEL",
                this.RenterId.value,
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
                    a.download = "RenterTransaction_" + ChangedFormat + ".xls";
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
