import { Component, OnInit } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { ChangePassword } from "../../Models/change-password";
import { AlertifyService } from "../../Services/alertify.service";
import { AuthService } from "../../Services/auth.service";
import { TranslationService } from "../../Services/translation.service";

@Component({
    selector: "app-change-password",
    templateUrl: "./change-password.component.html",
    styleUrls: ["./change-password.component.css"],
})
export class ChangePasswordComponent implements OnInit {
    errors: any[] = [];
    request: ChangePassword = { currentPassword: "", newPassword: "" };
    form!: FormGroup;
    submitted = false;
    constructor(
        private authService: AuthService,
        private fb: FormBuilder,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.createFrom();
    }

    createFrom() {
        this.form = this.fb.group({
            CurrentPassword: [null, Validators.required],
            NewPassword: [null, Validators.required],
        });
    }

    get CurrentPassword() {
        return this.form.controls["CurrentPassword"] as FormControl;
    }
    get NewPassword() {
        return this.form.controls["NewPassword"] as FormControl;
    }

    onSubmit() {
        this.errors = [];
        this.submitted = true;
        if (this.form.invalid) {
            return;
        }

        this.authService.ChangePassword(this.request).subscribe(
            (_) => {
                const successMsg = this.translateService.Translate(
                    "ChangePasswordSuccess"
                );
                this.alertify.success(successMsg);
                this.authService.loggOut();
                this.router.navigate(["/auth/login"]);
            },
            (error) => {
                const errorMsg =
                    this.translateService.Translate("ErrorOccurred");
                this.alertify.error(errorMsg);
                this.errors = error?.error?.errors;
            }
        );
    }
    backToList() {
        this.router.navigate(["/"]);
    }
}
