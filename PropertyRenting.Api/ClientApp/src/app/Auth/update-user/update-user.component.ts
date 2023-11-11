import { Component, OnInit } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslationService } from "../../Services/translation.service";
import { AlertifyService } from "../../Services/alertify.service";
import { AuthService } from "../../Services/auth.service";
import { UpdateUser } from "../../Models/update-user";

@Component({
    selector: "app-update-user",
    templateUrl: "./update-user.component.html",
    styleUrls: ["./update-user.component.css"],
})
export class UpdateUserComponent implements OnInit {
    showForm = false;
    errors: any[] = [];
    user: UpdateUser = {};
    currentUsername = "";
    form!: FormGroup;
    submitted = false;
    roles = ["Admin", "SubAdmin", "User"];
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
        this.getUser(id ?? "");
    }

    createFrom() {
        this.form = this.fb.group({
            Username: [null, Validators.required],
            Email: [null, [Validators.email]],
            Role: [null, Validators.required],
        });
        this.showForm = true;
    }
    getUser(userId: string) {
        this.authService.GetUserById(userId).subscribe({
            next: (res) => {
                this.currentUsername = res.username ?? "";
                this.user = {
                    userId: res.id,
                    username: res.username,
                    email: res.email,
                    role: res.role,
                };

                this.createFrom();
            },
            error: (error) => {
                const errorMsg =
                    this.translateService.Translate("ErrorOccurred");
                this.alertify.error(errorMsg);
                console.log(error);
            },
        });
    }
    get Username() {
        return this.form.controls["Username"] as FormControl;
    }
    get Email() {
        return this.form.controls["Email"] as FormControl;
    }
    get Role() {
        return this.form.controls["Role"] as FormControl;
    }

    onSubmit() {
        this.errors = [];
        this.submitted = true;
        if (this.form.invalid) {
            return;
        }

        this.authService.UpdateUser(this.user).subscribe({
            next: () => {
                const successMsg =
                    this.translateService.Translate("UpdateSuccess");
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
