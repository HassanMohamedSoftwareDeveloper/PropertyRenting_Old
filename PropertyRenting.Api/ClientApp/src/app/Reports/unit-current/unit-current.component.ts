import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { Breadcrumb } from "../../Models/breadcrumb";
import { UnitCurrent } from "../../Models/Reports/unit-current";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";

@Component({
    selector: "app-unit-current",
    templateUrl: "./unit-current.component.html",
    styleUrls: ["./unit-current.component.css"],
})
export class UnitCurrentComponent implements OnInit {
    data: UnitCurrent[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.CurrentUnitsReportItems;

        this.loadReport();
    }

    loadReport() {
        this.reportService.GetCurrentUnits().subscribe(
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
        this.reportService.ExportCurrentUnits("PDF").subscribe(
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
                a.download = "CurrentUnits_" + ChangedFormat + ".pdf";
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
        this.reportService.ExportCurrentUnits("EXCEL").subscribe(
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
                a.download = "CurrentUnits_" + ChangedFormat + ".xls";
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
