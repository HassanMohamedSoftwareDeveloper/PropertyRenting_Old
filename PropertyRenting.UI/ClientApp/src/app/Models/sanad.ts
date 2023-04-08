import { SanadDetails } from "./sanad-details";

export interface Sanad {
    id?: any;
    autoNumber?: number;
    sanadNumber?: string;
    sanadDate?: Date;
    description?: string;
    amount?: number;
    cashBankId?: any;
    cashBank?: string;
    renterId?: any;
    renter?: string;
    ownerId?: any;
    owner?: string;
    contributerId?: any;
    contributer?: string;
    sanadTypeId: number;
    stateId?: number;
    sanadDetails: SanadDetails[];
}
