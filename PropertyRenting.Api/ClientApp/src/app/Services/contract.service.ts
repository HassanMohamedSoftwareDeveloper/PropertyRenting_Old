import { Injectable } from "@angular/core";
import { Enum } from "../Models/enum";

@Injectable({
    providedIn: "root",
})
export class ContractService {
    PaymentMethods: Enum[] = [
        { id: 1, description: "PaymentMethod.Monthly" },
        { id: 2, description: "PaymentMethod.Quarter" },
        { id: 3, description: "PaymentMethod.TwoMonths" },
        { id: 4, description: "PaymentMethod.HalfYear" },
        { id: 5, description: "PaymentMethod.Yearly" },
    ];

    private ContractStates: Enum[] = [
        { id: 1, description: "ContractState.Open" },
        { id: 2, description: "ContractState.Activated" },
        { id: 3, description: "ContractState.Canceled" },
        { id: 4, description: "ContractState.Closed" },
    ];

    GetContractStateById(id: number): string {
        return this.ContractStates?.find((x) => x.id == id)?.description || "";
    }
    GetPaymentMethodById(id: number): string {
        return this.PaymentMethods?.find((x) => x.id == id)?.description || "";
    }
}
