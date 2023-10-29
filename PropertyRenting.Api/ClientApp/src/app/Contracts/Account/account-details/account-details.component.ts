import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { v4 as uuidv4 } from "uuid";
import { Account } from "../../../Models/account";
import { Enum } from "../../../Models/enum";
import { AccountService } from "../../../Services/account.service";
import { AlertifyService } from "../../../Services/alertify.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-account-details",
    templateUrl: "./account-details.component.html",
    styleUrls: ["./account-details.component.css"],
})
export class AccountDetailsComponent implements OnInit {
    accountForm!: FormGroup;
    @Input() account: Account = { id: null };
    @Input() accounts: Account[] = [];
    @Output() hideModalWithRefreshEvent = new EventEmitter<boolean>();
    submitted = false;
    disabled = true;
    accountTypes: Enum[] = [];

    constructor(
        private fb: FormBuilder,
        private accountService: AccountService,
        private alertify: AlertifyService,
        private translateService: TranslationService
    ) {
        this.accountTypes = accountService.AccountType;
    }

    ngOnInit(): void {
        this.CreateFrom();
    }

    CreateFrom() {
        this.accountForm = this.fb.group({
            Code: [null, Validators.required],
            NameAR: [null, Validators.required],
            NameEN: [null],
            AccountTypeId: [null, Validators.required],
            ParentId: [null, Validators.required],
        });
    }

    //#region Form Controllers
    get Code() {
        return this.accountForm.controls["Code"] as FormControl;
    }
    get NameAR() {
        return this.accountForm.controls["NameAR"] as FormControl;
    }
    get NameEN() {
        return this.accountForm.controls["NameEN"] as FormControl;
    }
    get AccountTypeId() {
        return this.accountForm.controls["AccountTypeId"] as FormControl;
    }
    get ParentId() {
        return this.accountForm.controls["ParentId"] as FormControl;
    }
    //#endregion

    get HasChild() {
        return (
            !!this.account.accountChildren &&
            this.account.accountChildren.length > 0
        );
    }
    get IsLevelOne() {
        return this.account.id != null && this.account.level == 1;
    }
    onSubmit() {
        this.submitted = true;
        if (this.accountForm.invalid) {
            return;
        }
        if (this.account.id === null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.account.id = uuidv4();
        this.accountService.Create(this.account).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.cancel(true);
            },
            (error) => {
                console.log(error);
                this.account.id = null;
            }
        );
    }
    private Update() {
        this.accountService.Edit(this.account.id, this.account).subscribe(
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
        this.accountForm.reset();
        this.submitted = false;
    }

    cancel(isRefresh: boolean) {
        this.resetFrom();
        this.hideModalWithRefreshEvent.emit(isRefresh);
    }
}
