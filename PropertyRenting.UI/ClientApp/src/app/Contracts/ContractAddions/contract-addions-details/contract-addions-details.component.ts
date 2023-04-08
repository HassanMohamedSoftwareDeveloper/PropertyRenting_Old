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
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { TranslationService } from "../../../Services/translation.service";
import { ContractAddions } from "../../../Models/contract-addions";
import { ContractAddionsService } from "../../../Services/contract-addions.service";
import { AccountService } from "../../../Services/account.service";
import { Account } from "../../../Models/account";
import { forkJoin, Observable } from "rxjs";

@Component({
    selector: "app-contract-addions-details",
    templateUrl: "./contract-addions-details.component.html",
    styleUrls: ["./contract-addions-details.component.css"],
})
export class ContractAddionsDetailsComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    contractAddition: ContractAddions = {
        id: null,
    };
    submitted = false;
    contractAdditionForm!: FormGroup;
    accounts: Account[] = [];
    showForm = false;
    constructor(
        private fb: FormBuilder,
        private alertify: AlertifyService,
        private route: ActivatedRoute,
        private router: Router,
        private breadcrumbService: BreadcrumbService,
        private contractAdditionService: ContractAddionsService,
        private translateService: TranslationService,
        private accountService: AccountService
    ) {}

    ngOnInit(): void {
        this.createForm();

        this.breadcrumbItems =
            this.breadcrumbService.ContractAdditionDetailsItems;

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
            this.contractAdditionService.GetById(id)
        ).subscribe(
            ([accountRes, additionRes]) => {
                this.accounts = accountRes.filter(
                    (x) => (x.accountTypeId || 0) != 5
                );
                this.contractAddition = additionRes;
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
        return this.contractAdditionForm.controls["NameAR"] as FormControl;
    }
    get NameEN() {
        return this.contractAdditionForm.controls["NameEN"] as FormControl;
    }
    get AccountId() {
        return this.contractAdditionForm.controls["AccountId"] as FormControl;
    }

    createForm() {
        this.contractAdditionForm = this.fb.group({
            NameAR: [null, Validators.required],
            NameEN: [null, Validators.required],
            AccountId: [null, Validators.required],
        });
        this.showForm = true;
    }

    onSubmit() {
        this.submitted = true;
        if (this.contractAdditionForm.invalid) {
            return;
        }

        if (this.contractAddition.id == null) {
            this.Add();
        } else {
            this.Update();
        }
    }
    private Add() {
        this.contractAddition.id = uuidv4();
        this.contractAdditionService.Create(this.contractAddition).subscribe(
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
                this.contractAddition.id = null;
            }
        );
    }
    private Update() {
        this.contractAdditionService
            .Edit(this.contractAddition.id, this.contractAddition)
            .subscribe(
                () => {
                    const successMsg = this.translateService.Translate(
                        "UpdatedSuccessfully"
                    );
                    this.alertify.success(successMsg);
                    this.backToList();
                },
                (error) => {
                    const msg =
                        this.translateService.Translate("ErrorOccurred");
                    this.alertify.error(msg);
                    console.log(error);
                }
            );
    }

    backToList() {
        this.router.navigate(["/contracts/contract-additions"]);
    }
}
