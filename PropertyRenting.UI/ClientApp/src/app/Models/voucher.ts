import { VoucherDetails } from "./voucher-details";

export interface Voucher {
    id?: any;
    voucherId?: string;
    autoNumber?: number;
    voucherDate?: Date;
    referenceId?: any;
    referenceAutoNumber?: number;
    referenceManualNumber?: string;
    referenceType?: string;
    description?: string;
    stateId?: number;
    voucherDetails: VoucherDetails[];
}
