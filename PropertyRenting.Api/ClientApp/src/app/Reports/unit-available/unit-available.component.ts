import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { Breadcrumb } from "../../Models/breadcrumb";
import { UnitAvailable } from "../../Models/Reports/unit-available";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";

@Component({
    selector: "app-unit-available",
    templateUrl: "./unit-available.component.html",
    styleUrls: ["./unit-available.component.css"],
})
export class UnitAvailableComponent implements OnInit {
    data: UnitAvailable[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.AvailableUnitsReportItems;

        this.loadReport();
    }

    loadReport() {
        this.reportService.GetAvailableUnits().subscribe(
            (res) => {
                this.data = res;
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }

    exportPDF() {
        this.reportService.ExportAvailableUnits("PDF").subscribe(
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
                a.download = "AvailableUnits_" + ChangedFormat + ".pdf";
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
        this.reportService.ExportAvailableUnits("EXCEL").subscribe(
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
                a.download = "AvailableUnits_" + ChangedFormat + ".xls";
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
