import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Observable } from "rxjs";
import { ConfirmDialogComponent } from "../Shared/confirm-dialog/confirm-dialog.component";

@Injectable({
    providedIn: "root",
})
export class DialogService {
    constructor(private dialog: MatDialog) {}

    confirm(msg: string): Observable<boolean> {
        return this.dialog
            .open(ConfirmDialogComponent, {
                width: "800px",
                panelClass: "confirm-dialog-container",
                disableClose: true,
                position: { top: "4rem" },
                data: {
                    message: msg,
                },
            })
            .afterClosed();
    }
}
