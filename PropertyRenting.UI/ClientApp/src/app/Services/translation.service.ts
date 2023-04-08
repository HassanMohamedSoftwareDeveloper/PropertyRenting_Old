import { Injectable } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";

@Injectable({
    providedIn: "root",
})
export class TranslationService {
    LangList = [
        {
            DisplayName: "NavBar.Lang-Arabic",
            Value: "ar",
            Img: "ar.png",
        },
        {
            DisplayName: "NavBar.Lang-English",
            Value: "en",
            Img: "en.png",
        },
    ];

    constructor(private translate: TranslateService) {
        translate.setDefaultLang("en");
    }

    GetCurrentLang(): string {
        return localStorage.getItem("lang") || "en";
    }

    SetCurrentLang(lang: string) {
        localStorage.setItem("lang", lang);
        window.location.reload();
    }

    UseLang() {
        const lang = this.GetCurrentLang();
        this.translate.use(lang);
        document.documentElement.lang = lang;
    }

    GetCurrentLanguageDisplayName() {
        const langValue = this.GetCurrentLang();
        const fullLang = this.LangList.find((x) => x.Value === langValue);
        return fullLang?.DisplayName || "NavBar.Lang-English";
    }

    Translate(key: string) {
        let value = "";
        this.translate.get(key).subscribe((res) => (value = res));
        return value;
    }
}
