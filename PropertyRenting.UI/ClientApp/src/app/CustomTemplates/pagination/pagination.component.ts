import { Component, EventEmitter, Input, Output } from "@angular/core";
import { PageChangedEvent } from "ngx-bootstrap/pagination";
import { environment } from "../../../environments/environment";

@Component({
    selector: "app-pagination",
    templateUrl: "./pagination.component.html",
    styleUrls: ["./pagination.component.css"],
})
export class PaginationComponent {
    @Input() showBoundaryLinks = true;
    @Input() pagePage = 1;
    @Input() totalItems = 0;
    @Input() pageSize = environment.PageSize;
    /*@Input() maxSize = environment.PageSize;*/

    @Input() firstText = "First";
    @Input() lastText = "Last";
    @Input() nextText = "Next";
    @Input() previousText = "Previous";

    page?: number;
    @Output() pageChangedEvent = new EventEmitter<number>();

    pageChanged(event: PageChangedEvent): void {
        this.page = event.page;
        this.pageChangedEvent.emit(this.page);
    }
}
