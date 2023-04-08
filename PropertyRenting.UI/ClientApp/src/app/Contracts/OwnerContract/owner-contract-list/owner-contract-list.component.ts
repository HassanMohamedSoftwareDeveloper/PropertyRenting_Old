import { Component, OnInit } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { OwnerContract } from "../../../Models/owner-contract";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { ContractService } from "../../../Services/contract.service";
import { DialogService } from "../../../Services/dialog.service";
import { OwnerContractService } from "../../../Services/owner-contract.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-owner-contract-list",
    templateUrl: "./owner-contract-list.component.html",
    styleUrls: ["./owner-contract-list.component.css"],
})
export class OwnerContractListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    contracts: OwnerContract[] = [];
    pageNumber = 1;
    totalItems = 0;

    constructor(
        private ownerContractService: OwnerContractService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private contractService: ContractService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.loadContracts();
        this.breadcrumbItems = this.breadcrumbService.OwnerContractListItems;
    }

    loadContracts() {
        this.ownerContractService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.contracts = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteContract(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.ownerContractService.Delete(id).subscribe(
                    () => {
                        this.loadContracts();
                        const successMsg = this.translateService.Translate(
                            "DeletedSuccessfully"
                        );
                        this.alertify.success(successMsg);
                    },
                    (error) => console.log(error)
                );
            }
        });
    }

    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.loadContracts();
    }

    GetContractState(id: number): string {
        return this.contractService.GetContractStateById(id);
    }
    GetPaymentMethod(id: number): string {
        return this.contractService.GetPaymentMethodById(id);
    }
}
