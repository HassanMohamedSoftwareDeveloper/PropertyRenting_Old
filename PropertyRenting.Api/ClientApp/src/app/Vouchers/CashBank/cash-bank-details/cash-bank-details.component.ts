import { Component, OnInit } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { forkJoin } from "rxjs";
import { v4 as uuidv4 } from "uuid";
import { Account } from "../../../Models/account";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { CashBank } from "../../../Models/cash-bank";
import { Enum } from "../../../Models/enum";
import { AccountService } from "../../../Services/account.service";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { CashBankService } from "../../../Services/cash-bank.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-cash-bank-details",
    templateUrl: "./cash-bank-details.component.html",
    styleUrls: ["./cash-bank-details.component.css"],
})
export class CashBankDetailsComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    cashBank: CashBank = {
        id: null,
    };
    submitted = false;
    cashBankForm!: FormGroup;
    cashBankTypes: Enum[] = [];
    accounts: Account[] = [];
    showForm = false;
    constructor(
        private fb: FormBuilder,
        private alertify: AlertifyService,
        private route: ActivatedRoute,
        private router: Router,
        private breadcrumbService: BreadcrumbService,
        private cashBankService: CashBankService,
        private translateService: TranslationService,
        private accountService: AccountService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.CashBankDetailsItems;
        this.cashBankTypes = this.cashBankService.Types;

        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.loadDataWithId(id);
        } else {
            this.loadData();
        }
        this.createForm();
    }
    loadDataWithId(id: any) {
        forkJoin(
            this.accountService.GetAll(),
            this.cashBankService.GetById(id)
        ).subscribe(
            ([accountRes, cashBankRes]) => {
                this.accounts = accountRes.filter(
                    (x) => (x.accountTypeId || 0) != 5
                );
                this.cashBank = cashBankRes;
                this.createForm();
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }
    loadData() {
        this.accountService.GetAll().subscribe(
            (accountRes) => {
                this.accounts = accountRes.filter(
                    (x) => (x.accountTypeId || 0) != 5
                );
                this.createForm();
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }
    get Name() {
        return this.cashBankForm.controls["Name"] as FormControl;
    }
    get TypeId() {
        return this.cashBankForm.controls["TypeId"] as FormControl;
    }
    get AccountId() {
        return this.cashBankForm.controls["AccountId"] as FormControl;
    }

    createForm() {
        this.cashBankForm = this.fb.group({
            Name: [null, Validators.required],
            TypeId: [null, Validators.required],
            AccountId: [null, Validators.required],
        });
        this.showForm = true;
    }

    onSubmit() {
        this.submitted = true;
        if (this.cashBankForm.invalid) {
            return;
        }

        if (this.cashBank.id == null) {
            this.Add();
        } else {
            this.Update();
        }
    }
    private Add() {
        this.cashBank.id = uuidv4();
        this.cashBankService.Create(this.cashBank).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.backToList();
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
                this.cashBank.id = null;
            }
        );
    }
    private Update() {
        this.cashBankService.Edit(this.cashBank.id, this.cashBank).subscribe(
            () => {
                const successMsg = this.translateService.Translate(
                    "UpdatedSuccessfully"
                );
                this.alertify.success(successMsg);
                this.backToList();
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }

    backToList() {
        this.router.navigate(["/financials/cash-bank"]);
    }
}
