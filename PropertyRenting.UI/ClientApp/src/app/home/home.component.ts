import { Component, OnInit } from "@angular/core";
import { Breadcrumb } from "../Models/breadcrumb";
import { BreadcrumbService } from "../Services/breadcrumb.service";

@Component({
    selector: "app-home",
    templateUrl: "./home.component.html",
    styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    constructor(private breadcrumbService: BreadcrumbService) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.HomepageItems;
    }
}
