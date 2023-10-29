import { Component, OnInit, ViewChild } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { City } from "../../../Models/city";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { CityService } from "../../../Services/city.service";
import { DialogService } from "../../../Services/dialog.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-city-list",
    templateUrl: "./city-list.component.html",
    styleUrls: ["./city-list.component.css"],
})
export class CityListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    cities: City[] = [];
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    city: City = {
        id: null,
        nameAR: "",
        nameEN: "",
        countryId: null,
        country: "",
    };

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private cityService: CityService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.LoadCities();
        this.breadcrumbItems = this.breadcumbService.CityListItems;
    }

    LoadCities() {
        this.cityService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.cities = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteCity(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.cityService.DeleteCity(id).subscribe(
                    () => {
                        this.LoadCities();
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
        this.city = {
            id: null,
            nameAR: "",
            nameEN: "",
            countryId: null,
            country: "",
        };
        this.modal?.showModal();
    }

    ShowEditModal(city: City) {
        this.city = { ...city };
        this.modal?.showModal();
    }

    HideModal(refreshList: boolean) {
        if (refreshList) {
            this.LoadCities();
        }
        this.modal?.hideModal();
    }

    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.LoadCities();
        console.log(page);
    }
}
