import { Component, OnInit } from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { forkJoin } from "rxjs";
import { v4 as uuidv4 } from "uuid";
import { AccountSetup } from "../../../Models/account-setup";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { AccountSetupSetupService } from "../../../Services/account-setup.service";
import { AccountService } from "../../../Services/account.service";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { TranslationService } from "../../../Services/translation.service";
import { AccountLookup } from "../../../Models/account-lookup";

@Component({
    selector: "app-account-setup-details",
    templateUrl: "./account-setup-details.component.html",
    styleUrls: ["./account-setup-details.component.css"],
})
export class AccountSetupDetailsComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    accountSetupForm!: FormGroup;
    accounts: AccountLookup[] = [];
    accountSetup: AccountSetup = {
        id: null,
        revenueAccountId: null,
        expenseAccountId: null,
        contributerAccountId: null,
        renterAccountId: null,
        ownerAccountId: null,
        accruedExpenseAccountId: null,
        accruedRevenueAccountId: null,
    };
    submitted = false;
    showForm = false;
    constructor(
        private fb: FormBuilder,
        private translateService: TranslationService,
        private alertify: AlertifyService,
        private accountSetupService: AccountSetupSetupService,
        private accountService: AccountService,
        private breadcrumbService: BreadcrumbService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.AccountSetupItems;

        this.loadData();
    }
    loadData() {
        forkJoin([
            this.accountService.GetLookup(),
            this.accountSetupService.Get(),
        ]).subscribe({
            next: ([accountRes, accSetupRes]) => {
                this.accounts = accountRes.filter((x) => x.accountTypeId !== 5);
                this.accountSetup = accSetupRes || {
                    id: null,
                    revenueAccountId: null,
                    expenseAccountId: null,
                    contributerAccountId: null,
                    renterAccountId: null,
                    ownerAccountId: null,
                };
                this.createForm();
            },
            error: (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            },
        });
    }

    createForm() {
        this.accountSetupForm = this.fb.group({
            RevenueAccountId: [
                null,
                [Validators.required, this.validateDropdown],
            ],
            AccruedRevenueAccountId: [
                null,
                [Validators.required, this.validateDropdown],
            ],
            ExpenseAccountId: [
                null,
                [Validators.required, this.validateDropdown],
            ],
            AccruedExpenseAccountId: [
                null,
                [Validators.required, this.validateDropdown],
            ],
            ContributerAccountId: [
                null,
                [Validators.required, this.validateDropdown],
            ],
            RenterAccountId: [
                null,
                [Validators.required, this.validateDropdown],
            ],
            OwnerAccountId: [
                null,
                [Validators.required, this.validateDropdown],
            ],
        });
        this.showForm = true;
    }
    get RevenueAccountId() {
        return this.accountSetupForm.controls[
            "RevenueAccountId"
        ] as FormControl;
    }
    get AccruedRevenueAccountId() {
        return this.accountSetupForm.controls[
            "AccruedRevenueAccountId"
        ] as FormControl;
    }
    get ExpenseAccountId() {
        return this.accountSetupForm.controls[
            "ExpenseAccountId"
        ] as FormControl;
    }
    get AccruedExpenseAccountId() {
        return this.accountSetupForm.controls[
            "AccruedExpenseAccountId"
        ] as FormControl;
    }
    get ContributerAccountId() {
        return this.accountSetupForm.controls[
            "ContributerAccountId"
        ] as FormControl;
    }
    get RenterAccountId() {
        return this.accountSetupForm.controls["RenterAccountId"] as FormControl;
    }
    get OwnerAccountId() {
        return this.accountSetupForm.controls["OwnerAccountId"] as FormControl;
    }
    onSubmit() {
        this.submitted = true;
        if (this.accountSetupForm.invalid) {
            return;
        }
        if (this.accountSetup.id === null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.accountSetup.id = uuidv4();
        this.accountSetupService.Create(this.accountSetup).subscribe(
            (res: any) => {
                this.accountSetup.id = res.id;
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
                this.accountSetup.id = null;
            }
        );
    }
    private Update() {
        this.accountSetupService
            .Edit(this.accountSetup.id, this.accountSetup)
            .subscribe(
                () => {
                    const successMsg = this.translateService.Translate(
                        "UpdatedSuccessfully"
                    );
                    this.alertify.success(successMsg);
                },
                (error) => {
                    const msg =
                        this.translateService.Translate("ErrorOccurred");
                    this.alertify.error(msg);
                    console.log(error);
                }
            );
    }

    validateDropdown(control: AbstractControl) {
        const thisValue = control.value;
        if (
            thisValue == undefined ||
            thisValue == null ||
            thisValue == "" ||
            thisValue == "null"
        ) {
            return { required: true };
        }
        return null;
    }
}
