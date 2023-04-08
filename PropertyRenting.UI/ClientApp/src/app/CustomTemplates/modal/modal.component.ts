import { Component, Input, ViewChild } from "@angular/core";
import { ModalDirective, BsModalRef } from "ngx-bootstrap/modal";

@Component({
    selector: "app-modal",
    templateUrl: "./modal.component.html",
    styleUrls: ["./modal.component.css"],
})
export class ModalComponent {
    @Input() modalHeader = "";
    modalRef?: BsModalRef;
    config = {
        backdrop: false,
        ignoreBackdropClick: true,
        keyboard: false,
    };
    @ViewChild("childModal", { static: false }) childModal?: ModalDirective;

    showModal(): void {
        this.childModal?.show();
    }

    hideModal(): void {
        this.childModal?.hide();
    }
}
