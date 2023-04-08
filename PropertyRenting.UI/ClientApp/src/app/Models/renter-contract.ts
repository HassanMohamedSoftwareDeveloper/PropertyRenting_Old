import { ContractFinancialTransaction } from "./contract-financial-transaction";

export interface RenterContract {
    id?: any;
    autoNumber?: number;
    contractNumber?: string;
    unitId?: any;
    renterId?: any;
    description?: string;
    contractDate?: Date;
    contractStartDate?: Date;
    contractEndDate?: Date;
    contractAmount?: number;
    contractState?: number;
    paymentMethod?: number;
    increasing?: boolean;
    increasingValue?: number;
    unitName?: string;
    unitNumber?: string;
    buildingId?: any;
    buildingName?: string;
    renter?: string;
    renterFinancialTransactions?: ContractFinancialTransaction[];
}
