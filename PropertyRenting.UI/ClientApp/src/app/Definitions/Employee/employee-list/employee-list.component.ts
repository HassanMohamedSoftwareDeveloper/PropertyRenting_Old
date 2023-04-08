import { Component, OnInit, ViewChild } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Employee } from "../../../Models/employee";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { EmployeeService } from "../../../Services/employee.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-employee-list",
    templateUrl: "./employee-list.component.html",
    styleUrls: ["./employee-list.component.css"],
})
export class EmployeeListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    employees: Employee[] = [];
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    employee: Employee = { id: null, nameAR: "", nameEN: "" };

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private employeeService: EmployeeService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.LoadEmployees();
        this.breadcrumbItems = this.breadcrumbService.EmployeeListItems;
    }

    LoadEmployees() {
        this.employeeService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.employees = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteEmployee(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.employeeService.DeleteEmployee(id).subscribe(
                    () => {
                        this.LoadEmployees();
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
        this.employee = { id: null, nameAR: "", nameEN: "" };
        this.modal?.showModal();
    }

    ShowEditModal(employee: Employee) {
        this.employee = { ...employee };
        this.modal?.showModal();
    }

    HideModal(refreshList: boolean) {
        if (refreshList) {
            this.LoadEmployees();
        }
        this.modal?.hideModal();
    }

    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.LoadEmployees();
        console.log(page);
    }
}
