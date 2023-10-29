import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { City } from "../../../Models/city";
import { AlertifyService } from "../../../Services/alertify.service";
import { CityService } from "../../../Services/city.service";
import { TranslationService } from "../../../Services/translation.service";
import { v4 as uuidv4 } from "uuid";
import { CountryService } from "../../../Services/country.service";
import { Country } from "../../../Models/country";
import { Lookup } from "../../../Models/lookup";

@Component({
    selector: "app-city-add-update",
    templateUrl: "./city-add-update.component.html",
    styleUrls: ["./city-add-update.component.css"],
})
export class CityAddUpdateComponent implements OnInit {
    cityForm!: FormGroup;
    @Input() city: City = {
        id: null,
        nameAR: "",
        nameEN: "",
        countryId: null,
        country: "",
    };
    @Output() hideModalWithRefreshEvent = new EventEmitter<boolean>();
    submitted = false;
    countries: Lookup[] = [];
    constructor(
        private fb: FormBuilder,
        private cityService: CityService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private countryService: CountryService
    ) {}

    ngOnInit(): void {
        this.CreateFrom();
        this.LoadCountries();
    }
    LoadCountries() {
        this.countryService.GetLookup().subscribe({
            next: (result) => {
                this.countries = result;
            },
            error: (error) => console.log(error),
        });
    }

    CreateFrom() {
        this.cityForm = this.fb.group({
            NameAR: [null, Validators.required],
            NameEN: [null],
            CountryId: [null, [Validators.required, this.validateDropdown]],
        });
    }

    //#region Form Controllers
    get NameAR() {
        return this.cityForm.controls["NameAR"] as FormControl;
    }
    get NameEN() {
        return this.cityForm.controls["NameEN"] as FormControl;
    }
    get CountryId() {
        return this.cityForm.controls["CountryId"] as FormControl;
    }
    //#endregion

    onSubmit() {
        this.submitted = true;
        if (this.cityForm.invalid) {
            return;
        }
        if (this.city.id === null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.city.id = uuidv4();
        this.cityService.CreateNewCity(this.city).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.cancel(true);
            },
            (error) => {
                console.log(error);
                this.city.id = null;
            }
        );
    }
    private Update() {
        this.cityService.EditCity(this.city.id, this.city).subscribe(
            () => {
                const successMsg = this.translateService.Translate(
                    "UpdatedSuccessfully"
                );
                this.alertify.success(successMsg);
                this.cancel(true);
            },
            (error) => console.log(error)
        );
    }

    resetFrom() {
        this.cityForm.reset();
        this.submitted = false;
    }

    cancel(isRefresh: boolean) {
        this.resetFrom();
        this.hideModalWithRefreshEvent.emit(isRefresh);
    }
    validateDropdown(control: AbstractControl) {
        const thisValue = control.value;
        if (
            thisValue === undefined ||
            thisValue === null ||
            thisValue === "" ||
            thisValue === "null"
        ) {
            return { required: true };
        }
        return null;
    }
}
