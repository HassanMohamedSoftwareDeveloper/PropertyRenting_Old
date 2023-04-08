import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Country } from "../../../Models/country";
import { CountryService } from "../../../Services/country.service";
import { v4 as uuidv4 } from "uuid";
import { AlertifyService } from "../../../Services/alertify.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-country-add-update",
    templateUrl: "./country-add-update.component.html",
    styleUrls: ["./country-add-update.component.css"],
})
export class CountryAddUpdateComponent implements OnInit {
    countryForm!: FormGroup;
    @Input() country: Country = { id: null, nameAR: "", nameEN: "" };
    @Output() hideModalWithRefreshEvent = new EventEmitter<boolean>();
    submitted = false;

    constructor(
        private fb: FormBuilder,
        private countryService: CountryService,
        private alertify: AlertifyService,
        private translateService: TranslationService
    ) {}

    ngOnInit(): void {
        this.CreateFrom();
    }

    CreateFrom() {
        this.countryForm = this.fb.group({
            NameAR: [null, Validators.required],
            NameEN: [null, Validators.required],
        });
    }

    //#region Form Controllers
    get NameAR() {
        return this.countryForm.controls["NameAR"] as FormControl;
    }
    get NameEN() {
        return this.countryForm.controls["NameEN"] as FormControl;
    }
    //#endregion

    onSubmit() {
        this.submitted = true;
        if (this.countryForm.invalid) {
            return;
        }
        if (this.country.id === null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.country.id = uuidv4();
        this.countryService.CreateNewCountry(this.country).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.cancel(true);
            },
            (error) => {
                console.log(error);
                this.country.id = null;
            }
        );
    }
    private Update() {
        this.countryService
            .EditCountry(this.country.id, this.country)
            .subscribe(
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
        this.countryForm.reset();
        this.submitted = false;
    }

    cancel(isRefresh: boolean) {
        this.resetFrom();
        this.hideModalWithRefreshEvent.emit(isRefresh);
    }
}
