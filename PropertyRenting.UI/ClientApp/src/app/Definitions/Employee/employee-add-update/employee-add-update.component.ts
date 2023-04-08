import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { v4 as uuidv4 } from "uuid";
import { Employee } from "../../../Models/employee";
import { AlertifyService } from "../../../Services/alertify.service";
import { EmployeeService } from "../../../Services/employee.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-employee-add-update",
    templateUrl: "./employee-add-update.component.html",
    styleUrls: ["./employee-add-update.component.css"],
})
export class EmployeeAddUpdateComponent implements OnInit {
    employeeForm!: FormGroup;
    @Input() employee: Employee = { id: null, nameAR: "", nameEN: "" };
    @Output() hideModalWithRefreshEvent = new EventEmitter<boolean>();
    submitted = false;

    constructor(
        private fb: FormBuilder,
        private employeeService: EmployeeService,
        private alertify: AlertifyService,
        private translateService: TranslationService
    ) {}

    ngOnInit(): void {
        this.CreateFrom();
    }

    CreateFrom() {
        this.employeeForm = this.fb.group({
            NameAR: [null, Validators.required],
            NameEN: [null, Validators.required],
        });
    }

    //#region Form Controllers
    get NameAR() {
        return this.employeeForm.controls["NameAR"] as FormControl;
    }
    get NameEN() {
        return this.employeeForm.controls["NameEN"] as FormControl;
    }
    //#endregion

    onSubmit() {
        this.submitted = true;
        if (this.employeeForm.invalid) {
            return;
        }
        if (this.employee.id === null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.employee.id = uuidv4();
        this.employeeService.CreateNewEmployee(this.employee).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.cancel(true);
            },
            (error) => {
                console.log(error);
                this.employee.id = null;
            }
        );
    }
    private Update() {
        this.employeeService
            .EditEmployee(this.employee.id, this.employee)
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
        this.employeeForm.reset();
        this.submitted = false;
    }

    cancel(isRefresh: boolean) {
        this.resetFrom();
        this.hideModalWithRefreshEvent.emit(isRefresh);
    }
}
