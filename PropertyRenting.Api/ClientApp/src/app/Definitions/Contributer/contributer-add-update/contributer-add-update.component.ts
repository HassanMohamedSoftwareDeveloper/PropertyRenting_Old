import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { v4 as uuidv4 } from "uuid";
import { Contributer } from "../../../Models/contributer";
import { AlertifyService } from "../../../Services/alertify.service";
import { ContributerService } from "../../../Services/contributer.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-contributer-add-update",
    templateUrl: "./contributer-add-update.component.html",
    styleUrls: ["./contributer-add-update.component.css"],
})
export class ContributerAddUpdateComponent implements OnInit {
    contributerForm!: FormGroup;
    @Input() contributer: Contributer = { id: null, nameAR: "", nameEN: "" };
    @Output() hideModalWithRefreshEvent = new EventEmitter<boolean>();
    submitted = false;

    constructor(
        private fb: FormBuilder,
        private contributerService: ContributerService,
        private alertify: AlertifyService,
        private translateService: TranslationService
    ) {}

    ngOnInit(): void {
        this.CreateFrom();
    }

    CreateFrom() {
        this.contributerForm = this.fb.group({
            NameAR: [null, Validators.required],
            NameEN: [null],
        });
    }

    //#region Form Controllers
    get NameAR() {
        return this.contributerForm.controls["NameAR"] as FormControl;
    }
    get NameEN() {
        return this.contributerForm.controls["NameEN"] as FormControl;
    }
    //#endregion

    onSubmit() {
        this.submitted = true;
        if (this.contributerForm.invalid) {
            return;
        }
        if (this.contributer.id === null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.contributer.id = uuidv4();
        this.contributerService.Create(this.contributer).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.cancel(true);
            },
            (error) => {
                console.log(error);
                this.contributer.id = null;
            }
        );
    }
    private Update() {
        this.contributerService
            .Edit(this.contributer.id, this.contributer)
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
        this.contributerForm.reset();
        this.submitted = false;
    }

    cancel(isRefresh: boolean) {
        this.resetFrom();
        this.hideModalWithRefreshEvent.emit(isRefresh);
    }
}
