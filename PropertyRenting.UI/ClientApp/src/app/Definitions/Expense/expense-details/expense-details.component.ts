import { Component, OnInit } from "@angular/core";
import { v4 as uuidv4 } from "uuid";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Expense } from "../../../Models/expense";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { ExpenseService } from "../../../Services/expense.service";
import { TranslationService } from "../../../Services/translation.service";
import { AccountService } from "../../../Services/account.service";
import { Account } from "../../../Models/account";
import { forkJoin } from "rxjs";

@Component({
    selector: "app-expense-details",
    templateUrl: "./expense-details.component.html",
    styleUrls: ["./expense-details.component.css"],
})
export class ExpenseDetailsComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    expense: Expense = {
        id: null,
    };
    submitted = false;
    expenseForm!: FormGroup;
    accounts: Account[] = [];
    showForm = false;
    constructor(
        private fb: FormBuilder,
        private alertify: AlertifyService,
        private route: ActivatedRoute,
        private router: Router,
        private breadcrumbService: BreadcrumbService,
        private expenseService: ExpenseService,
        private translateService: TranslationService,
        private accountService: AccountService
    ) {}

    ngOnInit(): void {
        this.createForm();

        this.breadcrumbItems = this.breadcrumbService.ExpenseDetailsItems;

        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.loadDataWithId(id);
        } else {
            this.loadData();
        }
    }
    loadDataWithId(id: any) {
        forkJoin(
            this.accountService.GetAll(),
            this.expenseService.GetById(id)
        ).subscribe(
            ([accountRes, expenseRes]) => {
                this.accounts = accountRes.filter(
                    (x) => (x.accountTypeId || 0) != 5
                );
                this.expense = expenseRes;
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

    get NameAR() {
        return this.expenseForm.controls["NameAR"] as FormControl;
    }
    get NameEN() {
        return this.expenseForm.controls["NameEN"] as FormControl;
    }
    get AccountId() {
        return this.expenseForm.controls["AccountId"] as FormControl;
    }

    createForm() {
        this.expenseForm = this.fb.group({
            NameAR: [null, Validators.required],
            NameEN: [null, Validators.required],
            AccountId: [null, Validators.required],
        });
        this.showForm = true;
    }

    onSubmit() {
        this.submitted = true;
        if (this.expenseForm.invalid) {
            return;
        }

        if (this.expense.id == null) {
            this.Add();
        } else {
            this.Update();
        }
    }
    private Add() {
        this.expense.id = uuidv4();
        this.expenseService.Create(this.expense).subscribe(
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
                this.expense.id = null;
            }
        );
    }
    private Update() {
        this.expenseService.Edit(this.expense.id, this.expense).subscribe(
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
        this.router.navigate(["/accounts/expenses"]);
    }
}
