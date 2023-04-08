import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Nationality } from "../../../Models/nationality";
import { v4 as uuidv4 } from "uuid";
import { AlertifyService } from "../../../Services/alertify.service";
import { NationalityService } from "../../../Services/nationality.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-nationality-add-update",
    templateUrl: "./nationality-add-update.component.html",
    styleUrls: ["./nationality-add-update.component.css"],
})
export class NationalityAddUpdateComponent implements OnInit {
    nationalityForm!: FormGroup;
    @Input() nationality: Nationality = {};
    @Output() hideModalWithRefreshEvent = new EventEmitter<boolean>();
    submitted = false;

    constructor(
        private fb: FormBuilder,
        private nationalityService: NationalityService,
        private alertify: AlertifyService,
        private translateService: TranslationService
    ) {}

    ngOnInit(): void {
        this.CreateFrom();
    }

    CreateFrom() {
        this.nationalityForm = this.fb.group({
            NameAR: [null, Validators.required],
            NameEN: [null, Validators.required],
        });
    }

    //#region Form Controllers
    get NameAR() {
        return this.nationalityForm.controls["NameAR"] as FormControl;
    }
    get NameEN() {
        return this.nationalityForm.controls["NameEN"] as FormControl;
    }
    //#endregion

    onSubmit() {
        this.submitted = true;
        if (this.nationalityForm.invalid) {
            return;
        }
        if (this.nationality.id === null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.nationality.id = uuidv4();
        this.nationalityService.Create(this.nationality).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.cancel(true);
            },
            (error) => {
                console.log(error);
                this.nationality.id = null;
            }
        );
    }
    private Update() {
        this.nationalityService
            .Edit(this.nationality.id, this.nationality)
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
        this.nationalityForm.reset();
        this.submitted = false;
    }

    cancel(isRefresh: boolean) {
        this.resetFrom();
        this.hideModalWithRefreshEvent.emit(isRefresh);
    }
}
