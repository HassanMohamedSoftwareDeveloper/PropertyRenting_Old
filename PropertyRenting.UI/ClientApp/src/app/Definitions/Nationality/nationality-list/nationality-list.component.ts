import { Component, OnInit, ViewChild } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Nationality } from "../../../Models/nationality";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { NationalityService } from "../../../Services/nationality.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-nationality-list",
    templateUrl: "./nationality-list.component.html",
    styleUrls: ["./nationality-list.component.css"],
})
export class NationalityListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    nationalities: Nationality[] = [];
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    nationality: Nationality = {};

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private nationalityService: NationalityService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.loadNationalities();
        this.breadcrumbItems = this.breadcrumbService.NationalityListItems;
    }

    loadNationalities() {
        this.nationalityService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.nationalities = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteNationality(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.nationalityService.Delete(id).subscribe(
                    () => {
                        this.loadNationalities();
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
        this.nationality = { id: null, nameAR: "", nameEN: "" };
        this.modal?.showModal();
    }

    ShowEditModal(nationality: Nationality) {
        this.nationality = { ...nationality };
        this.modal?.showModal();
    }

    HideModal(refreshList: boolean) {
        if (refreshList) {
            this.loadNationalities();
        }
        this.modal?.hideModal();
    }

    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.loadNationalities();
    }
}
