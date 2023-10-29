export interface ContractFinancialTransaction {
    id?: any;
    contractId?: any;
    amount?: number;
    paidAmount?: number;
    balance?: number;
    contractAdditionId?: any;
    dueDate?: Date;
    isPaid?: boolean;
    isCancelled?: boolean;
    tempId?: any;
    contractAddition?: string;
    contractNumber?: string;
    buildingId?: any;
    unitId?: any;
    selected?: boolean;
}
