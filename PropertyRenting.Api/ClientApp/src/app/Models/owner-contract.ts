import { ContractFinancialTransaction } from "./contract-financial-transaction";

export interface OwnerContract {
    id?: any;
    autoNumber?: number;
    contractNumber?: string;
    buildingId?: any;
    ownerId?: any;
    description?: string;
    contractDate?: Date;
    contractStartDate?: Date;
    contractEndDate?: Date;
    contractAmount?: number;
    contractState?: number;
    paymentMethod?: number;
    building?: string;
    owner?: string;
    ownerFinancialTransactions?: ContractFinancialTransaction[];
}
