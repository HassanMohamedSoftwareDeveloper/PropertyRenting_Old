import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { Router } from "@angular/router";
import { AlertifyService } from "../../Services/alertify.service";
import { AuthService } from "../../Services/auth.service";
import { TranslationService } from "../../Services/translation.service";

@Component({
    selector: "app-nav-bar",
    templateUrl: "./nav-bar.component.html",
    styleUrls: ["./nav-bar.component.css"],
})
export class NavBarComponent implements OnInit {
    @Output() sideNavToggled = new EventEmitter<boolean>();
    menuStatus = true;
    lang = "";
    displayName = "";
    langList: any[] = [];

    constructor(
        private translate: TranslationService,
        public authService: AuthService,
        private router: Router,
        private alertify: AlertifyService,
        private translateService: TranslationService
    ) {
        translate.UseLang();
        this.langList = translate.LangList;
    }

    ngOnInit(): void {
        this.displayName = this.translate.GetCurrentLanguageDisplayName();
        this.lang = this.translate.GetCurrentLang();
    }

    changeLang(lang: string) {
        if (lang === this.lang) {
            return;
        }
        this.translate.SetCurrentLang(lang);
    }

    SideNavToggle() {
        this.menuStatus = !this.menuStatus;
        this.sideNavToggled.emit(this.menuStatus);
    }
    get isLoggedIn() {
        return this.authService.LoggedIn();
    }
    get isAdmin() {
        return this.authService.LoggedIn() && this.authService.IsAdmin();
    }
    logOut() {
        this.authService.loggOut();
        this.router.navigate(["/"]);
        const successMsg = this.translateService.Translate("LoggedOutSuccess");
        this.alertify.success(successMsg);
    }
}
