import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Voucher } from "../../../Models/voucher";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { TranslationService } from "../../../Services/translation.service";
import { VoucherService } from "../../../Services/voucher.service";

@Component({
    selector: "app-voucher-view",
    templateUrl: "./voucher-view.component.html",
    styleUrls: ["./voucher-view.component.css"],
})
export class VoucherViewComponent implements OnInit {
    voucher: Voucher = { voucherDetails: [] };
    breadcrumbItems: Breadcrumb[] = [];
    constructor(
        private breadcrumbService: BreadcrumbService,
        private router: Router,
        private route: ActivatedRoute,
        private voucherService: VoucherService,
        private alertify: AlertifyService,
        private translateService: TranslationService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.VoucherDetailsItems;

        const id = this.route.snapshot.paramMap.get("id");
        this.voucherService.GetVoucherById(id).subscribe(
            (res) => {
                this.voucher = res;
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
    backToList() {
        this.router.navigate(["/financials/vouchers"]);
    }
}
