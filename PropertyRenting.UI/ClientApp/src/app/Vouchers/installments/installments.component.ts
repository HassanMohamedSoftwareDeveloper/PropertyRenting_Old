import { Component, EventEmitter, Input, Output } from "@angular/core";
import { ContractFinancialTransaction } from "../../Models/contract-financial-transaction";

@Component({
    selector: "app-installments",
    templateUrl: "./installments.component.html",
    styleUrls: ["./installments.component.css"],
})
export class InstallmentsComponent {
    @Input() installments: ContractFinancialTransaction[] = [];
    @Output() hideModalEvent = new EventEmitter();
    @Output() selectedInstallmentsEvent = new EventEmitter<
        ContractFinancialTransaction[]
    >();
    allSelected = false;
    selectAll() {
        for (const installment of this.installments) {
            installment.selected = this.allSelected;
        }
    }
    selectChange(curInstallment: ContractFinancialTransaction) {
        if (curInstallment.selected === false) {
            this.allSelected = false;
        } else {
            this.allSelected = true;
            for (const installment of this.installments) {
                if (installment.selected === false) {
                    this.allSelected = false;
                    return;
                }
            }
        }
    }
    cancel() {
        this.hideModalEvent.emit();
    }
    addInstallments() {
        const added = this.installments.filter((x) => x.selected === true);
        this.selectedInstallmentsEvent.emit(added);
    }
}
