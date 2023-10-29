import { Component, OnInit, ViewChild } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Contributer } from "../../../Models/contributer";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { ContributerService } from "../../../Services/contributer.service";
import { DialogService } from "../../../Services/dialog.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-contributer-list",
    templateUrl: "./contributer-list.component.html",
    styleUrls: ["./contributer-list.component.css"],
})
export class ContributerListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    contributers: Contributer[] = [];
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    contributer: Contributer = { id: null, nameAR: "", nameEN: "" };

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private contributerService: ContributerService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.LoadContributers();
        this.breadcrumbItems = this.breadcumbService.ContributerListItems;
    }

    LoadContributers() {
        this.contributerService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.contributers = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteContributer(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.contributerService.Delete(id).subscribe(
                    () => {
                        this.LoadContributers();
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
        this.contributer = { id: null, nameAR: "", nameEN: "" };
        this.modal?.showModal();
    }

    ShowEditModal(contributer: Contributer) {
        this.contributer = { ...contributer };
        this.modal?.showModal();
    }

    HideModal(refreshList: boolean) {
        if (refreshList) {
            this.LoadContributers();
        }
        this.modal?.hideModal();
    }

    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.LoadContributers();
        console.log(page);
    }
}
