import { Component } from "@angular/core";

@Component({
    selector: "app-integer-number-input",
    templateUrl: "./integer-number-input.component.html",
    styleUrls: ["./integer-number-input.component.css"],
})
export class IntegerNumberInputComponent {
    keyPressNumbers(event: any) {
        const charCode = event.which ? event.which : event.keyCode;
        // Only Numbers 0-9
        if (charCode < 48 || charCode > 57) {
            event.preventDefault();
            return false;
        } else {
            return true;
        }
    }
}
