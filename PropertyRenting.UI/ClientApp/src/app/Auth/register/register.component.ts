import { Component, OnInit } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { Register } from "../../Models/register";
import { AlertifyService } from "../../Services/alertify.service";
import { AuthService } from "../../Services/auth.service";
import { TranslationService } from "../../Services/translation.service";

@Component({
    selector: "app-register",
    templateUrl: "./register.component.html",
    styleUrls: ["./register.component.css"],
})
export class RegisterComponent implements OnInit {
    errors: any[] = [];
    register: Register = {};
    registerForm!: FormGroup;
    submitted = false;
    roles = ["Admin", "User"];
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
        this.registerForm = this.fb.group({
            Username: [null, Validators.required],
            Email: [null, [Validators.required, Validators.email]],
            Password: [null, Validators.required],
            Role: [null, Validators.required],
        });
    }

    get Username() {
        return this.registerForm.controls["Username"] as FormControl;
    }
    get Email() {
        return this.registerForm.controls["Email"] as FormControl;
    }
    get Password() {
        return this.registerForm.controls["Password"] as FormControl;
    }
    get Role() {
        return this.registerForm.controls["Role"] as FormControl;
    }

    onSubmit() {
        this.errors = [];
        this.submitted = true;
        if (this.registerForm.invalid) {
            return;
        }

        this.authService.Register(this.register).subscribe(
            (_) => {
                const successMsg =
                    this.translateService.Translate("RegisterSuccess");
                this.alertify.success(successMsg);
                this.backToList();
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
        this.router.navigate(["/auth/users"]);
    }
}
