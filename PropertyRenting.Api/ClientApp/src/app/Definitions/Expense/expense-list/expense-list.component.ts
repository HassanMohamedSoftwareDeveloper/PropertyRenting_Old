import { Component, OnInit } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Expense } from "../../../Models/expense";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { ExpenseService } from "../../../Services/expense.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-expense-list",
    templateUrl: "./expense-list.component.html",
    styleUrls: ["./expense-list.component.css"],
})
export class ExpenseListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    expenses: Expense[] = [];

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private expenseService: ExpenseService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.loadData();
        this.breadcrumbItems = this.breadcrumbService.ExpenseListItems;
    }

    loadData() {
        this.expenseService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.expenses = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteExpense(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.expenseService.Delete(id).subscribe(
                    () => {
                        this.loadData();
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

    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.loadData();
    }
}
