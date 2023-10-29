import { Component, Input } from "@angular/core";

@Component({
    selector: "app-custom-collapse",
    templateUrl: "./custom-collapse.component.html",
    styleUrls: ["./custom-collapse.component.css"],
})
export class CustomCollapseComponent {
    @Input() isCollapsed = false;
}
