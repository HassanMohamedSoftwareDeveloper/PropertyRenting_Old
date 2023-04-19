import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { v4 as uuidv4 } from "uuid";
import { Owner } from "../../../Models/owner";
import { AlertifyService } from "../../../Services/alertify.service";
import { OwnerService } from "../../../Services/owner.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-owner-add-update",
    templateUrl: "./owner-add-update.component.html",
    styleUrls: ["./owner-add-update.component.css"],
})
export class OwnerAddUpdateComponent implements OnInit {
    ownerForm!: FormGroup;
    @Input() owner: Owner = { id: null, nameAR: "", nameEN: "" };
    @Output() hideModalWithRefreshEvent = new EventEmitter<boolean>();
    submitted = false;

    constructor(
        private fb: FormBuilder,
        private ownerService: OwnerService,
        private alertify: AlertifyService,
        private translateService: TranslationService
    ) {}

    ngOnInit(): void {
        this.CreateFrom();
    }

    CreateFrom() {
        this.ownerForm = this.fb.group({
            NameAR: [null, Validators.required],
            NameEN: [null],
        });
    }

    //#region Form Controllers
    get NameAR() {
        return this.ownerForm.controls["NameAR"] as FormControl;
    }
    get NameEN() {
        return this.ownerForm.controls["NameEN"] as FormControl;
    }
    //#endregion

    onSubmit() {
        this.submitted = true;
        if (this.ownerForm.invalid) {
            return;
        }
        if (this.owner.id === null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.owner.id = uuidv4();
        this.ownerService.CreateNewOwner(this.owner).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.cancel(true);
            },
            (error) => {
                console.log(error);
                this.owner.id = null;
            }
        );
    }
    private Update() {
        this.ownerService.EditOwner(this.owner.id, this.owner).subscribe(
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
        this.ownerForm.reset();
        this.submitted = false;
    }

    cancel(isRefresh: boolean) {
        this.resetFrom();
        this.hideModalWithRefreshEvent.emit(isRefresh);
    }
}
