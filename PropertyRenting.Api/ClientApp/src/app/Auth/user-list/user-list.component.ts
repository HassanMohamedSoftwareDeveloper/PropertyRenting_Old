import { Component, OnInit } from "@angular/core";
import { Breadcrumb } from "../../Models/breadcrumb";
import { User } from "../../Models/user";
import { AlertifyService } from "../../Services/alertify.service";
import { AuthService } from "../../Services/auth.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { TranslationService } from "../../Services/translation.service";

@Component({
    selector: "app-user-list",
    templateUrl: "./user-list.component.html",
    styleUrls: ["./user-list.component.css"],
})
export class UserListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    users: User[] = [];

    constructor(
        private authService: AuthService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcumbService: BreadcrumbService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcumbService.UsersItems;
        this.LoadUsers();
    }

    LoadUsers() {
        this.authService.GetAll().subscribe(
            (result) => {
                this.users = result;
            },
            (error) => {
                const errorMsg =
                    this.translateService.Translate("ErrorOccurred");
                this.alertify.error(errorMsg);
                console.log(error);
            }
        );
    }
}
