import { NestedTreeControl } from "@angular/cdk/tree";
import { Component, OnInit, ViewChild } from "@angular/core";
import { MatTreeNestedDataSource } from "@angular/material/tree";
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
    selectedAccount: Account = { id: null };
    isArabic = false;
    //pageNumber = 1;
    //totalItems = 0;

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
        this.accountService.GetAll().subscribe(
            (result) => {
                this.accounts = result;
                this.parentAccounts = this.accounts.filter(
                    (x) => x.accountTypeId === 5
                );
                this.dataSource.data = this.accounts.filter(
                    (x) => x.level === 1
                );
                if (this.account.id != null) {
                    this.selectedAccount = this.accounts.find(
                        (x) => x.id == this.account.id
                    ) || { id: null };
                }
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
                        if (this.selectedAccount.id == id) {
                            this.selectedAccount = { id: null };
                        }
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

    dataSource = new MatTreeNestedDataSource<Account>();
    treeControl = new NestedTreeControl<Account>(
        (node) => node?.accountChildren
    );
    hasChild(_: number, _nodeData: Account) {
        return (
            !!_nodeData.accountChildren && _nodeData.accountChildren.length > 0
        );
    }

    selectAccount(acc: Account) {
        this.selectedAccount = { ...acc };
    }
}
