import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { OwnerContract } from "../../../Models/owner-contract";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { ContractService } from "../../../Services/contract.service";
import { OwnerContractService } from "../../../Services/owner-contract.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-owner-contract-view",
    templateUrl: "./owner-contract-view.component.html",
    styleUrls: ["./owner-contract-view.component.css"],
})
export class OwnerContractViewComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    contract: OwnerContract = { id: null, buildingId: null, ownerId: null };
    constructor(
        private breadcrumbService: BreadcrumbService,
        private ownerContractService: OwnerContractService,
        private router: Router,
        private route: ActivatedRoute,
        private translateService: TranslationService,
        private alertify: AlertifyService,
        private contractService: ContractService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.OwnerContractDetailsItems;

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
        this.ownerContractService.GetById(id).subscribe(
            (result) => {
                this.contract = result;
            },
            (error) => console.log(error)
        );
    }
    CancelContract(id: any) {
        this.ownerContractService.Cancel(id).subscribe(
            () => {
                const successMsg = this.translateService.Translate(
                    "CanceledSuccessfully"
                );
                this.alertify.success(successMsg);
                this.getContractById(id);
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }
    backToList() {
        this.router.navigate(["/contracts/ownercontracts"]);
    }
}
