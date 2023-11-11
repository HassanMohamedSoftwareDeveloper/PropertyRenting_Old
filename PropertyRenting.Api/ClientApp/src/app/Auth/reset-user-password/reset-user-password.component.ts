import { Component, OnInit } from "@angular/core";
import { ResetUserPassword } from "../../Models/reset-user-password";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuthService } from "../../Services/auth.service";
import { AlertifyService } from "../../Services/alertify.service";
import { TranslationService } from "../../Services/translation.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: "app-reset-user-password",
    templateUrl: "./reset-user-password.component.html",
    styleUrls: ["./reset-user-password.component.css"],
})
export class ResetUserPasswordComponent implements OnInit {
    showForm = false;
    errors: any[] = [];
    user: ResetUserPassword = {};
    currentUsername = "";
    form!: FormGroup;
    submitted = false;
    constructor(
        private authService: AuthService,
        private fb: FormBuilder,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private router: Router,
        private route: ActivatedRoute
    ) {}

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get("id");
        this.user = { userId: id ?? "" };
        this.createFrom();
    }

    createFrom() {
        this.form = this.fb.group({
            Password: [null, Validators.required],
        });
        this.showForm = true;
    }
    onSubmit() {
        this.errors = [];
        this.submitted = true;
        if (this.form.invalid) {
            return;
        }

        this.authService.ResetUserPassword(this.user).subscribe({
            next: () => {
                const successMsg =
                    this.translateService.Translate("ResetSuccess");
                this.alertify.success(successMsg);
                if (this.authService.UserName() === this.currentUsername) {
                    this.authService.loggOut();
                    this.router.navigate(["/auth/login"]);
                } else {
                    this.backToList();
                }
            },
            error: (error) => {
                const errorMsg =
                    this.translateService.Translate("ErrorOccurred");
                this.alertify.error(errorMsg);
                this.errors = error?.error?.errors;
            },
        });
    }
    backToList() {
        this.router.navigate(["/auth/users"]);
    }
}
