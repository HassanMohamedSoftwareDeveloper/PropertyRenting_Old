import { Component, OnInit, ViewChild } from "@angular/core";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { Account } from "../../../Models/account";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { AccountService } from "../../../Services/account.service";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-account-list",
    templateUrl: "./account-list.component.html",
    styleUrls: ["./account-list.component.css"],
})
export class AccountListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    accounts: Account[] = [];
    parentAccounts: Account[] = [];
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    account: Account = { id: null };
    isArabic = false;

    constructor(
        private accountService: AccountService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.loadAccounts();
        this.breadcrumbItems = this.breadcrumbService.AccountListItems;
        this.isArabic = this.translateService.GetCurrentLang() == "ar";
    }

    loadAccounts() {
        this.accountService.GetAllGrid().subscribe(
            (result) => {
                this.accounts = result;
                this.parentAccounts = this.accounts.filter(
                    (x) => x.accountTypeId === 5
                );
            },
            (error) => console.log(error)
        );
    }

    getAccountType(typeId: number) {
        return this.accountService.GetAccountTypeById(typeId);
    }
    DeleteAccount(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.accountService.Delete(id).subscribe(
                    () => {
                        this.loadAccounts();
                        const successMsg = this.translateService.Translate(
                            "DeletedSuccessfully"
                        );
                        this.alertify.success(successMsg);
                    },
                    (error) => console.log(error)
                );
            }
        });
    }

    ShowAddModal() {
        this.parentAccounts = this.accounts.filter((x) => x.accountTypeId == 5);
        this.account = { id: null, nameAR: "", nameEN: "" };
        this.modal?.showModal();
    }

    ShowEditModal(account: Account) {
        this.parentAccounts = this.accounts.filter(
            (x) =>
                (x.level || 0) < (account?.level || 0) && x.accountTypeId == 5
        );
        this.account = { ...account };
        this.modal?.showModal();
    }

    HideModal(refreshList: boolean) {
        if (refreshList) {
            this.loadAccounts();
        }
        this.modal?.hideModal();
    }
}
