import { Component, OnInit, ViewChild } from "@angular/core";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { Country } from "../../../Models/country";
import { CountryService } from "../../../Services/country.service";
import { AlertifyService } from "../../../Services/alertify.service";
import { TranslationService } from "../../../Services/translation.service";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { environment } from "../../../../environments/environment";
import { DialogService } from "../../../Services/dialog.service";

@Component({
    selector: "app-country-list",
    templateUrl: "./country-list.component.html",
    styleUrls: ["./country-list.component.css"],
})
export class CountryListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    countries: Country[] = [];
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    country: Country = { id: null, nameAR: "", nameEN: "" };

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private countryService: CountryService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadCrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.LoadCountries();

        this.breadcrumbItems = this.breadCrumbService.CountryListItems;
    }

    LoadCountries() {
        this.countryService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.countries = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteCountry(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.countryService.DeleteCountry(id).subscribe(
                    () => {
                        this.LoadCountries();
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
        this.country = { id: null, nameAR: "", nameEN: "" };
        this.modal?.showModal();
    }

    ShowEditModal(country: Country) {
        this.country = { ...country };
        this.modal?.showModal();
    }

    HideModal(refreshList: boolean) {
        if (refreshList) {
            this.LoadCountries();
        }
        this.modal?.hideModal();
    }

    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.LoadCountries();
    }
}
