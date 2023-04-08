export interface Contract {
    id?: any;
    contractNumber?: number;
    buildingId?: any;
    ownerId?: any;
    description?: string;
    contractDate?: Date;
    contractStartDate?: Date;
    contractEndDate?: Date;
    contractAmount?: number;
    contractState?: number;
    paymentMethod?: number;
}
