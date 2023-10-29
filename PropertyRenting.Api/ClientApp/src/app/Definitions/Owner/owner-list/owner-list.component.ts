import { Component, OnInit, ViewChild } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Owner } from "../../../Models/owner";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { OwnerService } from "../../../Services/owner.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-owner-list",
    templateUrl: "./owner-list.component.html",
    styleUrls: ["./owner-list.component.css"],
})
export class OwnerListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    owners: Owner[] = [];
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    owner: Owner = { id: null, nameAR: "", nameEN: "" };

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private ownerService: OwnerService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.LoadOwners();
        this.breadcrumbItems = this.breadcrumbService.OwnerListItems;
    }

    LoadOwners() {
        this.ownerService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.owners = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteOwner(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.ownerService.DeleteOwner(id).subscribe(
                    () => {
                        this.LoadOwners();
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

    ShowAddModal() {
        this.owner = { id: null, nameAR: "", nameEN: "" };
        this.modal?.showModal();
    }

    ShowEditModal(owner: Owner) {
        this.owner = { ...owner };
        this.modal?.showModal();
    }

    HideModal(refreshList: boolean) {
        if (refreshList) {
            this.LoadOwners();
        }
        this.modal?.hideModal();
    }

    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.LoadOwners();
        console.log(page);
    }
}
