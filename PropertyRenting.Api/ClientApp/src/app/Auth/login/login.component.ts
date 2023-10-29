import { Component, OnInit } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Login } from "../../Models/login";
import { AlertifyService } from "../../Services/alertify.service";
import { AuthService } from "../../Services/auth.service";
import { TranslationService } from "../../Services/translation.service";

@Component({
    selector: "app-login",
    templateUrl: "./login.component.html",
    styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
    loginForm!: FormGroup;
    submitted = false;
    logginRes = true;
    constructor(
        private fb: FormBuilder,
        public authService: AuthService,
        private route: ActivatedRoute,
        private router: Router,
        private alertify: AlertifyService,
        private translateService: TranslationService
    ) {}

    ngOnInit(): void {
        if (this.authService.LoggedIn()) {
            this.router.navigate(["/"]);
        }
        this.createFrom();
    }
    createFrom() {
        this.loginForm = this.fb.group({
            Username: [null, Validators.required],
            Password: [null, Validators.required],
        });
    }
    get Username() {
        return this.loginForm.controls["Username"] as FormControl;
    }
    get Password() {
        return this.loginForm.controls["Password"] as FormControl;
    }
    onSubmit() {
        this.submitted = true;
        this.logginRes = true;

        if (this.loginForm.invalid) {
            return;
        }
        const login: Login = {
            username: this.Username.value,
            password: this.Password.value,
        };

        this.authService.Login(login).subscribe(
            (_) => {
                const successMsg =
                    this.translateService.Translate("LoggedInSuccess");
                this.alertify.success(successMsg);
                const returnUrl =
                    this.route.snapshot.queryParamMap.get("returnUrl") || "";
                this.router.navigate(["/" + returnUrl]);
            },
            (error) => {
                const errorMsg =
                    this.translateService.Translate("ErrorOccurred");
                this.alertify.error(errorMsg);
                console.log(error);
                this.logginRes = false;
            }
        );
    }
}
