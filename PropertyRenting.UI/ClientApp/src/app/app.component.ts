import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { AuthService } from "./Services/auth.service";

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.css"],
})
export class AppComponent {
    title = "ClientApp";
    sideNavStatus = true;
    constructor(
        public translate: TranslateService,
        private authService: AuthService
    ) {
        translate.addLangs(["en", "ar"]);
        translate.setDefaultLang("en");
    }
    get isLoggedIn() {
        return this.authService.LoggedIn();
    }
    switchLang(lang: string) {
        this.translate.use(lang);
    }
}
