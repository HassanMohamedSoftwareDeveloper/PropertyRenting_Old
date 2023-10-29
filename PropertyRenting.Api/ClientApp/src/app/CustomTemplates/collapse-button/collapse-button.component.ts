import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
    selector: "app-collapse-button",
    templateUrl: "./collapse-button.component.html",
    styleUrls: ["./collapse-button.component.css"],
})
export class CollapseButtonComponent {
    @Output() collapsedEvent = new EventEmitter<boolean>();
    @Input() isCollapsed = false;

    collapse() {
        this.isCollapsed = !this.isCollapsed;
        this.collapsedEvent.emit(this.isCollapsed);
    }
}
