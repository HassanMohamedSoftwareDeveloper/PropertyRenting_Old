import { Component, EventEmitter, Input, Output } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Breadcrumb } from "../../Models/breadcrumb";

@Component({
    selector: "app-list",
    templateUrl: "./list.component.html",
    styleUrls: ["./list.component.css"],
})
export class ListComponent {
    @Input() hasPagination = false;
    @Input() pageNumber = 1;
    @Input() totalItems = 0;
    @Input() title = "";
    pageSize = environment.PageSize;
    @Input() breadcrumbItems: Breadcrumb[] = [];
    @Output() pageChangedEvent = new EventEmitter<number>();

    GetDataByPage(page: number) {
        this.pageChangedEvent.emit(page);
    }
}
