import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Sanad } from "../../../Models/sanad";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { TranslationService } from "../../../Services/translation.service";
import { VoucherService } from "../../../Services/voucher.service";

@Component({
    selector: "app-exchange-view",
    templateUrl: "./exchange-view.component.html",
    styleUrls: ["./exchange-view.component.css"],
})
export class ExchangeViewComponent implements OnInit {
    voucher: Sanad = { sanadTypeId: 1, sanadDetails: [] };
    breadcrumbItems: Breadcrumb[] = [];
    constructor(
        private breadcrumbService: BreadcrumbService,
        private voucherService: VoucherService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private router: Router,
        private route: ActivatedRoute
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.ExchangeDetailsItems;

        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.getVoucherById(id);
        }
    }
    getVoucherById(id: any) {
        this.voucherService.GetExchangeById(id).subscribe(
            (result) => {
                this.voucher = result;
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }
    GetStateById(id: number) {
        return this.voucherService.GetVoucherStateById(id);
    }
    GetTypeById(id: number) {
        return this.voucherService.GetSanadTypeById(id);
    }
    backToList() {
        this.router.navigate(["/financials/exchanges"]);
    }
    Print(id: any) {
        this.voucherService.PrintExchange(id).subscribe(
            (blob) => {
                const today = new Date();
                const pipe = new DatePipe("en-US");
                const ChangedFormat = pipe.transform(today, "dd-MM-YYYY");
                const a = document.createElement("a");
                const objectUrl = URL.createObjectURL(blob);
                a.href = objectUrl;
                a.download = "Exchange_" + ChangedFormat + "_" + id + ".pdf";
                a.click();
                URL.revokeObjectURL(objectUrl);
            },
            (error) => {
                console.log(error);
            }
        );
    }
}
