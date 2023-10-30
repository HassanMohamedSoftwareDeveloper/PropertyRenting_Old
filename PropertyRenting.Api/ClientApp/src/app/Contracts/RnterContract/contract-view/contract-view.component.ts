import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { RenterContract } from "../../../Models/renter-contract";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { ContractService } from "../../../Services/contract.service";
import { RenterContractService } from "../../../Services/renter-contract.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-contract-view",
    templateUrl: "./contract-view.component.html",
    styleUrls: ["./contract-view.component.css"],
})
export class ContractViewComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    contract: RenterContract = { id: null, unitId: null, renterId: null };
    constructor(
        private breadcrumbService: BreadcrumbService,
        private renterContractService: RenterContractService,
        private router: Router,
        private route: ActivatedRoute,
        private translateService: TranslationService,
        private alertify: AlertifyService,
        private contractService: ContractService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.RenterContractDetailsItems;

        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.getContractById(id);
        }
    }
    GetContractState(id: number): string {
        return this.contractService.GetContractStateById(id);
    }
    GetPaymentMethod(id: number): string {
        return this.contractService.GetPaymentMethodById(id);
    }
    getContractById(id: string) {
        this.renterContractService.GetById(id).subscribe({
            next: (result) => {
                this.contract = result;
            },
            error: (error) => console.log(error),
        });
    }
    CancelContract(id: any) {
        this.renterContractService.Cancel(id).subscribe({
            next: () => {
                const successMsg = this.translateService.Translate(
                    "CanceledSuccessfully"
                );
                this.alertify.success(successMsg);
                this.getContractById(id);
            },
            error: (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            },
        });
    }
    backToList() {
        this.router.navigate(["/contracts/rentercontracts"]);
    }
}
